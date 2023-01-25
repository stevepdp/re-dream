using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD_PuzzleCount : MonoBehaviour
{
    TMP_Text hudPuzzlePieceCountText;

    void Awake()
    {
        hudPuzzlePieceCountText = transform.GetComponent<TMP_Text>();
    }

    void Start()
    {
        SetPuzzlePieceCountText();
    }

    void OnEnable()
    {
        GameManager.OnPlayerCrystalCountUpdated += SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated += SetPuzzlePieceCountText;
        GameManager.OnRoomCrystalsCounted += SetPuzzlePieceCountText;
        GameManager.OnRoomPuzzlePiecesCounted += SetPuzzlePieceCountText;
    }

    void OnDisable()
    {
        GameManager.OnPlayerCrystalCountUpdated -= SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated -= SetPuzzlePieceCountText;
        GameManager.OnRoomCrystalsCounted -= SetPuzzlePieceCountText;
        GameManager.OnRoomPuzzlePiecesCounted -= SetPuzzlePieceCountText;
    }

    void SetPuzzlePieceCountText()
    {
        hudPuzzlePieceCountText.text = $"<size=150%>{GameManager.instance.PlayerPuzzlePiecesCount}</size>/<size=75%>{GameManager.instance.RoomPuzzlePiecesTotal}*</size>";
    }
}
