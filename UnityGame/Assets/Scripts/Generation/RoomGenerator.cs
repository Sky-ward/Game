using System;
using System.IO;
using System.Text.Json;
using UnityEngine;

namespace Generation
{
    [Serializable]
    public class RoomTemplate
    {
        public string id;
        public string[] tiles;
    }

    [Serializable]
    public class RoomTemplatesConfig
    {
        public RoomTemplate[] rooms;
    }

    [CreateAssetMenu(menuName = "Configs/RoomTemplates")]
    public class RoomTemplatesScriptable : ScriptableObject
    {
        public RoomTemplate[] rooms;
    }

    /// <summary>
    /// Generates a simple room layout from a JSON configuration.
    /// Uses Unity cube primitives as placeholder tiles to visualise the room.
    /// </summary>
    public class RoomGenerator : MonoBehaviour
    {
        [Tooltip("Relative path under Assets to room template json")]
        public string configPath = "Configs/Rooms/room_templates.json";

        private void Start()
        {
            var path = Path.Combine(Application.dataPath, configPath);
            if (!File.Exists(path))
            {
                Debug.LogError($"Room config not found: {path}");
                return;
            }

            var cfg = JsonSerializer.Deserialize<RoomTemplatesConfig>(File.ReadAllText(path));
            if (cfg == null || cfg.rooms == null || cfg.rooms.Length == 0)
            {
                Debug.LogError("No rooms defined in config");
                return;
            }

            // For prototype purposes just generate the first template
            Generate(cfg.rooms[0]);
        }

        /// <summary>
        /// Spawn tiles based on template data.
        /// </summary>
        public void Generate(RoomTemplate template)
        {
            for (int y = 0; y < template.tiles.Length; y++)
            {
                var line = template.tiles[y];
                for (int x = 0; x < line.Length; x++)
                {
                    var ch = line[x];
                    var go = CreateTile(ch);
                    if (go != null)
                        go.transform.position = new Vector3(x, 0, -y);
                }
            }
        }

        private GameObject CreateTile(char c)
        {
            // Using cube primitives with colours as placeholder prefabs
            switch (c)
            {
                case '#': return CreatePrimitive(Color.gray); // wall
                case '.': return CreatePrimitive(Color.white); // floor
                case 'S': return CreatePrimitive(Color.green); // start
                case 'E': return CreatePrimitive(Color.red);   // exit
                default: return null;
            }
        }

        private GameObject CreatePrimitive(Color colour)
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            var renderer = go.GetComponent<Renderer>();
            if (renderer != null)
                renderer.sharedMaterial.color = colour;
            return go;
        }

        /// <summary>
        /// Helper used by unit tests to ensure a template has exactly one start and exit tile.
        /// </summary>
        public static bool HasSingleStartAndExit(RoomTemplate template)
        {
            int s = 0, e = 0;
            foreach (var line in template.tiles)
            {
                foreach (var ch in line)
                {
                    if (ch == 'S') s++;
                    else if (ch == 'E') e++;
                }
            }
            return s == 1 && e == 1;
        }
    }
}
