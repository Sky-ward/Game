# Asset Replacement Guide

All assets listed in `Assets/Configs/assets_manifest.csv` must be provided as external binary files.

1. Place the new asset under the path specified in the manifest.
2. Enable **Git LFS** or dedicated asset branch to commit binaries.
3. Update Addressables groups accordingly.
4. Keep placeholder `.keep` files for empty directories.
