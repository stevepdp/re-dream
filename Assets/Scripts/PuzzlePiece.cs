using System;
using UnityEngine;

public class PuzzlePiece : Item
{
    public static event Action<PuzzlePiece> OnPuzzlePieceCollected;

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
        }
    }
}
