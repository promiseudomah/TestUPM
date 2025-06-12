import os
import shutil

def sync_sdk():
    base = os.path.dirname(os.path.abspath(__file__))

    source_dir = os.path.join(base, "UnityDevProject", "Assets", "TPromise")
    dest_dir = os.path.join(base, "upm", "com.metaversemagna.tpromise")

    if not os.path.exists(source_dir):
        print(f"[❌] Source folder not found: {source_dir}")
        exit(1)

    # Delete destination folder if it exists
    if os.path.exists(dest_dir):
        print(f"[🧹] Cleaning existing folder: {dest_dir}")
        shutil.rmtree(dest_dir)

    # Copy entire TPromise folder (including Runtime, Editor, etc.)
    print(f"[📥] Copying from:\n  {source_dir}\nto:\n  {dest_dir}")
    shutil.copytree(source_dir, dest_dir)

    print("[✅] SDK sync complete.")

if __name__ == "__main__":
    sync_sdk()
