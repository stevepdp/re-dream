using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] Camera canvasCam;
    [SerializeField] Player player;
    [SerializeField] TMP_Text hudCrystalCountText;
    [SerializeField] TMP_Text hudPuzzlePieceCountText;
    [SerializeField] TMP_Text hudProjectileStatusText;

    void Start()
    {
        SetCrystalCountText();
        SetPuzzlePieceCountText();
    }

    void Update()
    {
        SetProjectileText();
    }

    void OnEnable()
    {
        GameManager.OnRoomCrystalsCounted += SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated += SetCrystalCountText;
        GameManager.OnRoomPuzzlePiecesCounted += SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated += SetPuzzlePieceCountText;
        Player.OnPlayerToggleHUD += ToggleHUD;
        Player.OnPlayerProjectileBurnout += SetProjectileBurnedOut;
        Player.OnPlayerProjectileReady += SetProjectileReady;
    }

    void OnDisable()
    {
        GameManager.OnRoomCrystalsCounted -= SetCrystalCountText;
        GameManager.OnPlayerCrystalCountUpdated -= SetCrystalCountText;
        GameManager.OnRoomPuzzlePiecesCounted -= SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated -= SetPuzzlePieceCountText;
        Player.OnPlayerToggleHUD -= ToggleHUD;
        Player.OnPlayerProjectileBurnout -= SetProjectileBurnedOut;
        Player.OnPlayerProjectileReady -= SetProjectileReady;
    }

    void SetCrystalCountText()
    {
        if (hudCrystalCountText != null)
            hudCrystalCountText.text = $"<size=150%>{GameManager.instance?.PlayerCrystalsCount}</size>/<size=75%>{GameManager.instance?.RoomCrystalsTotal}</size>";
    }

    void SetProjectileBurnedOut()
    {
        hudProjectileStatusText.text = "X";
    }

    void SetProjectileReady()
    {
        hudProjectileStatusText.text = player?.FireCooldownTime.ToString();
    }

    void SetProjectileText()
    {
        if (player != null)
        {
            if ((bool)!player?.FireBurnedOut)
                hudProjectileStatusText.text = player?.FireCooldownTime.ToString();
        }
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
