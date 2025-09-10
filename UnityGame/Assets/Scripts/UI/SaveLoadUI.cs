using System;
using UnityEngine;
using Game.Save;

public class SaveLoadUI : MonoBehaviour
{
    private int selectedSlot;

    private void Start()
    {
        RefreshSlots();
    }

    public void SelectSlot(int slot)
    {
        selectedSlot = slot;
    }

    public void RefreshSlots()
    {
        foreach (var info in SaveSystem.GetAllSlots())
        {
            var time = DateTimeOffset.FromUnixTimeSeconds(info.timestamp).ToLocalTime();
            Debug.Log($"Slot {info.slot}: version {info.version} saved {time:u}");
        }
    }

    public void SaveGame()
    {
        SaveSystem.Save(selectedSlot);
        RefreshSlots();
    }

    public void LoadGame()
    {
        SaveSystem.Load(selectedSlot);
    }
}
