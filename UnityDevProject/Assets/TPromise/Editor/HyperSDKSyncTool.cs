using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;

namespace TPromise.Utilities
{
    public class HyperSDKSyncTool : EditorWindow
    {
        private static readonly string sourcePath = "Assets/TPromise";
        private static readonly string destinationPath = "upm/com.metaversemagna.tpromise";
        private static bool changesDetected = false;

        // Main menu item that runs the sync
        [MenuItem("HyperSDK/Sync to Hyper UPM %h", true)]
        private static bool ValidateSyncMenu()
        {
            // Validation method: return true to enable, false to disable
            changesDetected = !DirectoriesAreEqual(sourcePath, destinationPath);
            return changesDetected;
        }

        [MenuItem("HyperSDK/Sync to Hyper UPM %h", false)]
        public static void SyncToUPM()
        {
            if (!Directory.Exists(sourcePath))
            {
                Debug.LogError("[HyperSDK Sync] Source path not found: " + sourcePath);
                return;
            }

            if (Directory.Exists(destinationPath))
            {
                Debug.Log("[HyperSDK Sync] Cleaning existing UPM folder...");
                Directory.Delete(destinationPath, true);
            }

            Debug.Log("<color=#E7B73DFF>[HyperSDK Sync] Copying from: " + sourcePath + " to: " + destinationPath +"</color>");
            CopyDirectory(sourcePath, destinationPath);
            AssetDatabase.Refresh();
            Debug.Log("<color=#3DE73DFF>[HyperSDK Sync] UPM sync complete.</color>");
        }

        [InitializeOnLoadMethod]
        private static void SetupWatcher()
        {
            EditorApplication.update += EditorUpdate;
        }

        private static void EditorUpdate()
        {
            // Force menu to revalidate on every update
            Menu.SetChecked("HyperSDK/Sync to Hyper UPM %h", !DirectoriesAreEqual(sourcePath, destinationPath));
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

        private static bool DirectoriesAreEqual(string dir1, string dir2)
        {
            if (!Directory.Exists(dir1) || !Directory.Exists(dir2))
                return false;

            var files1 = Directory.GetFiles(dir1, "*", SearchOption.AllDirectories)
                .Select(f => f.Substring(dir1.Length).TrimStart(Path.DirectorySeparatorChar)).OrderBy(f => f).ToArray();
            var files2 = Directory.GetFiles(dir2, "*", SearchOption.AllDirectories)
                .Select(f => f.Substring(dir2.Length).TrimStart(Path.DirectorySeparatorChar)).OrderBy(f => f).ToArray();

            if (files1.Length != files2.Length)
                return false;

            for (int i = 0; i < files1.Length; i++)
            {
                if (files1[i] != files2[i])
                    return false;

                var file1 = Path.Combine(dir1, files1[i]);
                var file2 = Path.Combine(dir2, files2[i]);

                if (!File.ReadAllBytes(file1).SequenceEqual(File.ReadAllBytes(file2)))
                    return false;
            }

            return true;
        }
    }
}
