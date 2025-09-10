using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && inventoryPanel != null)
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
