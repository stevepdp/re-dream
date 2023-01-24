using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public static event Action OnRoomRequirementsMet;
    public static event Action OnRoomRequirementsNotMet;
    
    private float exitWaitTime = 1.5f;
    [SerializeField] private int playerPuzzlePieces = 0;

    public int PlayerPuzzlePieces
    {
        get { return playerPuzzlePieces; }
        set { playerPuzzlePieces = value; }
    }

    void Awake()
    {
        EnforceSingleInstance();
        LevelSetup();
    }

    void OnEnable()
    {
        Exit.OnPlayerEnteredExit += TestRoomRequirementsMet;
        PuzzlePiece.OnPuzzlePieceCollected += IncrementPuzzlePiece;
    }

    void OnDisable()
    {
        Exit.OnPlayerEnteredExit -= TestRoomRequirementsMet;
        PuzzlePiece.OnPuzzlePieceCollected -= IncrementPuzzlePiece;
    }

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void IncrementPuzzlePiece() => playerPuzzlePieces += 1;

    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelSetup();
    }

    void LevelSetup() => playerPuzzlePieces = 0;

    void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
