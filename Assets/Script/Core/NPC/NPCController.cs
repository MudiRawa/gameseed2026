using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public NavMeshAgent agent;

    [Header("Patrol")]
    public Transform[] waypoints;
    public float[] waypointWaitTimes;
    private int currentWaypoint;

    [Header("Chase")]
    public Transform player;
    public float visionRange = 10f;
    public float visionAngle = 60f;

    [Header("State")]
    public bool canChase = true;
    public bool chasing;
    public float waitTime = 2f;
    private bool waiting;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public static NPCController instance;

    void Start()
    {
        instance = this;
        // SIMPAN POSISI AWAL
        startPosition = transform.position;
        startRotation = transform.rotation;

        GoToNextWaypoint();
    }

    void Update()
    {
        if (Caught.instance.isCaught)
        {
            ResetNPC();
            return;
        }

        if (canChase)
        {
            DetectPlayer();
        }
        else
        {
            chasing = false;
        }

        if (chasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    public void ResetNPC()
    {
        agent.enabled = false;

        transform.position = startPosition;
        transform.rotation = startRotation;

        agent.enabled = true;

        chasing = false;

        GoToNextWaypoint();
    }

    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (!waiting)
                StartCoroutine(WaitAndGoToNextWaypoint());
        }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        agent.destination = waypoints[currentWaypoint].position;

        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }

    IEnumerator WaitAndGoToNextWaypoint()
    {
        waiting = true;

        int arrivedIndex =
            (currentWaypoint - 1 + waypoints.Length) % waypoints.Length;

        float thisWait = waitTime;

        if (waypointWaitTimes != null &&
            waypointWaitTimes.Length == waypoints.Length)
        {
            thisWait = waypointWaitTimes[arrivedIndex];
        }

        yield return new WaitForSeconds(thisWait);

        if (!chasing)
            GoToNextWaypoint();

        waiting = false;
    }

    void DetectPlayer()
    {
        Vector3 directionToPlayer =
            (player.position - transform.position).normalized;

        float distance =
            Vector3.Distance(transform.position, player.position);

        float angle =
            Vector3.Angle(transform.forward, directionToPlayer);

        if (distance < visionRange && angle < visionAngle / 2f)
        {
            RaycastHit hit;

            if (Physics.Raycast(
                transform.position + Vector3.up,
                directionToPlayer,
                out hit,
                visionRange))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    chasing = true;
                    return;
                }
            }
        }

        chasing = false;
    }

    void ChasePlayer()
    {
        agent.destination = player.position;
    }
}