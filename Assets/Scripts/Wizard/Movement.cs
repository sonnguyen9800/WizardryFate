using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField]
    float accel = 0.1f;


    [SerializeField]
    float bounce = 1.0f;



    Rigidbody2D mybody;
    CircleCollider2D myCircleCollider2d;
    public WizardState.State state = WizardState.State.IDLE;

    private Camera main;
    void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
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
        transform.localScale = mousePosition.x > transform.position.x ? new Vector3(1,1,1) : new Vector3(-1,1,1);
    }

    private void Jumping()
    {
        if (!myCircleCollider2d.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
           // mybody.velocity = new Vector2(mybody.velocity.x, 0);
            return;
        }
        if (Input.GetButton("Jump"))
        {
            Vector2 jumpVector = new Vector2(0f, bounce);

            Vector2 newVectorJump = mybody.velocity + jumpVector;

            if (newVectorJump.y > bounce){
                newVectorJump.y = bounce;
            }
            mybody.velocity = newVectorJump;
            this.state = WizardState.State.JUMP;
        }
    }

    private void Running()
    {
        float control = Input.GetAxis("Horizontal");
        if (control != 0)
        {
            //acceleration
            speed += accel;
        }
        else
        {
            // deceleration
            speed -= accel;
            if (speed < 0)
            {
                speed = 0;
            }
        }
        speed = Mathf.Clamp(speed, -2.5f, 2.5f);
        Vector2 playerVector = new Vector2(control * speed, mybody.velocity.y);
        mybody.velocity = playerVector;


        if (control * speed == 0)
        {
            state = WizardState.State.IDLE;
            return;
        }

            state = WizardState.State.RUNNING;
        
    }

}
