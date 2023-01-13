using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerHasPuzzlePiece())
            GameManager.instance.GameReset(); 
        else
            Debug.Log("You're missing this room's puzzle piece");
    }

    private bool PlayerHasPuzzlePiece() => GameManager.instance.PlayerPuzzlePieces == 1 ? true : false;
}
