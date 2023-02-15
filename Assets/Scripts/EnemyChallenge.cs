using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyChallenge : Enemy
{
    public static event Action OnChallengeEnemyDefeated;
    public static event Action OnChallengeEnemyAutoKill;

    void OnEnable() => Challenge.OnChallengeEnemyAutokill += AutoKill;

    void OnDisable() => Challenge.OnChallengeEnemyAutokill -= AutoKill;

    public override void DeductHP()
    {
        HP--;
        if (HP <= 0) Destroy(gameObject);
        OnChallengeEnemyDefeated?.Invoke();
    }

    void AutoKill() => Destroy(gameObject);
}
