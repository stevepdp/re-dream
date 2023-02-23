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
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
        GameObject[] oldGameObject = GameObject.FindGameObjectsWithTag("GameController");

        if (oldGameObject?.Length > 0)
            Destroy(oldGameObject[0]);

        oldGameObject = GameObject.FindGameObjectsWithTag("InputManager");

        if (oldGameObject?.Length > 0)
            Destroy(oldGameObject[0]);

        Instantiate(gameManager);
        Instantiate(inputManager);
    }

    void NextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
}