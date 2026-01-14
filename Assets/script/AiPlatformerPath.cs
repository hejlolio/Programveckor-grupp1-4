using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class AiPlatformerPath : MonoBehaviour
{
    //target where the lead will try to move to
    Transform target;

    public PathfindingTargetList targetList;
    public UnityEvent hasReachedEnd;

    //speed off the lead down the path
    public float speed = 1f;
    //Decides how near the next waypoint the lead needs to be. Makes the lead follow the path more closely at higher amounts
    public float nextWaypointDistance = 0.5f;
    //decides how many the path will update per second
    public float repathInterval = 0.5f;

    public Path path;
    public int currentWaypoint;
    bool pathComplete = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        targetList = GetComponent<PathfindingTargetList>();

        //Update the path an amount of times per second equal to repathInterval
        InvokeRepeating("UpdatePath", 0f, repathInterval);
    }
    void UpdatePath()
    {
        //Checks that an existing path isnt currently loading while the path is updating
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        //checks for errors
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
            pathComplete = false;
        }
    }

    private void Update()
    {
        if (targetList != null)
        {
            target = targetList.currentTarget.transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }

        //checks that path is complete then moves on to next waypoint
        if (currentWaypoint >= path.vectorPath.Count)
        {
            if (!pathComplete)
            {
                pathComplete = true;
                hasReachedEnd.Invoke();
            }
            return;
        }

        //Decides a direction down the path then moves the lead in that direction
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 velocity = (direction * Time.deltaTime).normalized;

        rb.linearVelocity = velocity * speed;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        //Updates waypoint when lead reaches the next waypoint
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
