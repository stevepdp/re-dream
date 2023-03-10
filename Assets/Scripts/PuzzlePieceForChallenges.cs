using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceForChallenges : Item
{
    [SerializeField] Challenge challenge;

    public static event Action OnPuzzlePieceCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPuzzlePieceCollected?.Invoke();
            challenge?.SetChallengePuzzlePieceCollected();
        }
    }
}
