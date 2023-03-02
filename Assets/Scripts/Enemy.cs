using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private bool hasChased;
    [SerializeField] private bool isChasing;
    [SerializeField] private int aggroDistance = 7;
    [SerializeField] private int giveUpDistance = 14;
    [SerializeField] private int hp = 1;
    [SerializeField] private float moveSpeed;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 basePos;

    public int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    private Color startingColour;
    private Renderer rend;

    void OnEnable()
    {
        GameManager.OnRoomRequirementsMet += AutoKill;
    }

    void OnDisable()
    {
        GameManager.OnRoomRequirementsMet -= AutoKill;
    }

    void Awake()
    {
        rend = GetComponent<Renderer>();

        SetupEnemy();
        GetPlayerTransform();
    }

    void Update()
    {
        ChasePlayer();
    }

    void OnParticleCollision(GameObject other)
    {
        DeductHP();
    }

    void AutoKill() => Destroy(gameObject);

    void ChasePlayer()
    {
        if (player != null)
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

    public virtual void DeductHP()
    {
        hp--;
        if (hp <= 0) Destroy(gameObject);
    }

    void GetPlayerTransform() => player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();

    void SetupEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        basePos = transform.position;
        startingColour = rend.material.color;
    }
}
