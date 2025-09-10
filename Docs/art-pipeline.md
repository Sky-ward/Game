# Art Pipeline

## Sprite Import Settings

- **Naming**: Sprite textures must start with `spr_` and reside under `Assets/Art`.
- **Type**: Imported as *Sprite (2D and UI)*.
- **Compression**: Uses `Compressed` texture compression.
- **Mip Maps**: Disabled.

### Workflow
1. Drop the PNG into `Assets/Art` with a `spr_` prefix.
2. The `AssetPostprocessor` automatically applies import settings and warns on naming errors.
3. Verify in the Inspector that the sprite type and compression match the guidelines.

These rules ensure consistent, memory‑efficient textures across the project.
