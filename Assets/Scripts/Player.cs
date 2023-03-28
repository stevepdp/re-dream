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
    public static event Action OnPlayerProjectileBurnout;
    public static event Action OnPlayerProjectileReady;
    

    [SerializeField] bool canFire;
    float idleCheckTime = 6f;
    [SerializeField] bool playerHasMoved;
    [SerializeField] bool playerIdleHintShown;
    [SerializeField] bool fireBurnedOut;
    [SerializeField] int fireCooldownMin = 0;
    [SerializeField] int fireCooldownStep = 1;
    [SerializeField] int fireCooldownMax = 5;
    [SerializeField] int fireCooldownTime;
    [SerializeField] int hp = 3;
    [SerializeField] InputAction fire;
    [SerializeField] InputAction movement;
    [SerializeField] ParticleSystem particleProjectile;
    [SerializeField] PlayerControls playerControls;

    public bool FireBurnedOut
    {
        get { return fireBurnedOut; }
    }

    public int FireCooldownMin
    {
        get { return fireCooldownMin; }
    }

    public int FireCooldownMax
    {
        get { return fireCooldownMax; }
    }

    public int FireCooldownTime
    {
        get { return fireCooldownTime; }
        set { fireCooldownTime = value; }
    }

    void Awake()
    {
        canFire = true;
        playerControls = new PlayerControls();
    }

    void Update()
    {
        CheckEnemyCollision();
    }

    void Start()
    {
        Invoke("CheckIdle", idleCheckTime);
        InvokeRepeating("FireCooldown", fireCooldownStep, fireCooldownStep);
    }

    void OnEnable()
    {
        Challenge.OnDisableProjectile += DisableProjectile;
        Challenge.OnEnableProjectile += EnableProjectile;
        Crystal.OnCrystalCollected += ResetProjectileAndHUD;

        movement = playerControls.Player.Move;
        movement?.Enable();
        movement.performed += CheckNotIdle;

        fire = playerControls.Player.Fire;
        fire?.Enable();
        fire.performed += FireProjectile;
    }

    void OnDisable()
    {
        Challenge.OnDisableProjectile -= DisableProjectile;
        Challenge.OnEnableProjectile -= EnableProjectile;
        fire?.Disable();
    }

    void CheckEnemyCollision()
    {
        // Enemies do not move with physics, so it's neccessary to instead manually check for them
        float capsuleRadius = 0.6f;
        float centerOffset = 2f;

        Vector3 capsulePos = transform.position;
        capsulePos.y += centerOffset; // center
        
        Collider[] colliders = Physics.OverlapCapsule(capsulePos, capsulePos, capsuleRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
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

    void DeductHP() {
        hp--;
        if (hp <= 0)
            OnPlayerDead?.Invoke();
    }

    void DisableProjectile() => canFire = false;

    void EnableProjectile()
    {
        canFire = true;
    }

    void ResetProjectile()
    {
        canFire = true;
        OnPlayerProjectileReady?.Invoke();
    }

    void ResetProjectileAndHUD()
    {
        fireBurnedOut = false;
        fireCooldownTime = fireCooldownMin;
        ResetProjectile();
    }

    void FireCooldown()
    {
        if (fireCooldownTime > fireCooldownMin)
            fireCooldownTime--;

        if (fireCooldownTime >= fireCooldownMax)
        {
            DisableProjectile();
        }
        else if (fireCooldownTime == fireCooldownMin && fireBurnedOut && !canFire)
        {
            ResetProjectile();
        }

        if (fireCooldownTime == fireCooldownMin && fireBurnedOut)
        {
            fireBurnedOut = false;
            EnableProjectile();
        }
    }

    void FireProjectile(InputAction.CallbackContext context)
    {
        if (fireCooldownTime <= fireCooldownMax && canFire && !fireBurnedOut)
        {
            fireCooldownTime++;
            particleProjectile?.Play();
        }

        if (fireCooldownTime >= fireCooldownMax && !fireBurnedOut)
        {
            fireBurnedOut = true;
            OnPlayerProjectileBurnout?.Invoke();
            DisableProjectile();
        }
    }

    
}
