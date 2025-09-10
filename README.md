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

The repository uses GitHub Actions for automated checks. Both Unity workflows rely on `game-ci/unity-actions/setup@v2` and `game-ci/unity-actions/activate@v2`; omitting the sub‑action (e.g. `game-ci/unity-actions@v2`) will fail with *unable to find version v2*.

- `python-app.yml` runs the archived Python tests.
- `config-validate.yml` runs Unity in batch mode to execute `Game.ConfigValidation.Run` and fails when configuration data is inconsistent (missing enemy ids, invalid room types, non-monotonic progression, etc.).

  - Requires a `UNITY_LICENSE` secret to activate Unity. If the secret is missing the "Activate Unity" step fails and the logs will note that the license key was not supplied.
  - The `unityVersion` must match the project (Unity 2022.3.x LTS).
  - Logs are printed to the workflow using `-logFile -` for easier debugging.
  - Common failures: missing license, incorrect Unity version or invalid config data.
  - To run locally:

    ```bash
    unity-editor -batchmode -projectPath UnityGame -executeMethod Game.ConfigValidation.Run -quit -nographics -logFile -
    ```

- `unity-build.yml` produces a downloadable Windows x86_64 build via `game-ci/unity-builder`.

  - Requires `UNITY_LICENSE` like the validate job.
  - Built artifacts are available from the workflow run page.
  - Failures often relate to license activation or missing build support modules.


