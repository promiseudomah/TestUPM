using UnityEditor;
using UnityEngine;
using System.IO;

namespace TPromise.Utilities
{
    public class HyperSDKSyncTool
    {
        // ✅ Source is still inside Unity project
        private static readonly string sourcePath = "Assets/TPromise";

        // ✅ Destination is now OUTSIDE Unity project, so we calculate from project root
        private static readonly string destinationPath = "../upm/com.metaversemagna.tpromise";

        [MenuItem("HyperSDK/Sync to UPM %#s")]
        public static void SyncToUPM()
        {
            string fullSourcePath = Path.Combine(Application.dataPath, "TPromise");
            string fullDestinationPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", destinationPath));

            if (!Directory.Exists(fullSourcePath))
            {
                Debug.LogError("[HyperSDK Sync] Source path not found: " + fullSourcePath);
                return;
            }

            if (Directory.Exists(fullDestinationPath))
            {
                Debug.Log("[HyperSDK Sync] Clearing existing UPM folder...");
                Directory.Delete(fullDestinationPath, true);
            }

            Debug.Log($"[HyperSDK Sync] Copying from:\n  {fullSourcePath}\nto:\n  {fullDestinationPath}");
            CopyDirectory(fullSourcePath, fullDestinationPath);

            AssetDatabase.Refresh();
            Debug.Log("[✅] UPM sync complete.");
        }

        private static void CopyDirectory(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (var file in Directory.GetFiles(sourceDir))
            {
                var fileName = Path.GetFileName(file);
                var targetFilePath = Path.Combine(targetDir, fileName);
                File.Copy(file, targetFilePath);
            }

            foreach (var directory in Directory.GetDirectories(sourceDir))
            {
                var dirName = Path.GetFileName(directory);
                var targetSubDir = Path.Combine(targetDir, dirName);
                CopyDirectory(directory, targetSubDir);
            }
        }
    }
}
