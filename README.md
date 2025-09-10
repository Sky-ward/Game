# Game

**Main development lives in [`UnityGame/`](UnityGame/)** – a Unity 2022.3 LTS project using the Universal Render Pipeline (2D Renderer) with the SRP Batcher enabled.

The original Python prototype has been archived under [`_archive_python/`](_archive_python/) and is no longer maintained.

## Unity Project

To run the Unity prototype:

1. Open `UnityGame/` with Unity 2022.3.
2. Play the `Bootstrap` scene which initialises services and loads the `Hub`.

No binary assets are committed. All art, audio and third‑party files are represented by placeholders or listed in `UnityGame/Assets/Configs/assets_manifest.csv`.

See `UnityGame/Assets/Docs/Readme_tech.md` for project layout and `Docs/assets_replace_guide.md` for guidance on importing real assets.

For high‑level design notes refer to [GDD.md](GDD.md).

## Continuous Integration

The repository uses GitHub Actions for automated checks:

- `python-app.yml` runs the archived Python tests.
- `config-validate.yml` runs Unity in batch mode to execute `Game.ConfigValidation.Run` and fails when configuration data is inconsistent (missing enemy ids, invalid room types, non-monotonic progression, etc.).
  - Requires a `UNITY_LICENSE` secret to activate Unity.
  - The `unityVersion` must match the project (Unity 2022.3.x LTS).
  - The command uses `-logFile -` so logs appear in the workflow output for easier debugging.

