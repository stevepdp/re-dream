using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHUD : MonoBehaviour
{
    public static event Action OnPlayerToggleHUD;

    PlayerControls playerControls;
    InputAction toggleHUD;

    void Awake()
    {
        playerControls = new PlayerControls();
        toggleHUD = playerControls.Player.ToggleHUD;
    }

    void OnEnable() => toggleHUD.performed += ToggleHUD;
    
    void OnDisable() => toggleHUD.performed -= ToggleHUD;

    void ToggleHUD(InputAction.CallbackContext context) => OnPlayerToggleHUD?.Invoke();
}
