using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float acceleration = 0.1f;


    [SerializeField]
    private float jumpForce = 1.0f;



    private Rigidbody2D rb;
    private CircleCollider2D myCircleCollider2d;
    public WizardState state = WizardState.IDLE;

    private Camera main;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCircleCollider2d = GetComponent<CircleCollider2D>();
        main = Camera.main;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Running();
        Jumping();
        FlipOnMouse();
    }

    private void FlipOnMouse()
    {
        Vector3 mousePosition = main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        transform.rotation = mousePosition.x > transform.position.x ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0);
    }

    private void Jumping()
    {
        if (!myCircleCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // rb.velocity = new Vector2(rb.velocity.x, 0);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            this.state = WizardState.JUMP;
        }
    }

    private void Running()
    {
        float control = Input.GetAxis("Horizontal");
        if (control != 0)
        {
            //acceleration
            speed += acceleration;
        }
        else
        {
            // deceleration
            speed -= acceleration;
            if (speed < 0)
            {
                speed = 0;
            }
        }
        speed = Mathf.Clamp(speed, -2.5f, 2.5f);
        Vector2 playerVector = new Vector2(control * speed, rb.velocity.y);
        rb.velocity = playerVector;


        if (control * speed == 0)
        {
            state = WizardState.IDLE;
            return;
        }

        state = WizardState.RUNNING;

    }

}
