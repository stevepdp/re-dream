using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private int playerPuzzlePieces = 0;

    public int PlayerPuzzlePieces
    {
        get { return playerPuzzlePieces; }
        set { playerPuzzlePieces = value; }
    }

    void Awake()
    {
        EnforceSingleInstance();
        GameSetup();
    }

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

   
    public void GameReset()
    {
        playerPuzzlePieces = 0;
        GameRestart();
    }

    public void GameRestart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    void GameSetup() => playerPuzzlePieces = 0;
}
