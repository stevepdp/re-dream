using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDead;

    [SerializeField] int hp = 3;

    void Update()
    {
        CheckEnemyCollision();
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

    void DeductHP()
    {
        hp--;
        if (hp <= 0)
            OnPlayerDead?.Invoke();
    }
}
