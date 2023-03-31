using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] private int hp = 1;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    void OnEnable()
    {
        GameManager.OnRoomRequirementsMet += AutoKill;
    }

    void OnDisable()
    {
        GameManager.OnRoomRequirementsMet -= AutoKill;
    }

    void OnParticleCollision(GameObject other)
    {
        DeductHP();
    }

    void AutoKill() => Destroy(gameObject);

    public virtual void DeductHP()
    {
        hp--;
        if (hp <= 0)
            Destroy(gameObject);
    }
}
