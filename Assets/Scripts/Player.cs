using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerDead;
    public static event Action OnPlayerIdle;
    public static event Action OnPlayerInput;

    [SerializeField] bool canFire;
    float idleCheckTime = 6f;
    [SerializeField] bool playerHasMoved;
    [SerializeField] bool playerIdleHintShown;
    [SerializeField] int hp = 3;
    [SerializeField] InputAction fire;
    [SerializeField] ParticleSystem particleProjectile;
    [SerializeField] PlayerControls playerControls;
   
    void Awake()
    {
        canFire = true;
        playerControls = new PlayerControls();
    }

    void Start()
    {
        Invoke("CheckIdle", idleCheckTime);
    }

    void Update()
    {
        CheckNotIdle();
    }

    void OnEnable()
    {
        Challenge.OnDisableProjectile += DisableProjectile;
        Challenge.OnEnableProjectile += EnableProjectile;
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += FireProjectile;
    }

    void OnDisable()
    {
        Challenge.OnDisableProjectile -= DisableProjectile;
        Challenge.OnEnableProjectile -= EnableProjectile;
        fire.Disable();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            DeductHP();
        }
    }

    void CheckIdle()
    {
        if (!playerHasMoved && !playerIdleHintShown)
        {
            OnPlayerIdle?.Invoke();
            playerIdleHintShown = true;
        }
    }

    void CheckNotIdle()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (!playerHasMoved && Input.anyKeyDown || !playerHasMoved && horizontalInput != 0f || !playerHasMoved && verticalInput != 0f)
        {
            playerHasMoved = true;
            OnPlayerInput?.Invoke();
        }
    }

    void DeductHP() {
        hp--;
        if (hp <= 0)
            OnPlayerDead?.Invoke();
    }

    void DisableProjectile() => canFire = false;

    void EnableProjectile() => canFire = true;

    void FireProjectile(InputAction.CallbackContext context)
    {
        if (canFire) {
            particleProjectile.Play();
        }
    }
}
