using System;
using UnityEngine;

public class PuzzlePieceForChallenges : Item
{
    public static event Action<PuzzlePieceForChallenges> OnPuzzlePieceCollected;

    [SerializeField] Challenge challenge;
    [SerializeField] int storyId;

    public int StoryId
    {
        get { return storyId; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnPuzzlePieceCollected?.Invoke(this);
            challenge?.SetChallengePuzzlePieceCollected();
        }
    }
}
