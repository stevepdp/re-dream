using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceForChallenges : Item
{
    public static event Action OnChallengePuzzlePieceCollected;
    public static event Action OnPuzzlePieceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPuzzlePieceCollected?.Invoke();
            OnChallengePuzzlePieceCollected?.Invoke();
        }
    }
}
