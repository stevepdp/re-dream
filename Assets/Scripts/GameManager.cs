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
        LevelSetup();
    }

    void EnforceSingleInstance()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

   
    public void LevelReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelSetup();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        LevelSetup();
    }

    void LevelSetup() => playerPuzzlePieces = 0;
}
