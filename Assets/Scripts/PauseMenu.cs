using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject canvasPause;
    [SerializeField] GameObject panelPause;

    public static event Action OnPlayerPaused;
    public static event Action OnPlayerResumed;

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            if (!panelPause.activeInHierarchy)
                ShowPanel();
        }

        if (panelPause.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ClosePanel()
    {
        OnPlayerResumed?.Invoke();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1;
        panelPause?.SetActive(false);
    }

    void ShowPanel()
    {
        OnPlayerPaused?.Invoke();

        Time.timeScale = 0;
        panelPause?.SetActive(true);
    }
}
