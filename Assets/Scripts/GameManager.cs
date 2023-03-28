using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance {
        get { 
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            if (_instance == null)
            {
                _instance = Instantiate(new GameObject("GameManager")).AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public static event Action OnGameLostFocus;
    public static event Action OnGameRefocused;
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
        HideCursorLocked();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus) OnGameRefocused?.Invoke();
        else OnGameLostFocus?.Invoke();
    }

    void OnEnable()
    {
        Crystal.OnCrystalCollected += IncrementPlayerCrystalCount;
        Exit.OnPlayerEnteredExit += TestRoomRequirementsMet;
        Player.OnPlayerDead += LevelReset;
        PuzzlePiece.OnPuzzlePieceCollected += IncrementPuzzlePiece;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected += IncrementPuzzlePiece;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        Crystal.OnCrystalCollected -= IncrementPlayerCrystalCount;
        Exit.OnPlayerEnteredExit -= TestRoomRequirementsMet;
        Player.OnPlayerDead -= LevelReset;
        PuzzlePiece.OnPuzzlePieceCollected -= IncrementPuzzlePiece;
        PuzzlePieceForChallenges.OnPuzzlePieceCollected -= IncrementPuzzlePiece;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void CountRoomPuzzlePieces()
    {
        PuzzlePiece[] puzzlePieces = FindObjectsOfType<PuzzlePiece>();
        PuzzlePieceForChallenges[] puzzlePiecesForChallenges = FindObjectsOfType<PuzzlePieceForChallenges>();
        roomPuzzlePiecesTotal = (puzzlePieces.Length + puzzlePiecesForChallenges.Length);
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
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void IncrementPlayerCrystalCount()
    {
        playerCrystalsCount += 1;
        OnPlayerCrystalCountUpdated?.Invoke();
    }

    void IncrementPuzzlePiece(UnityEngine.Object puzzlePiece) {
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

    public void HideCursorLocked()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void HideCursorConfined()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ShowCursorConfined()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ShowCursorFree()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
