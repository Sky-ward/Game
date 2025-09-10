# Asset Replacement Guide

This repository intentionally omits binary game assets such as images, audio, and 3D models. Use this guide to replace placeholder assets with production-ready content.

## Workflow
1. Place new assets under `Art/` or `Audio/` as appropriate.
2. Maintain directory structure referenced in `Configs/assets_manifest.csv`.
3. Import assets into Unity using the recommended settings below.
4. Commit only text-based metadata; **do not** commit binary files. Supply them separately via the asset delivery channel.

## Recommended Import Settings
- **Textures**: Set compression to `LZ4`, max size 2048, use mipmaps only when required.
- **Audio**: Compress to Vorbis, quality 70, load type `Streaming` for music.
- **Models**: Apply scale factor 1, generate lightmap UVs if needed.

## Placeholder Policy
If an asset is missing, the game should fall back to a neutral placeholder (e.g., grey box, silent clip). Log a warning but continue running.

