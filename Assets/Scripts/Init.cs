using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    void Start()
    {
        CleanupObjects();
        NextScene();
    }
    void OnApplicationFocus()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CleanupObjects()
    {
        GameObject[] oldGameManager = GameObject.FindGameObjectsWithTag("GameController");

        if (oldGameManager.Length > 0)
            Destroy(oldGameManager[0]);

        Instantiate(gameManager);
    }

    void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
}