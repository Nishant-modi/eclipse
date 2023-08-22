using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using static UnityEngine.GraphicsBuffer;

public class ZombieController : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public Rigidbody2D targetrb;
    public Transform eyeLevel;
    float activateDistance;
    public float dayActivetDistance = 10f;
    public float nightActivateDistance = 25f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    float speed;
    public float daySpeed = 200f;
    public float nightSpeed = 500f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;
    [SerializeField] private LayerMask jumpableGround;

    [Header("Custom Behaviour")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;
    public TimeController timeCheck;
    public float mainCastDistance = 5f;
    public float backCastDistance = -2f;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    private Vector2 currentVelocity;
    private bool targetSighted = false;
    
    Seeker seeker;
    Rigidbody2D rb;
    CapsuleCollider2D col;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        

        InvokeRepeating("UpdatePath", 0, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        TargetSighted();
        TimeCheck();

        if(targetSighted)
        {
            ChasePlayer();
        }
        
        
    }

    private void ChasePlayer()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            //targetSighted = false;
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //targetSighted = false;
            return;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        RaycastHit2D isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, 0.1f, jumpableGround);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded && Mathf.Abs(rb.velocity.y)<0.1)
        {
            if (target.position.y - 1f > rb.transform.position.y && targetrb.velocity.y == 0 && path.path.Count < 20)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        // Movement
        rb.velocity = Vector2.SmoothDamp(rb.velocity, force, ref currentVelocity, 0.5f);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                mainCastDistance = Mathf.Abs(mainCastDistance);
                backCastDistance = -Mathf.Abs(backCastDistance);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                mainCastDistance = -(Mathf.Abs(mainCastDistance));
                backCastDistance = Mathf.Abs(backCastDistance);
            }
        }
    }

    private bool TargetInDistance()
    {
        if(Vector2.Distance(transform.position, target.transform.position) < activateDistance)
        {
            return true;
        }
        else
        {
            targetSighted = false;
            return false;
        }
    }

    private void TargetSighted()
    {
        
        Vector2 mainEndPos = eyeLevel.position + Vector3.right * mainCastDistance;
        Vector2 backEndPos = eyeLevel.position + Vector3.right * backCastDistance;
        RaycastHit2D hitMain = Physics2D.Linecast(eyeLevel.position, mainEndPos, 1 << LayerMask.NameToLayer("Player"));
        RaycastHit2D hitBack = Physics2D.Linecast(eyeLevel.position, backEndPos, 1 << LayerMask.NameToLayer("Player"));

        if (hitMain.collider != null || hitBack.collider !=null)
        {
            targetSighted = true;
        }

        Debug.DrawLine(eyeLevel.position, mainEndPos, Color.yellow);
        Debug.DrawLine(eyeLevel.position, backEndPos, Color.cyan);


    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void TimeCheck()
    {

        if (timeCheck.isDay)
        {
            speed = daySpeed;
            activateDistance = dayActivetDistance;
            jumpEnabled = false;
        }
        else
        {
            speed = nightSpeed;
            activateDistance = nightActivateDistance;
            jumpEnabled = true;
        }
    }
}

