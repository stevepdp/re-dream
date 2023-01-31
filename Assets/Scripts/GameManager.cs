using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static event Action OnPlayerCrystalCountUpdated;
    public static event Action OnPlayerPuzzlePiecesCountUpdated;
    public static event Action OnRoomRequirementsMet;
    public static event Action OnRoomRequirementsNotMet;
    public static event Action OnRoomCrystalsCounted;
    public static event Action OnRoomPuzzlePiecesCounted;

    [SerializeField] private float exitWaitTime = 1.5f;
    [SerializeField] private int playerPuzzlePiecesCount = 0;
    [SerializeField] private int playerCrystalsCount = 0;
    [SerializeField] private int roomPuzzlePiecesTotal = 0;
    [SerializeField] private int roomCrystalsTotal = 0;

    public int PlayerCrystalsCount
    {
        get { return playerCrystalsCount; }
        set { playerCrystalsCount = value; }
    }

    public int PlayerPuzzlePiecesCount
    {
        get { return playerPuzzlePiecesCount; }
        set { playerPuzzlePiecesCount = value; }
    }

    public int RoomCrystalsTotal 
    {
        get { return roomCrystalsTotal; }
        set { roomCrystalsTotal = value; }
    }

    public int RoomPuzzlePiecesTotal
    {
        get { return roomPuzzlePiecesTotal; }
        set { roomPuzzlePiecesTotal = value; }
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
        Player.OnPlayerDead += LevelReset;
        PuzzlePiece.OnPuzzlePieceCollected += IncrementPuzzlePiece;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        Crystal.OnCrystalCollected -= IncrementPlayerCrystalCount;
        Exit.OnPlayerEnteredExit -= TestRoomRequirementsMet;
        Player.OnPlayerDead -= LevelReset;
        PuzzlePiece.OnPuzzlePieceCollected -= IncrementPuzzlePiece;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void CountRoomPuzzlePieces()
    {
        PuzzlePiece[] puzzlePieces = FindObjectsOfType<PuzzlePiece>();
        roomPuzzlePiecesTotal = puzzlePieces.Length;
        OnRoomPuzzlePiecesCounted?.Invoke();
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
        playerPuzzlePiecesCount += 1;
        OnPlayerPuzzlePiecesCountUpdated?.Invoke();
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LevelSetup()
    {
        roomCrystalsTotal = 0;
        roomPuzzlePiecesTotal = 0;
        playerCrystalsCount = 0;
        playerPuzzlePiecesCount = 0;
        OnPlayerCrystalCountUpdated?.Invoke();
        OnPlayerPuzzlePiecesCountUpdated?.Invoke();
        CountRoomCrystals();
        CountRoomPuzzlePieces();
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
        if (playerPuzzlePiecesCount == roomPuzzlePiecesTotal)
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
