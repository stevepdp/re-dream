using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ChallengeType
{
    DefeatEnemiesNoTimer,
    SpeedReducedDefeatEnemiesNoTimer,
    JumpDisabledSurviveTimer,
    WeaponDisabledSurviveTimer,
    ContinuousEnemiesSurviveTimer,
    ContinuousEnemiesSpeedReducedSurviveTimer,
    ContinuousEnemiesJumpDisabledSurviveTimer
}

public class Challenge : MonoBehaviour
{
    [SerializeField] ChallengeType challengeType;

    [SerializeField] Transform challengePuzzlePiece;
    [SerializeField] Transform challengePuzzlePieceTarget;
    [SerializeField] List<GameObject> challengeSpawnPoints;
    [SerializeField] List<GameObject> challengeWalls;
    [SerializeField] List<GameObject> enemyPrefabsToSpawn;
    [SerializeField] TMP_Text challengeInstructionsText;

    [SerializeField] bool challengeIsOngoing;
    [SerializeField] bool challengeIsComplete;
    [SerializeField] bool challengePuzzlePieceReleased;
    [SerializeField] bool challengePuzzlePieceCollected;
    [SerializeField] float challengeEnemySpawnDelay;
    [SerializeField] float challengeEndDelay;
    [SerializeField] float challengeStartDelay;
    [SerializeField] int challengeTimeoutSeconds;
    [SerializeField] int enemiesDefeated;
    [SerializeField] string challengeInstructions;

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
        if (other.gameObject.CompareTag("Player") && !challengeIsOngoing && !challengeIsComplete)
        {
            Invoke("StartChallenge", challengeStartDelay);
        }
    }

    void StartDefeatEnemiesNoTimer()
    {
        // For the purposes of dev/testing, invoke immediate release
        Invoke("EndChallenge", challengeEndDelay);
    }

    void EndChallenge()
    {
        challengeIsComplete = true;
        if (challengeIsComplete && !challengePuzzlePieceReleased && !challengePuzzlePieceCollected)
        {
            challengeInstructionsText.text = "Congratulations!";
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

    void RemoveChallengeText()
    {
        challengeInstructionsText.text = "";
        challengeInstructionsText.GetComponent<MeshRenderer>().enabled = false;
    }

    void SetChallengeText() 
    {
        challengeInstructionsText.text = challengeInstructions;
        challengeInstructionsText.GetComponent<MeshRenderer>().enabled = true;
    }

    void StartChallenge()
    {
        challengeIsOngoing = true;
        WallsUp();

        switch (challengeType)
        {
            case ChallengeType.DefeatEnemiesNoTimer:
                StartDefeatEnemiesNoTimer();
                return;

            default:
                Invoke("EndChallenge", challengeEndDelay);
                return;
        }
    }

    void WallsDown()
    {
        if (challengeIsComplete && challengePuzzlePieceCollected)
        {
            Debug.Log("Walls coming down!");
            foreach (GameObject wall in challengeWalls)
            {
                wall.GetComponent<MeshCollider>().enabled = false;
                wall.GetComponent<MeshRenderer>().enabled = false;
            }

            RemoveChallengeText();
        }
    }

    void WallsUp()
    {
        if (challengeWalls.Count > 0 && !challengeIsComplete)
        {
            Debug.Log("Walls going up!");
            foreach (GameObject wall in challengeWalls)
            {
                wall.GetComponent<MeshCollider>().enabled = true;
                wall.GetComponent<MeshRenderer>().enabled = true;
            }

            SetChallengeText();
        }
    }
}
