using UnityEngine;
using Pathfinding;
using System.Collections;

// FSM to manupulate action of AI
public enum AIState
{
    WAIT, // WAIT: Moving repeat in a pattern (predefined by waypoint)
    HUNTING, // Using A* 
    ATTACK,
    BACKHOME
}

// To navigate the raycast to scan player

public enum FaceToward
{
    LEFT = -1,
    RIGHT = 1
}


// Fly monser AI
public class EnemyAI : MonoBehaviour
{

    [Header("Speed")]
    [SerializeField] public float speed = 1.5f;

    [Header("A* Properties")]
    public float nextPointDis = 3f; // Threshold to move to next point
    private int _currentPoint = 0;
    public Path path;

    [Header("Monster Parameters:")]
    [SerializeField] private GameObject _enemyAttackProjectile;
    // Predefined path

    [Header("Seeker")]
    public Seeker _seeker;

    [SerializeField] private float _timeToCalNewPath = 0.0f;
    [SerializeField] private float repeatRate = 0.5f;


    // Initialize Player game object;
    public GameObject player;

    [Header("FSM")]
    [SerializeField] private AIState state = AIState.HUNTING;

    // AI FSM
    [Header("FSM: WAIT MODE")]
    [SerializeField] Transform[] waypoints;
    [SerializeField] [Range(4f, 10f)] private float SearchRange;

    // Raycast Hit to check 
    [Header("FSM: HUNTING MODE")]

    [SerializeField] [Range(4f, 10f)] private float _raycastLength = 4.0f;
    RaycastHit2D[] hits;

    [Header("FSM: ATTACK MODE")]

    // Attack Mode:
    [SerializeField] private float firerate = 1.0f;
    private float currentTimer = 0.0f;
    // Animation Sprite
    private FaceToward face; // Indicate the face of 
    [SerializeField] private float thurstProjectile = 5.0f;


    CharacterStats _iniStats;

    // Initilize
    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", _timeToCalNewPath, repeatRate);
        player = GameObject.FindGameObjectWithTag("Player");
        state = AIState.WAIT;
        face = FaceToward.RIGHT;

        _iniStats = GetComponent<Damageable>().getStats();
    }

    void Start()
    {
        speed = 1.5f;
        thurstProjectile = 4.5f;

        // Initiatate stats
        firerate = _iniStats.baseDamageFirerate;

    }

    public virtual void UpdatePath()
    {
        if (player == null) return;
        if (_seeker.IsDone())
        {
            _seeker.StartPath
            (transform.position, player.transform.position, onPathComplete);
        }
    }

    public void onPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            _currentPoint = 0;
        }

    }

    // Start is called before the first frame update


    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null) return;
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

    // Customize waypoints
    public void setWaypoints(Transform[] waypoints)
    {
        this.waypoints = waypoints;
    }

    // FSM: WAIT
    private void Wait()
    {
        // Check Distance player and Enemy
        var distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance < SearchRange)
        {
            state = AIState.HUNTING;
        }
        if (waypoints.Length == 0) return; // check lenght of predefined path
        System.Random random = new System.Random();

        // Move according to the waypoints
        var rndMember = waypoints[random.Next(waypoints.Length)];
        transform.position = Vector3.MoveTowards(transform.position, rndMember.position, speed * Time.deltaTime);      
    }


    

    private void Approaching()
    {
        if (player == null) return;
        if (path == null) return; // If find no path: skip
        if (_currentPoint >= path.vectorPath.Count) return;
        transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[_currentPoint], 
            speed * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, path.vectorPath[_currentPoint]);

        if (distance < nextPointDis) {_currentPoint++;}

        // Flip the sprite to player
        FlipSpriteAnimate();

        // Check condition to move to next state        
        if (Vector2.Distance(player.transform.position, transform.position) > SearchRange){
            state = AIState.WAIT; // Move back to wait state
            return;
        }
        state = CheckRayCastCollide() ? AIState.ATTACK : AIState.HUNTING;
    }

    // Flip the monster toward player
    private void FlipSpriteAnimate()
    {
        if (player == null) return;
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
    
    //FSM: Attack
    private void Attack()
    {
        if (!CheckRayCastCollide()) { state = AIState.HUNTING; }
        currentTimer -= Time.deltaTime;
        if (currentTimer < 0.0f) { currentTimer = 0; }
        if (currentTimer > 0.0f) return;

        GameObject projectile = Instantiate(_enemyAttackProjectile, transform.position, transform.rotation);


        if (_iniStats != null)
        {
            Damager damagerProjectile = projectile.GetComponent<Damager>();
            damagerProjectile.damage = _iniStats.baseDamage;
        }

        currentTimer = firerate;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2((int)face, 0) * thurstProjectile, ForceMode2D.Impulse);

        Destroy(projectile, 10);
        
    }



    // Important method -> Check collide of raycast to player
    private bool CheckRayCastCollide()
    {
        hits = Physics2D.RaycastAll(transform.position, new Vector2((int)face, 0), _raycastLength);
        Debug.DrawLine(transform.position, new Vector3(transform.position.x + (int)face * _raycastLength, transform.position.y));
        foreach (var hit in hits)
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SearchRange);
        
    }
}
