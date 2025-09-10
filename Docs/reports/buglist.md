| id | severity | scene | steps | expected | actual | logs | status |
|----|----------|-------|-------|----------|--------|------|--------|
| BL-001 | P1 | N/A | Run `unity-editor -batchmode -projectPath UnityGame -executeMethod ConfigValidation.Run` | Config validation completes | `unity-editor` command not found | `Docs/reports/config_validate.log` | Open |
| BL-002 | P2 | N/A | Call `PlaceholderFactory.CreatePlaceholderSprite()` repeatedly | Objects should reuse textures/materials | Each call allocates new `Texture2D` and `Material` leading to GC pressure | n/a | Open |
