using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Color startingColour;
    NavMeshAgent agent;
    PlayerIdle player;
    Renderer rend;
    Vector3 basePos;

    [SerializeField] int aggroDistance = 7;
    [SerializeField] int giveUpDistance = 14;
    [SerializeField] float moveSpeed;
    bool hasChased;
    bool isChasing;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }

    void Awake()
    {
        SetupEnemy();
    }

    void Update()
    {
        ChasePlayer();
    }

    void ChasePlayer()
    {
        if (player != null && rend != null && agent != null)
        {
            float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);
            float distanceFromStart = Vector3.Distance(transform.position, basePos);
            float playerDistanceFromStart = Vector3.Distance(player.transform.position, basePos);

            if (!hasChased && !isChasing && distanceFromPlayer <= aggroDistance)
            {
                // Start chasing
                hasChased = true;
                isChasing = true;
                rend.material.color = Color.red;
                agent.destination = player.transform.position;
            }
            else if (hasChased && isChasing && distanceFromStart < giveUpDistance)
            {
                // Continue chasing
                agent.destination = player.transform.position;
            }
            else if (hasChased && isChasing && distanceFromStart >= giveUpDistance)
            {
                // Give up
                isChasing = false;
                agent.destination = basePos;
                rend.material.color = Color.magenta;
            }
            else if (hasChased && !isChasing && agent.velocity == Vector3.zero)
            {
                // Back on guard
                rend.material.color = startingColour;
                hasChased = false;
            }
        }
    }

    void SetupEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
        rend = GetComponent<Renderer>();
        player = FindObjectOfType<PlayerIdle>();

        if (agent != null && rend != null)
        {
            agent.speed = moveSpeed;
            basePos = transform.position;
            startingColour = rend.material.color;
        }
    }
}
