using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerDead;

    [SerializeField] bool canFire;
    [SerializeField] int hp = 3;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] InputAction fire;
    [SerializeField] ParticleSystem particleProjectile;

    void Awake()
    {
        canFire = true;
        playerControls = new PlayerControls();
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
            Debug.Log("Fire!!");
            particleProjectile.Play();
        }
    }
}
