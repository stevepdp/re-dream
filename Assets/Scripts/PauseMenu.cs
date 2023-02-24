using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject canvasPause;
    [SerializeField] GameObject panelPause;
    [SerializeField] InputAction pause;
    [SerializeField] PlayerControls playerControls;

    public static event Action OnPlayerPaused;
    public static event Action OnPlayerResumed;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        GameManager.OnGameLostFocus += ShowPanelLostFocus;
        GameManager.OnGameRefocused += ShowCursor;

        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += ShowPanel;
    }

    void OnDisable()
    {
        GameManager.OnGameLostFocus -= ShowPanelLostFocus;
        GameManager.OnGameRefocused -= ShowCursor;

        pause.Disable();
    }

    public void ClosePanel()
    {
        OnPlayerResumed?.Invoke();

        GameManager.instance.HideCursorLocked();

        Time.timeScale = 1;
        panelPause?.SetActive(false);
    }

    void Pause()
    {
        OnPlayerPaused?.Invoke();
        Time.timeScale = 0;
        panelPause?.SetActive(true);
    }

    void ShowCursor()
    {
        if (panelPause.activeInHierarchy)
        {
            // permits moving the game window when bringing it back into focus
            GameManager.instance.ShowCursorFree();
        }
        else
        {
            // in gameplay, so lock cursor center and hide it
            GameManager.instance.HideCursorLocked();
        }
            
    }

    void ShowPanel(InputAction.CallbackContext context)
    {
        if (!panelPause.activeInHierarchy)
        {
            GameManager.instance.ShowCursorConfined();
            Pause();
        }
        else
        {
            ClosePanel();
        }
    }

    void ShowPanelLostFocus()
    {
        if (!panelPause.activeInHierarchy)
        {
            GameManager.instance.ShowCursorFree();
            Pause();
        }
    }
}
