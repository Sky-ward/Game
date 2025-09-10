using UnityEngine;

public class SaveLoadUI : MonoBehaviour
{
    public void SaveGame()
    {
        SaveSystem.Save();
    }

    public void LoadGame()
    {
        SaveSystem.Load();
    }
}
