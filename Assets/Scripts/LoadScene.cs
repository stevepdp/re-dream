using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void LoadNextScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

    public static void LoadSceneByName(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
}