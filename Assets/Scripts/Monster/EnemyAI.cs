using UnityEngine;
using Pathfinding;
using System.Collections;

public enum AIState
{
    WAIT, // WAIT: Moving repeat in a pattern (predefined by waypoint)
    HUNTING, // Using A* 
    ATTACK
}

public enum FaceToward
{
    LEFT = -1,
    RIGHT = 1
}


// Fly monser AI
public class EnemyAI : MonoBehaviour
{
    public Path path;

    public Transform target;
    [SerializeField] public float speed = 1.5f;
    public float nextPointDis = 3f; // Threshold to move to next point
    int currentPoint = 0;
    //bool reachedEnd = false;
    [SerializeField] private GameObject _enemyAttackProjectile;
    // Predefined path
    [SerializeField] Transform[] waypoints;

    // Seeker
    Seeker seeker;

    [SerializeField] public float timeToCalNewPath = 0.0f;
    public float repeatRate = 0.5f;

    // Initialize Player game object;
    GameObject player;
    // AI FSM
    [SerializeField] [Range(4f, 10f)] private float attackRange;
    [SerializeField]  private AIState state = AIState.HUNTING;

    // Raycast Hit to check 
    
    [SerializeField] [Range(4f, 10f)] private float _raycastLength = 4.0f;
    RaycastHit2D[] hits;


    // Attack Mode:
    private float firerate = 1.0f;
    private float currentTimer = 0.0f;
    // Animation Sprite
    private FaceToward face; // Indicate the face of 
    [SerializeField] 
    private float speedProjectile = 5.0f;

    // Customize waypoints

    public void setWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }




    private void Wait()
    {
        // Check Distance player and Enemy
        var distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < attackRange)
        {
            state = AIState.HUNTING;
        }
        if (waypoints.Length == 0) return; // check lenght of predefined path
        System.Random random = new System.Random();

        // Move according to the waypoints
        var rndMember = waypoints[random.Next(waypoints.Length)];
        transform.position = Vector3.MoveTowards(transform.position, rndMember.position, speed * Time.deltaTime);      
    }


    private bool CheckRayCastCollide()
    {
        hits = Physics2D.RaycastAll(transform.position, new Vector2((int)face, 0), _raycastLength);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + (int)face* _raycastLength, transform.position.y));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Player") {
                return true;
            }
        }
        return false;
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

        // Check condition to move to next state        
        if (Vector2.Distance(player.transform.position, transform.position) > attackRange){
            state = AIState.WAIT; // Move back to wait state
            return;
        }
        state = CheckRayCastCollide() ? AIState.ATTACK : AIState.HUNTING;
    }

    private void FlipSpriteAnimate()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.identity;
            face = FaceToward.RIGHT;

        } else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            face = FaceToward.LEFT;
        }
    }

    private void Attack()
    {
        if (!CheckRayCastCollide()) { state = AIState.HUNTING; }
        currentTimer -= Time.deltaTime;
        if (currentTimer < 0.0f) { currentTimer = 0; }
        if (currentTimer > 0.0f) return;

        GameObject projectile = Instantiate(_enemyAttackProjectile, transform.position, transform.rotation);

        currentTimer = firerate;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2((int)face, 0) * speedProjectile, ForceMode2D.Impulse);

        Destroy(projectile, 10);
        
    }



    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", timeToCalNewPath, repeatRate);
        player = GameObject.FindGameObjectWithTag("Player");
        state = AIState.HUNTING;
        face = FaceToward.RIGHT;
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
        speed = 1.5f;
        speedProjectile = 4.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(state);

        switch (state)
        {
            case AIState.WAIT:
                Wait();
                break;
            case AIState.HUNTING:
                Approaching();
                break;
            case AIState.ATTACK:
                Attack();
                break;
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
    }
}
