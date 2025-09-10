using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class LevelPathGenerator
{
    public static List<GeneratedRoom> Generate(string biomeId, RoomArchetypesConfig config, int minRooms = 6, int maxRooms = 8)
    {
        var biome = config?.biomes?.FirstOrDefault(b => b.id == biomeId);
        var result = new List<GeneratedRoom>();
        if (biome == null)
            return result;
        var rng = new System.Random();
        int count = rng.Next(minRooms, maxRooms + 1);
        for (int i = 0; i < count; i++)
        {
            var entry = WeightedPick(biome.rooms, rng);
            var room = new GeneratedRoom { type = entry.type };
            if (entry.type == "Combat")
                room.waveId = i < count / 2 ? "Combat_Tier1" : "Combat_Tier2";
            else if (entry.type == "Elite")
                room.waveId = "Elite";
            result.Add(room);
            if (rng.NextDouble() < biome.hidden_room_chance)
                result.Add(new GeneratedRoom { type = "Challenge", waveId = "Combat_Tier2" });
        }
        return result;
    }

    private static RoomTypeEntry WeightedPick(List<RoomTypeEntry> entries, System.Random rng)
    {
        int total = entries.Sum(e => e.weight);
        int roll = rng.Next(total);
        foreach (var e in entries)
        {
            if (roll < e.weight)
                return e;
            roll -= e.weight;
        }
        return entries.Last();
    }
}

public class GeneratedRoom
{
    public string type;
    public string waveId;
}
