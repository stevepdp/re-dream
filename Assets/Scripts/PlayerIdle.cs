using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdle : MonoBehaviour
{
    public static event Action OnPlayerIdle;
    public static event Action OnPlayerInput;

    InputAction movement;
    PlayerControls playerControls;

    [SerializeField] float idleCheckTime = 6f;
    bool playerHasMoved;
    bool playerIdleHintShown;

    void Awake()
    {
        playerControls = new PlayerControls();

        movement = playerControls.Player.Move;
        movement?.Enable();
    }

    void Start()
    {
        Invoke("CheckIdle", idleCheckTime);
    }

    void OnEnable()
    {
        movement.performed += CheckNotIdle;
    }

    void OnDisable()
    {
        movement.performed -= CheckNotIdle;
        movement?.Disable();
    }

    void CheckIdle()
    {
        if (!playerHasMoved && !playerIdleHintShown)
        {
            OnPlayerIdle?.Invoke();
            playerIdleHintShown = true;
        }
    }

    void CheckNotIdle(InputAction.CallbackContext ctx)
    {
        Vector2 moveAxis = ctx.ReadValue<Vector2>();

        if (!playerHasMoved && Input.anyKeyDown || !playerHasMoved && moveAxis.x != 0f || !playerHasMoved && moveAxis.y != 0f)
        {
            playerHasMoved = true;
            OnPlayerInput?.Invoke();
            movement?.Disable();
        }
    }
}
