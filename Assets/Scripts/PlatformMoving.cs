using System.Collections;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{   
    [SerializeField] Transform destination;
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] float pauseSeconds = 3f;

    PlayerIdle player;
    Vector3 originPos;
    float destinationThreshold = 0.1f;
    bool canMove = true;
    bool toDestination = true;

    void Awake()
    {
        originPos = transform.position;
        player = FindObjectOfType<PlayerIdle>();
    }

    void FixedUpdate()
    {
        if (destination != null)
            AutoMove();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player != null) player.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player != null) player.transform.parent = null;
        }
    }

    void AutoMove()
    {
        if (transform.position == originPos && !toDestination)
            toDestination = true;
        else if (transform.position == destination.position && toDestination)
            toDestination = false;

        float distanceToDestination = Vector3.Distance(transform.position, destination.position);
        float distanceToOrigin = Vector3.Distance(transform.position, originPos);

        if (toDestination && distanceToDestination < destinationThreshold)
        {
            transform.position = destination.position;
            canMove = false;
            StartCoroutine(Pause());
        }
        else if (!toDestination && distanceToOrigin < destinationThreshold)
        {
            transform.position = originPos;
            canMove = false;
            StartCoroutine(Pause());
        }
        else
        {
            Move();
        }      
    }

    void Move()
    {
        if (canMove)
        {
            if (toDestination)
                ToDestination();
            else if (!toDestination)
                ToOrigin();
        }
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(pauseSeconds);

        canMove = true;
    }

    void ToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, moveSpeed * Time.deltaTime);
    }

    void ToOrigin()
    {
        transform.position = Vector3.MoveTowards(transform.position, originPos, moveSpeed * Time.deltaTime);
    }
}