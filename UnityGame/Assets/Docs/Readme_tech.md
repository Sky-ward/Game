# Unity Technical Notes

This Unity project targets **Unity 2022.3 LTS** using the **Universal Render Pipeline (2D Renderer)**. The SRP Batcher is enabled in the pipeline asset for efficient rendering.

## Project Structure
- `Assets/AddressablesGroups` – Addressables group definitions (text only).
- `Assets/Art/Placeholders` – procedural placeholder assets. Real art should replace entries defined in `assets_manifest.csv`.
- `Assets/Audio/Placeholders` – procedural or silent audio placeholders.
- `Assets/Configs` – JSON/CSV configuration files loaded via `ConfigService`.
- `Assets/Scenes` – scene stubs for Bootstrap, Hub, three Biomes and Boss.
- `Assets/Scripts` – C# scripts organised by feature area.
- `Assets/Shaders/URP2D` – shader placeholders for URP 2D.

The repository follows a **zero binary asset policy**. Only text files and procedural placeholders are committed. Refer to `assets_replace_guide.md` for replacing placeholders with production assets.
