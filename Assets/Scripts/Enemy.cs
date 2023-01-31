using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDead;

    [SerializeField] int hp = 1;

    private void OnCollisionEnter(Collision other)
    {
        // TODO: To be replaced with player's projectile
        if (other.gameObject.CompareTag("Player"))
        {
            DeductHP();
        }
    }

    public void DeductHP()
    {
        hp--;
        if (hp <= 0) Destroy(gameObject);
    }
}
