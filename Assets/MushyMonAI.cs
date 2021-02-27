using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushyMonAI : MonoBehaviour
{
    // Start is called before the first frame update
    private new Rigidbody2D rigidbody2D;
    GameObject player;

    [SerializeField] public float moveRate = 1f;
    private float _currentTimer = 0;
    private FaceToward face;
    [SerializeField] private float thurst = 1f;

    [Range(0.1f,5f)] [SerializeField] private float DetectRange;
    [Range(1f, 5f)] [SerializeField] private float HuntingRangeRatio;
    [SerializeField] public float VerticalSpeed = 1.2f;

    CharacterStats _stats;
    Damager _monsterDamager;

    private Vector2 originPosition;

    AIState state;

    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        _stats = GetComponent<Damageable>().getStats();
        _monsterDamager = GetComponent<Damager>();

    }

    private void Start()
    {

        // Ini Original Position
        originPosition.x = transform.position.x;
        originPosition.y = transform.position.y;
        // Ini State;
        state = AIState.WAIT;

        if (_stats == null) return;
        if (_monsterDamager == null) return;
        _monsterDamager.damage = _stats.baseDamage;
    }
    private void FixedUpdate()
    {
        MainFSM(state);


    }

    void MainFSM(AIState state)
    {
        //print("Current State:" + state);

        switch (state)
        {
            case AIState.WAIT:
                DoNothing();
                return;
            case AIState.HUNTING:
                HuntingState();
                return;
            case AIState.BACKHOME:
                BackHomeState();
                return;
        }
    }

    void HuntingState()
    {
        if (_currentTimer > 0) { _currentTimer -= Time.deltaTime; return; }
        _currentTimer = moveRate;
        FlipSpriteAnimate();
        MoveTowardPlayer();
        if (Vector2.Distance(player.transform.position, transform.position) > HuntingRangeRatio * DetectRange)
        {
            state = AIState.BACKHOME;
        }

    }

    void BackHomeState()
    {
        FlipSpriteAnimateToOriginal();
        MoveTowardPlayer();
        if (Vector2.Distance(player.transform.position, transform.position) < HuntingRangeRatio * DetectRange)
        {
            state = AIState.HUNTING;
            return;
        }
        if (Vector2.Distance(originPosition, transform.position) < 0.5)
        {
            state = AIState.WAIT;
        }
    }

    void DoNothing()
    {
        if (Vector2.Distance(player.transform.position, transform.position) < DetectRange)
        {
            state = AIState.HUNTING;
        } 
    }

    void MoveTowardPlayer()
    {
        rigidbody2D.velocity = new Vector2((int)face, VerticalSpeed) * thurst;
        //rigidbody2D.AddForce(new Vector2((int)face, 1)*thurst, ForceMode2D.Impulse);

    }

    private void FlipSpriteAnimateToOriginal()
    {
        if (originPosition.x > transform.position.x)
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
    private void FlipSpriteAnimate()
    {
        if (player == null) return;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.collider.name);
    }

    private void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, DetectRange);
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, DetectRange*HuntingRangeRatio);
    }
}
