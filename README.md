# Game

Minimal prototype for a side-scrolling roguelike game.

## Running

```bash
python main.py
```

## Tests

```bash
python -m pytest
```

The code includes simple `Player`, `Enemy`, `Level`, and `Weapon` classes
to serve as a starting point for further development.

For a high-level overview of planned features, see [GDD.md](GDD.md).

## Unity Project
The repository now contains a `UnityGame/` directory with a Unity 2022.3 LTS project targeting the Universal Render Pipeline (2D Renderer). No binary assets are committed; all art, audio and third-party files are represented by procedural placeholders or entries in `Assets/Configs/assets_manifest.csv`. Refer to `UnityGame/Assets/Docs/Readme_tech.md` for project layout and `assets_replace_guide.md` for bringing real assets via Git LFS or a separate branch.

To run the Unity prototype:
1. Open `UnityGame/` with Unity 2022.3.
2. Open and play the `Bootstrap` scene which will initialise the game and load the `Hub` scene.
