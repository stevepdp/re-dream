using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    [SerializeField] Transform challengePuzzlePiece;
    [SerializeField] Transform challengePuzzlePieceTarget;

    [SerializeField] bool challengeRequirementsMet;
    [SerializeField] bool challengePuzzlePieceCollected;

    void Update()
    {
        ReleasePlayer();
    }

    void OnEnable()
    {
        PuzzlePieceForChallenges.OnChallengePuzzlePieceCollected += SetChallengePuzzlePieceCollected;
    }

    void OnDisable()
    {
        PuzzlePieceForChallenges.OnChallengePuzzlePieceCollected -= SetChallengePuzzlePieceCollected;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !challengeRequirementsMet)
        {
            challengeRequirementsMet = true; // immediately releasing for now so that we don't break the level
            Debug.Log("Challenge complete!");
        }
    }

    void ReleasePlayer()
    {
        if (challengeRequirementsMet && !challengePuzzlePieceCollected)
        {
            challengePuzzlePiece.position = challengePuzzlePieceTarget.position;
            challengePuzzlePiece.parent = challengePuzzlePieceTarget;
        }
    }

    void SetChallengePuzzlePieceCollected() => challengePuzzlePieceCollected = true;
}
