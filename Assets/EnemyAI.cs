using UnityEngine;
using Pathfinding;

public enum AIState
{
    WAIT, // WAIT: Moving repeat in a pattern (predefined by waypoint)
    HUNTING, // Using A* 
    ATTACK
}

// Fly monser AI
public class EnemyAI : MonoBehaviour
{
    public Path path;

    public Transform target;
    [SerializeField][Range(0.0f, 100.0f)]
    public float speed = 0.5f;


    public float nextPointDis = 3f; // Threshold to move to next point

    int currentPoint = 0;
    //bool reachedEnd = false;

    [SerializeField]
    private GameObject _enemyAttackProjectile;


    
    // Predefined path
    [SerializeField]
    Transform[] waypoints;
    // Seeker
    Seeker seeker;

    [SerializeField]
    public float timeToCalNewPath = 0.0f;
    public float repeatRate = 0.5f;

    // Initialize Player game object;
    GameObject player;

    // AI FSM
    [SerializeField] [Range(0.01f, 5f)] private float attackRange;


    [SerializeField] [Range(0.01f, 5f)]
    private AIState state = AIState.HUNTING;


    [SerializeField]
    private float _raycastLength;
    // Customize waypoints
    public void setWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;

    }

    private void Wait()
    {
        if (waypoints.Length == 0) return; // check lenght of predefined path
        System.Random random = new System.Random();

        // Move according to the waypoints
        var rndMember = waypoints[random.Next(waypoints.Length)];
        transform.position = Vector3.MoveTowards(transform.position, rndMember.position, speed * Time.deltaTime);

        // Check Distance player and Enemy
        var distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < attackRange)
        {
            state = AIState.HUNTING;
        }
    }

    private void Approaching()
    {
        if (path == null) return; // If find no path: skip
        if (currentPoint >= path.vectorPath.Count) return;
        transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentPoint], 
            speed * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentPoint]);

        if (distance < nextPointDis) {currentPoint++;}

        // Flip the sprite to player
        FlipSpriteAnimate();
        return;


        // Check condition to move to next state        
        if (Vector2.Distance(player.transform.position, transform.position) > attackRange){
            state = AIState.WAIT; // Move back to wait state
        }

        //Get the first object hit by the ray
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _raycastLength);
        if (hit.collider != null && hit.collider.tag == "Player")
        {
            state = AIState.ATTACK;
        }
        
        // 

    }

    private void FlipSpriteAnimate()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.identity;

        } else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //throw new System.NotImplementedException();
    }

    private void Attack()
    {
        GameObject projectile = Instantiate(_enemyAttackProjectile, transform.position, transform.rotation);
        // Need to navigate attack direction
        Destroy(projectile, 3);


        state = AIState.HUNTING;
    }


    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", timeToCalNewPath, repeatRate);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath
            (transform.position, target.position, onPathComplete);
        }
    }

    private void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentPoint = 0;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (state == AIState.WAIT)
        // {
        //     Wait();
        // }
        //else if (state == AIState.HUNTING)
        // {
        //     print("HUNTING");
        //     Approaching();
        // }
        //else if (state == AIState.ATTACK)
        // {
        //     Attack();
        // }
        Approaching();
    }

   
}
