using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action OnPlayerDead;

    [SerializeField] int hp = 3;

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
}
