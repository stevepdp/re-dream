using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    [SerializeField] Transform challengePuzzlePiece;
    [SerializeField] Transform challengePuzzlePieceTarget;
    [SerializeField] List <GameObject> challengeWalls;

    [SerializeField] bool challengePuzzlePieceReleased;
    [SerializeField] bool challengeRequirementsMet;
    [SerializeField] bool challengePuzzlePieceCollected;
    [SerializeField] float challengeStartDelay;

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
            Invoke("StartChallenge", challengeStartDelay);
        }
    }

    void EndChallenge()
    {
        challengeRequirementsMet = true;
        if (challengeRequirementsMet && !challengePuzzlePieceReleased && !challengePuzzlePieceCollected)
        {
            Debug.Log("Challenge requirements met!");
            challengePuzzlePiece.position = challengePuzzlePieceTarget.position;
            challengePuzzlePiece.parent = challengePuzzlePieceTarget;
            challengePuzzlePieceReleased = true;
        }
    }

    void SetChallengePuzzlePieceCollected()
    {
        Debug.Log("Puzzle piece collected!");
        challengePuzzlePieceCollected = true;
        WallsDown();
    }

    void StartChallenge()
    {
        WallsUp();

        // For the purposes of dev/testing, invoke immediate release
        Invoke("EndChallenge", 2f);
    }

    void WallsDown()
    {
        if (challengeRequirementsMet && challengePuzzlePieceCollected)
        {
            Debug.Log("Walls coming down!");
            foreach (GameObject wall in challengeWalls)
            {
                wall.GetComponent<MeshCollider>().enabled = false;
                wall.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    void WallsUp()
    {
        if (challengeWalls.Count > 0 && !challengeRequirementsMet)
        {
            Debug.Log("Walls going up!");
            foreach (GameObject wall in challengeWalls)
            {
                wall.GetComponent<MeshCollider>().enabled = true;
                wall.GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
}
