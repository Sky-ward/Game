# Game

**Main development lives in [`UnityGame/`](UnityGame/)** – a Unity 2022.3 LTS project using the Universal Render Pipeline (2D Renderer) with the SRP Batcher enabled.

The original Python prototype has been archived under [`_archive_python/`](_archive_python/) and is no longer maintained.

## Unity Project

To run the Unity prototype:

1. Open `UnityGame/` with Unity 2022.3.
2. Play the `Bootstrap` scene which initialises services and loads the `Hub`.

No binary assets are committed. All art, audio and third‑party files are represented by placeholders or listed in `UnityGame/Assets/Configs/assets_manifest.csv`.

See `UnityGame/Assets/Docs/Readme_tech.md` for project layout and `Docs/assets_replace_guide.md` for guidance on importing real assets.

For high‑level design notes refer to [GDD.md](GDD.md) (Chinese) or [GDD_en.md](GDD_en.md) (English). Both files should remain synchronized.

## CI / Build

This repository uses [GameCI](https://game.ci/) GitHub Actions:

- [`game-ci/unity-setup@v4`](https://github.com/game-ci/unity-setup)
- [`game-ci/unity-activate@v2`](https://github.com/game-ci/unity-activate)
- [`game-ci/unity-builder@v4`](https://github.com/game-ci/unity-builder)

To build or validate the Unity project:

1. Configure `UNITY_LICENSE` under *Settings → Secrets → Actions* (paste the ULF license content). Required for `unity-build.yml`, optional for `config-validate.yml`.
2. The Unity version is read from `UnityGame/ProjectSettings/ProjectVersion.txt`.
3. Workflows `config-validate.yml` and `unity-build.yml` run on pull requests and pushes.

Only `unity-build.yml` requires the `UNITY_LICENSE` secret. `config-validate.yml` should pass without it.

## Translation Workflow

Design documentation is maintained in Chinese and English:

1. Update both [GDD.md](GDD.md) and [GDD_en.md](GDD_en.md) in the same commit.
2. Translation tools or AI (e.g. DeepL, ChatGPT) may be used for a first pass.
3. Manually review and polish the translation to ensure accuracy.
4. Keep headings, numbering, and structure consistent across languages.

