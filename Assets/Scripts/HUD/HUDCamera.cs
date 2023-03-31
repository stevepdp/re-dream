using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDCamera : MonoBehaviour
{
    public static event Action OnToggleHUD;

    Camera canvasCam;
    InputAction toggleHUD;
    PlayerControls playerControls;

    void Awake()
    {
        canvasCam = GetComponent<Camera>();
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
        if (canvasCam != null)
            canvasCam.enabled = !canvasCam.enabled;
    }
}
