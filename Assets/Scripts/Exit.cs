using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] TMP_Text roomRequirementsText;
    [SerializeField] private float exitWaitTime;

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerHasPuzzlePiece())
        {
            roomRequirementsText.text = "Dream Solved!";
            Invoke("GameReset", exitWaitTime);
        }
        else
        {
            roomRequirementsText.text = "Puzzle Pieces: [0/1]";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (PlayerHasPuzzlePiece())
            roomRequirementsText.text = "Nice!";
        else
            roomRequirementsText.text = "Exit";
    }

    private void GameReset() => GameManager.instance.GameReset();

    private bool PlayerHasPuzzlePiece() => GameManager.instance.PlayerPuzzlePieces == 1 ? true : false;

    
}
