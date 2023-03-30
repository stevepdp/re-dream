using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDInput : MonoBehaviour
{
    public static event Action OnToggleHUD;

    PlayerControls playerControls;
    InputAction toggleHUD;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        if (playerControls != null)
        {
            toggleHUD = playerControls.Player.ToggleHUD;
            toggleHUD.Enable();
            toggleHUD.performed += ToggleHUD;
        }
    }

    void OnDisable()
    {
        toggleHUD.performed -= ToggleHUD;
        toggleHUD.Disable();
    }

    void ToggleHUD(InputAction.CallbackContext context)
    {
        OnToggleHUD?.Invoke();
    }
}
