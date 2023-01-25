using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static event Action OnPlayerCrystalCountUpdated;
    public static event Action OnRoomRequirementsMet;
    public static event Action OnRoomRequirementsNotMet;
    public static event Action OnRoomCrystalsCounted;

    [SerializeField] private float exitWaitTime = 1.5f;
    [SerializeField] private int playerPuzzlePieces = 0;
    [SerializeField] private int playerCrystalsCount = 0;
    [SerializeField] private int roomCrystalsTotal = 0;

    public int PlayerCrystalsCount
    {
        get { return playerCrystalsCount; }
        set { playerCrystalsCount = value; }
    }

    public int PlayerPuzzlePieces
    {
        get { return playerPuzzlePieces; }
        set { playerPuzzlePieces = value; }
    }

    public int RoomCrystalsTotal 
    {
        get { return roomCrystalsTotal; }
        set { roomCrystalsTotal = value; }
    }

    void Awake()
    {
        EnforceSingleInstance();
        LevelSetup();
    }

    void OnEnable()
    {
        Crystal.OnCrystalCollected += IncrementPlayerCrystalCount;
        Exit.OnPlayerEnteredExit += TestRoomRequirementsMet;
        PuzzlePiece.OnPuzzlePieceCollected += IncrementPuzzlePiece;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        Crystal.OnCrystalCollected -= IncrementPlayerCrystalCount;
        Exit.OnPlayerEnteredExit -= TestRoomRequirementsMet;
        PuzzlePiece.OnPuzzlePieceCollected -= IncrementPuzzlePiece;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void CountRoomCrystals()
    {
        Crystal[] crystals = FindObjectsOfType<Crystal>();
        roomCrystalsTotal = crystals.Length;
        OnRoomCrystalsCounted?.Invoke();
    }

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void IncrementPlayerCrystalCount()
    {
        playerCrystalsCount += 1;
        OnPlayerCrystalCountUpdated?.Invoke();
    }

    void IncrementPuzzlePiece() {
        playerPuzzlePieces += 1;
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LevelSetup()
    {
        roomCrystalsTotal = 0;
        playerCrystalsCount = 0;
        playerPuzzlePieces = 0;
        OnPlayerCrystalCountUpdated?.Invoke();
        CountRoomCrystals();
    }

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelSetup();
    }

    void TestRoomRequirementsMet()
    {
        if (playerPuzzlePieces == 1)
        {
            OnRoomRequirementsMet?.Invoke();
            Invoke("NextLevel", exitWaitTime);
        }
        else
        {
            OnRoomRequirementsNotMet?.Invoke();
        }
    }
}
