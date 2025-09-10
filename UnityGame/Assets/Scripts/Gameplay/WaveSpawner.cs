using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    private int _alive;
    private int _kills;

    public IEnumerator SpawnRoom(string biome, GeneratedRoom room)
    {
        var wave = ConfigService.Instance.GetWave(biome, room.waveId);
        if (wave == null)
            yield break;
        foreach (var spawn in wave)
        {
            for (int i = 0; i < spawn.count; i++)
            {
                var enemyCfg = ConfigService.Instance.GetEnemy(spawn.enemy);
                if (enemyCfg == null)
                {
                    Log.Warn($"Missing enemy '{spawn.enemy}' in wave {room.waveId}");
                    continue;
                }
                SpawnEnemy(enemyCfg);
                yield return new WaitForSeconds(spawn.spawn_interval);
            }
        }
        while (_alive > 0)
            yield return null;
    }

    private void SpawnEnemy(EnemyConfig cfg)
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        go.name = cfg.id;
        var enemy = go.AddComponent<EnemyPlaceholder>();
        enemy.Init(cfg.hp, () => { _alive--; _kills++; });
        _alive++;
    }

    public int Kills => _kills;
}
