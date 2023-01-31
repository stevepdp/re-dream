using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDead;
    
    [SerializeField] int hp = 1;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 startPos;

    void Awake()
    {
        SetupEnemy();
        GetPlayerTransform();
    }

    void Update()
    {
        ChasePlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //DeductHP();
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            agent.destination = player.transform.position;
        }
    }

    void DeductHP()
    {
        hp--;
        if (hp <= 0) Destroy(gameObject);
    }

    void GetPlayerTransform() => player = GameObject.FindObjectOfType<Player>().GetComponent<Transform>();

    void SetupEnemy()
    {
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
    }
}
