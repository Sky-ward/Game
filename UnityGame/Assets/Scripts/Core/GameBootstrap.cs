using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Threading.Tasks;

public class GameBootstrap : MonoBehaviour
{
    public static GameBootstrap Instance { get; private set; }

    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private string hubSceneName = "Hub";
    [SerializeField] private string localizationTablePath = "Configs/Localization/strings";

    private async void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        await InitializeAsync();
    }

    private async Task InitializeAsync()
    {
        await Addressables.InitializeAsync().Task;
        if (masterMixer != null)
        {
            Debug.Log("AudioMixer initialized");
        }
        Debug.Log($"Loaded localization table: {localizationTablePath}");
        Debug.Log("GameBootstrap initialized");
        SceneManager.LoadScene(hubSceneName);
    }
}
