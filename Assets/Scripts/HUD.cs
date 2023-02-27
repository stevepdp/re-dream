using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] Camera canvasCam;
    [SerializeField] TMP_Text hudCrystalCountText;
    [SerializeField] TMP_Text hudPuzzlePieceCountText;

    void Start()
    {
        SetCrystalCountText();
        SetPuzzlePieceCountText();
    }

    void OnEnable()
    {
        GameManager.OnRoomCrystalsCounted += SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated += SetCrystalCountText;
        GameManager.OnRoomPuzzlePiecesCounted += SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated += SetPuzzlePieceCountText;
        Player.OnPlayerToggleHUD += ToggleHUD;
    }

    void OnDisable()
    {
        GameManager.OnRoomCrystalsCounted -= SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated -= SetCrystalCountText;
        GameManager.OnRoomPuzzlePiecesCounted -= SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated -= SetPuzzlePieceCountText;
        Player.OnPlayerToggleHUD -= ToggleHUD;
    }

    void SetCrystalCountText()
    {
        if (hudCrystalCountText != null)
            hudCrystalCountText.text = $"<size=150%>{GameManager.instance?.PlayerCrystalsCount}</size>/<size=75%>{GameManager.instance?.RoomCrystalsTotal}</size>";
    }

    void SetPuzzlePieceCountText()
    {
        if (hudPuzzlePieceCountText != null)
            hudPuzzlePieceCountText.text = $"<size=150%>{GameManager.instance?.PlayerPuzzlePiecesCount}</size>/<size=75%>{GameManager.instance?.RoomPuzzlePiecesTotal}*</size>";
    }

    void ToggleHUD()
    {
        if (canvasCam != null)
            canvasCam.enabled = !canvasCam.enabled;
    }
}
