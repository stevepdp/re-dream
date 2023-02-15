using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ChallengeType
{
    DefeatEnemiesNoTimer,
    EndlessEnemiesSurviveTimer,
    SpeedReducedDefeatEnemiesNoTimer,
    //JumpDisabledSurviveTimer,
    //WeaponDisabledSurviveTimer,
    //ContinuousEnemiesSurviveTimer,
    //ContinuousEnemiesSpeedReducedSurviveTimer,
    //ContinuousEnemiesJumpDisabledSurviveTimer
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

    bool challengeIsOngoing;
    bool challengeIsComplete;
    bool challengePuzzlePieceReleased;
    bool challengePuzzlePieceCollected;
    [SerializeField] bool challengeShowStats;
    [SerializeField] bool useDeveloperTime;
    [SerializeField] float challengeEnemySpawnDelay;
    [SerializeField] float challengeEndDelay;
    [SerializeField] float challengeStartDelay;
    [SerializeField] int challengeLengthInSecs;
    int challengeTimeRemaining;
    [SerializeField] int challengeEnemiesDefeated;
    [SerializeField] int challengeEnemiesToDefeat;
    [SerializeField] string challengeInstructions;

    public static event Action OnChallengeEnemyAutokill;
    public static event Action OnDisableWeapon;
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

    IEnumerator CountdownTimer()
    {
#if UNITY_EDITOR
        if (useDeveloperTime) challengeTimeRemaining = 10;
#endif
        while (challengeTimeRemaining > 0)
        {
            challengeInstructionsText.text = string.Format("Survive: {0}", challengeTimeRemaining);
            challengeTimeRemaining -= 1;
            yield return new WaitForSeconds(1f);
        }

        EndChallenge();
    }

    void EndChallenge()
    {
        challengeIsComplete = true;
        challengeIsOngoing = false;

        OnChallengeEnemyAutokill?.Invoke();

        if (challengeIsComplete && !challengePuzzlePieceReleased && !challengePuzzlePieceCollected)
        {
            challengeInstructionsText.text = "Congratulations!";

            if (challengeShowStats)
            {
                challengeInstructionsText.text += "<br>";
                challengeInstructionsText.text += string.Format("<size=50%>{0} enemies defeated!</size>", challengeEnemiesDefeated);
            }
            challengePuzzlePiece.position = challengePuzzlePieceTarget.position;
            challengePuzzlePiece.parent = challengePuzzlePieceTarget;
            challengePuzzlePieceReleased = true;
        }
    }

    void IncrementKillCount()
    {
        if (challengeIsOngoing)
        {      
            challengeEnemiesDefeated++;
            TestChallengeConditionsMet();
        }
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

    void KeepSpawningEnemies()
    {
        while (challengeTimeRemaining >= 0)
        {
            int spawnPosChoice = UnityEngine.Random.Range(0, challengeSpawnPoints.Count);
            int enemyToSpawn = UnityEngine.Random.Range(0, enemyPrefabsToSpawn.Count);

            if (challengeSpawnPoints[spawnPosChoice].GetComponent<Transform>().position == lastSpawnPos)
                continue;

            Instantiate(enemyPrefabsToSpawn[enemyToSpawn], challengeSpawnPoints[spawnPosChoice].transform.position, Quaternion.identity);
            lastSpawnPos = challengeSpawnPoints[spawnPosChoice].transform.position;
            break;
        }

        if (challengeTimeRemaining <= 0)
            CancelInvoke("KeepSpawningEnemies");
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

            case ChallengeType.EndlessEnemiesSurviveTimer:
                StartEndlessEnemiesSurviveTimer();
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

    void StartEndlessEnemiesSurviveTimer()
    {
        challengeTimeRemaining = challengeLengthInSecs;
        Debug.Log(string.Format("Starting challenge: Endless enemies. Survive {0} seconds.", challengeTimeRemaining));
        StartCoroutine(CountdownTimer());
        InvokeRepeating("KeepSpawningEnemies", challengeEnemySpawnDelay, challengeEnemySpawnDelay);
    }

    void StartSpeedReducedDefeatEnemiesNoTimer()
    {
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
