using TMPro;
using UnityEngine;

public class HUDCrystals : MonoBehaviour
{
    TMP_Text hudCrystalCountText;

    void Awake()
    {
        hudCrystalCountText = GetComponent<TMP_Text>();        
    }

    void Start()
    {
        if (hudCrystalCountText != null)
            SetCrystalCountText();
    }

    void OnEnable()
    {
        GameManager.OnRoomCrystalsCounted += SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated += SetCrystalCountText;
    }

    void OnDisable()
    {
        GameManager.OnRoomCrystalsCounted -= SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated -= SetCrystalCountText;
    }

    void SetCrystalCountText()
    {
        if (hudCrystalCountText != null)
            hudCrystalCountText.text = $"<size=150%>{GameManager.instance?.PlayerCrystalsCount}</size>/<size=75%>{GameManager.instance?.RoomCrystalsTotal}</size>";
    }
}
