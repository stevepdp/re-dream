using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_CrystalCount : MonoBehaviour
{
    TMP_Text hudCrystalCountText;

    void Awake()
    {
        hudCrystalCountText = transform.GetComponent<TMP_Text>();
    }

    void Start()
    {
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
        hudCrystalCountText.text = $"<size=150%>{GameManager.instance.PlayerCrystalsCount}</size>/<size=75%>{GameManager.instance.RoomCrystalsTotal}</size>";
    }
}
