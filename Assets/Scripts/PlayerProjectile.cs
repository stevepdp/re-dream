using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProjectile : MonoBehaviour
{
    public static event Action OnPlayerProjectileBurnout;
    public static event Action OnPlayerProjectileReady;

    InputAction fire;
    [SerializeField] GameObject particleProjectile;
    ParticleSystem particleSystem;
    PlayerControls playerControls;

    bool canFire;
    bool fireBurnedOut;
    int fireCooldownStep = 1;
    int fireCooldownMin = 0;
    int fireCooldownMax = 5;
    int fireCooldownTime;

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

        particleProjectile = GameObject.Find("PlayerProjectile");

        if (particleProjectile != null)
            particleSystem = particleProjectile.GetComponent<ParticleSystem>();
    }

    void Start()
    {
        InvokeRepeating("FireCooldown", fireCooldownStep, fireCooldownStep);
    }

    void OnEnable()
    {
        Challenge.OnDisableProjectile += DisableProjectile;
        Challenge.OnEnableProjectile += EnableProjectile;
        Crystal.OnCrystalCollected += ResetProjectileAndHUD;

        fire = playerControls.Player.Fire;
        fire?.Enable();
        fire.performed += FireProjectile;
    }

    void OnDisable()
    {
        Challenge.OnDisableProjectile -= DisableProjectile;
        Challenge.OnEnableProjectile -= EnableProjectile;
        Crystal.OnCrystalCollected -= ResetProjectileAndHUD;

        fire?.Disable();
        fire.performed -= FireProjectile;
    }

    void EnableProjectile() => canFire = true;

    void DisableProjectile() => canFire = false;

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
            particleSystem?.Play();
        }

        if (fireCooldownTime >= fireCooldownMax && !fireBurnedOut)
        {
            fireBurnedOut = true;
            OnPlayerProjectileBurnout?.Invoke();
            DisableProjectile();
        }
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
}
