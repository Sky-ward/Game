# Asset Replacement Guide

This repository commits **no binary assets**. All real art, audio and third‑party libraries must be supplied outside of version control or via Git LFS on a separate branch.

1. Check `UnityGame/Assets/Configs/assets_manifest.csv` for required files.
2. Place the asset at the manifest path under `UnityGame/Assets`.
3. Import using Unity's recommended settings for the asset type.
4. Add the asset to the appropriate Addressables group.
5. Commit using Git LFS or exclude from commits when working on the main branch.

Keep `.keep` files intact for empty placeholder folders.

