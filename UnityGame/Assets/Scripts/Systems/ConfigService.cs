using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

[CreateAssetMenu(menuName = "Configs/ConfigService")]
public class ConfigService : ScriptableObject
{
    public async Task<T> LoadConfigAsync<T>(string path)
    {
        try
        {
            var handle = Addressables.LoadAssetAsync<TextAsset>(path);
            var asset = await handle.Task;
            if (asset != null)
            {
                return JsonUtility.FromJson<T>(asset.text);
            }
        }
        catch { }

        var textAsset = Resources.Load<TextAsset>(path);
        if (textAsset != null)
        {
            return JsonUtility.FromJson<T>(textAsset.text);
        }

        Debug.LogWarning($"Config {path} not found, using defaults.");
        return default;
    }
}
