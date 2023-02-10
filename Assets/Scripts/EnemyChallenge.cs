using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChallenge : Enemy
{
    public static event Action OnChallengeEnemyDefeated;

    public override void DeductHP()
    {
        HP--;
        if (HP <= 0) Destroy(gameObject);
        OnChallengeEnemyDefeated?.Invoke();
    }
}
