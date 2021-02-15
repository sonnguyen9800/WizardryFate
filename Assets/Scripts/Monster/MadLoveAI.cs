using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


[System.Serializable]
public class FlyingZone
{
    public int MaxY;
    public int MinY;
    public int MaxX;
    public int MinX;

}


public class MadLoveAI : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Extra Properties")]

    [SerializeField]
    public FlyingZone zone; // Working Zone of the system
    [Header("Speed")]
    [SerializeField] public float speed = 1.5f;

   

    [Header("Monster Parameters:")]
    [SerializeField] private GameObject _enemyAttackProjectile;
    // Predefined path



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



    private Vector3 _target; // target to be moved to

    bool direction = true; // True: Move to max X; False: Move to Min X


    // A* Parameters
    public Vector3 targetPosition;
    public float nextWaypointDistance = 0f ;
    Path path;
    int currentWaypoint = 0;
    bool reachEnd = false;
    Seeker seeker;


    Vector2 resetTargetPosition()
    {
        var randomX = pickRandomPointBetween(zone.MaxX, zone.MinX);
        var randomY = pickRandomPointBetween(zone.MaxY, zone.MinY);
        return new Vector2(player.transform.position.x + randomX, 
                                       player.transform.position.y + randomY);
    }

    // Initilize
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("DropLeft");
        state = AIState.WAIT;
        face = FaceToward.RIGHT;
        seeker = GetComponent<Seeker>();

    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 1.5f;
        thurstProjectile = 4.5f;

        targetPosition = resetTargetPosition();
        InvokeRepeating("UpdatePath", 0f, .5f);
        //UpdatePath()

    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            //targetPosition = resetTargetPosition();
            //seeker.StartPath(transform.position, targetPosition, OnPathComplete);
            seeker.StartPath(transform.position, player.transform.position, OnPathComplete);

        }


    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            //Debug.LogError("Path Error");
            Debug.Log("Path Error");
            path = p;
            currentWaypoint = 0;
            //UpdatePath();
            //return;
        }
        
    }


    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    switch (state)
    //    {
    //        case AIState.WAIT:
    //            Wait();
    //            break;
    //        case AIState.HUNTING:
    //            Hunting();
    //            break;
    //    }
    //}

    private void Update()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            print("Reach End");
            reachEnd = true;
            return;
        }
        else
        {
            print("Reach End Not Yet");
            reachEnd = false;
        }

        transform.position = Vector3.MoveTowards(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);
        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }


    private void Hunting()
    {

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
    // Flip the monster toward player
    private void FlipSpriteAnimate()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.identity;
            face = FaceToward.RIGHT;

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            face = FaceToward.LEFT;
        }
    }

    public static float pickRandomPointBetween(float corner1, float corner2)
    {
        System.Random rand = new System.Random();
        if (corner1 == corner2)
        {
            return corner1;
        }
        float delta = corner2 - corner1;
        float offset = (float)rand.NextDouble() * delta;
        return corner1 + offset;
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, SearchRange);

        if (player == null) return;
        float centerX = ((player.transform.position.x + zone.MinX) + (player.transform.position.x + zone.MaxX)) / 2;
        float centerY = ((player.transform.position.y + zone.MinY) + (player.transform.position.y + zone.MaxY)) / 2;

        Gizmos.DrawWireCube(new Vector2(centerX, centerY), new Vector2(zone.MaxX - zone.MinX, zone.MaxY - zone.MinY));
    }
}
