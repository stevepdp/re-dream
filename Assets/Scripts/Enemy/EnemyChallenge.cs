using System;
using UnityEngine;

public class EnemyChallenge : EnemyHP
{
    public static event Action OnChallengeEnemyDefeated;

    void OnEnable() => Challenge.OnChallengeEnemyAutokill += AutoKill;

    void OnDisable() => Challenge.OnChallengeEnemyAutokill -= AutoKill;

    void AutoKill() => Destroy(gameObject);

    public override void DeductHP()
    {
        HP--;
        if (HP <= 0)
            Destroy(gameObject);
        OnChallengeEnemyDefeated?.Invoke();
    }
}
