# Configs

All gameplay tuning data lives here. Each JSON includes a top-level `version` number for schema tracking.

## Files

- `Enemies/enemies.json`: enemy stats.
- `Items/items.json`: item definitions.
- `Levels/room_archetypes.json`: room type weights and options per biome.
- `Levels/wave_configs.json`: enemy wave compositions per biome.
- `Progression/progression.json`: player level XP curve.

## Conventions

- `id`: unique string identifier.
- `weight`, `count`, `hp`, `xp`, `value`: positive numbers.
- `hidden_room_chance`: 0–1.
- Room `type` values: `Combat`, `Elite`, `Reward`, `Shop`, `Rest`, `Challenge`.

## Minimal Example

```json
{"version":1,"enemies":[{"id":"grunt","hp":10,"speed":1.0,"attack":1,"ai":"melee"}]}
```
