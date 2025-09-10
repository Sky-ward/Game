using NUnit.Framework;
using System.IO;
using System.Text.Json;

public class RoomGenerationTests
{
    [Test]
    public void TemplatesHaveStartAndExit()
    {
        var path = Path.GetFullPath(Path.Combine(TestContext.CurrentContext.TestDirectory,
            "..", "..", "..", "UnityGame", "Assets", "Configs", "Rooms", "room_templates.json"));
        var cfg = JsonSerializer.Deserialize<RoomConfig>(File.ReadAllText(path));
        foreach (var room in cfg.rooms)
        {
            int start = 0, exit = 0;
            foreach (var line in room.tiles)
                foreach (var ch in line)
                {
                    if (ch == 'S') start++;
                    else if (ch == 'E') exit++;
                }
            Assert.AreEqual(1, start, $"Room {room.id} requires one start");
            Assert.AreEqual(1, exit, $"Room {room.id} requires one exit");
        }
    }

    private class RoomConfig
    {
        public Room[] rooms { get; set; }
    }

    private class Room
    {
        public string id { get; set; }
        public string[] tiles { get; set; }
    }
}
