using TMPro;
using UnityEngine;

public class HUDPuzzlePieces : MonoBehaviour
{
    TMP_Text hudPuzzlePieceCountText;

    void Awake()
    {
        hudPuzzlePieceCountText = GetComponent<TMP_Text>();
    }

    void Start()
    {
        SetPuzzlePieceCountText();
    }

    void OnEnable()
    {
        GameManager.OnRoomPuzzlePiecesCounted += SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated += SetPuzzlePieceCountText;
    }

    void OnDisable()
    {
        GameManager.OnRoomPuzzlePiecesCounted -= SetPuzzlePieceCountText;
        GameManager.OnPlayerPuzzlePiecesCountUpdated -= SetPuzzlePieceCountText;
    }

    void SetPuzzlePieceCountText()
    {
        if (hudPuzzlePieceCountText != null)
            hudPuzzlePieceCountText.text = $"<size=150%>{GameManager.instance?.PlayerPuzzlePiecesCount}</size>/<size=75%>{GameManager.instance?.RoomPuzzlePiecesTotal}*</size>";
    }
}
