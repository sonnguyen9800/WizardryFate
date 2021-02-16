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
    private void Awake()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if (_currentTimer > 0) { _currentTimer -= Time.deltaTime; return;  }
        FlipSpriteAnimate();
        MoveTowardPlayer();
        _currentTimer = moveRate;
    }

    void MoveTowardPlayer()
    {
        rigidbody2D.AddForce(new Vector2((int)face, 1)*thurst, ForceMode2D.Impulse);
        //rigidbody2D.AddForce(0, moveSpeed * Time.deltaTime, 0);
        //print("Move");
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
}
