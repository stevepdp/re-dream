using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : Item
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SetPuzzlePieceCollected();
            Destroy(gameObject);
        }
    }

    void SetPuzzlePieceCollected() => GameManager.instance.PlayerPuzzlePieces = 1;
}
