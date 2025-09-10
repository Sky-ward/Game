using UnityEngine;

public class ConfigHotReload : MonoBehaviour
{
#if DEVELOPMENT_BUILD
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && ConfigService.Instance != null)
            ConfigService.Instance.ReloadAll();
    }
#endif
}
