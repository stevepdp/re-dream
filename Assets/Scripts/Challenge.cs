using System;
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
    [SerializeField] Vector3 lastSpawnPos;

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
    [SerializeField] int challengeEnemiesDefeated;
    [SerializeField] int challengeTimeoutSeconds;
    [SerializeField] int challengeEnemiesToDefeat;
    [SerializeField] string challengeInstructions;

    public static event Action OnReducePlayerSpeed;
    public static event Action OnRestorePlayerSpeed;

    int enemiesReleased;

    void OnEnable()
    {
        EnemyChallenge.OnChallengeEnemyDefeated += IncrementKillCount;
        PuzzlePieceForChallenges.OnChallengePuzzlePieceCollected += SetChallengePuzzlePieceCollected;
    }

    void OnDisable()
    {
        EnemyChallenge.OnChallengeEnemyDefeated -= IncrementKillCount;
        PuzzlePieceForChallenges.OnChallengePuzzlePieceCollected -= SetChallengePuzzlePieceCollected;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !challengeIsOngoing && !challengeIsComplete)
        {
            Invoke("StartChallenge", challengeStartDelay);
        }
    }

    void EndChallenge()
    {
        challengeIsComplete = true;
        challengeIsOngoing = false;
        
        if (challengeIsComplete && !challengePuzzlePieceReleased && !challengePuzzlePieceCollected)
        {
            challengeInstructionsText.text = "Congratulations!";
            challengePuzzlePiece.position = challengePuzzlePieceTarget.position;
            challengePuzzlePiece.parent = challengePuzzlePieceTarget;
            challengePuzzlePieceReleased = true;
        }
    }

    void IncrementKillCount()
    {
        challengeEnemiesDefeated++;
        TestChallengeConditionsMet();
    }

    void RemoveChallengeText()
    {
        challengeInstructionsText.text = "";
        challengeInstructionsText.GetComponent<MeshRenderer>().enabled = false;
    }

    void SetChallengePuzzlePieceCollected()
    {
        Debug.Log("Puzzle piece collected!");
        challengePuzzlePieceCollected = true;
        WallsDown();
    }

    void SetChallengeText() 
    {
        challengeInstructionsText.text = challengeInstructions;
        challengeInstructionsText.GetComponent<MeshRenderer>().enabled = true;
    }

    void SpawnEnemy()
    {
        while (enemiesReleased < challengeEnemiesToDefeat)
        {
            int spawnPosChoice = UnityEngine.Random.Range(0, challengeSpawnPoints.Count);
            int enemyToSpawn = UnityEngine.Random.Range(0, enemyPrefabsToSpawn.Count);

            if (challengeSpawnPoints[spawnPosChoice].GetComponent<Transform>().position == lastSpawnPos)
            {
                // prefering not to instantiate in the same position, so loop over again
                continue;
            }

            Instantiate(enemyPrefabsToSpawn[enemyToSpawn], challengeSpawnPoints[spawnPosChoice].transform.position, Quaternion.identity);
            lastSpawnPos = challengeSpawnPoints[spawnPosChoice].transform.position;
            enemiesReleased++;
            break;
        }

        if (enemiesReleased > challengeEnemiesToDefeat)
            CancelInvoke("SpawnEnemy");
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

            case ChallengeType.SpeedReducedDefeatEnemiesNoTimer:
                StartSpeedReducedDefeatEnemiesNoTimer();
                return;

            default:
                Invoke("EndChallenge", challengeEndDelay);
                return;
        }
    }

    void StartDefeatEnemiesNoTimer()
    {
        Debug.Log(string.Format("Starting challenge: Defeat {0} Enemies. No Timer", challengeEnemiesToDefeat));
        InvokeRepeating("SpawnEnemy", challengeEnemySpawnDelay, challengeEnemySpawnDelay);
    }

    void StartSpeedReducedDefeatEnemiesNoTimer() {
        Debug.Log(string.Format("Starting challenge: Speed reduced. Defeat {0} Enemies. No Timer", challengeEnemiesToDefeat));
        OnReducePlayerSpeed?.Invoke();
        InvokeRepeating("SpawnEnemy", challengeEnemySpawnDelay, challengeEnemySpawnDelay);
    }

    void TestChallengeConditionsMet()
    {
        if (challengeType == ChallengeType.DefeatEnemiesNoTimer && challengeEnemiesDefeated >= challengeEnemiesToDefeat)
        {
            Invoke("EndChallenge", challengeEndDelay);
        }
        else if (challengeType == ChallengeType.SpeedReducedDefeatEnemiesNoTimer && challengeEnemiesDefeated >= challengeEnemiesToDefeat)
        {
            OnRestorePlayerSpeed?.Invoke();
            Invoke("EndChallenge", challengeEndDelay);
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
