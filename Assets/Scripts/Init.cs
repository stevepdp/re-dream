using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    [SerializeField] GameObject gameManager;
    [SerializeField] GameObject inputManager;

    void Start()
    {
        CleanupObjects();
        LoadScene.LoadNextScene();
        Time.timeScale = 1;
    }

    void CleanupObjects()
    {
        GameObject[] oldGameObject = GameObject.FindGameObjectsWithTag("GameController");

        if (oldGameObject?.Length > 0)
            Destroy(oldGameObject[0]);

        oldGameObject = GameObject.FindGameObjectsWithTag("InputManager");

        if (oldGameObject?.Length > 0)
            Destroy(oldGameObject[0]);

        Instantiate(gameManager);
        Instantiate(inputManager);
    }
}