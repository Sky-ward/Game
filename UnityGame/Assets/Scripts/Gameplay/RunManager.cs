using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public string biome = "Biome_A";
    private List<GeneratedRoom> _path;
    private WaveSpawner _spawner;
    private float _runStart;
    private int _totalKills;

    private void Awake()
    {
        _spawner = GetComponent<WaveSpawner>();
    }

    private IEnumerator Start()
    {
        GeneratePath();
        _runStart = Time.time;
        foreach (var room in _path)
        {
            Debug.Log($"Room {room.type}/{room.waveId}");
            if (room.waveId != null)
            {
                yield return StartCoroutine(_spawner.SpawnRoom(biome, room));
                _totalKills += _spawner.Kills;
            }
        }
        float duration = Time.time - _runStart;
        Debug.Log($"Run complete: {duration:F1}s, kills {_totalKills}");
    }

    private void OnEnable()
    {
        ConfigService.OnConfigsReloaded += GeneratePath;
    }

    private void OnDisable()
    {
        ConfigService.OnConfigsReloaded -= GeneratePath;
    }

    private void GeneratePath()
    {
        _path = LevelPathGenerator.Generate(biome, ConfigService.Instance.RoomArchetypes);
        Debug.Log("Generated path: " + string.Join("->", _path.Select(p => p.type)));
    }
}
