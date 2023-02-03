using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerDead;

    [SerializeField] int hp = 3;
    [SerializeField] PlayerControls playerControls;
    [SerializeField] InputAction fire;
    [SerializeField] ParticleSystem particleProjectile;

    void Awake()
    {
        playerControls = new PlayerControls();
    }

    void OnEnable()
    {
        fire = playerControls.Player.Fire;
        fire.Enable();
        fire.performed += FireProjectile;
    }

    void OnDisable()
    {
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

    void FireProjectile(InputAction.CallbackContext context) => particleProjectile.Play();
}
