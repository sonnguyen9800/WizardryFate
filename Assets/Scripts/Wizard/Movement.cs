using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    float control = 0f;
    float jumpCount = 0f;


    bool onGround = true;

    [SerializeField]
    float bounce = 5.0f;
    Rigidbody2D mybody;

    public WizardState.State state = WizardState.State.IDLE;

    private Camera main;
    void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag != "Ground")
         {
             //Debug.Log("On THE SKY");
             this.onGround = true;
         }
    }

    private void FlipOnMouse()
    {
        Vector3 mousePosition = main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        transform.localScale = mousePosition.x > transform.position.x ? new Vector3(1,1,1) : new Vector3(-1,1,1);
        //print(mousePosition);   
    }

    private void Jumping()
    {
        if (this.onGround == false ){return;}
        if (Input.GetButton("Jump"))
        {
            mybody.AddForce(new Vector2(0, bounce), ForceMode2D.Impulse);
            this.state = WizardState.State.JUMP;
            this.onGround = false;
        }
    }

    private void Running()
    {
        float control = Input.GetAxis("Horizontal");
        if (control != 0)
        {
            //acceleration
            speed += 0.1f;
        }
        else
        {
            // deceleration
            speed -= 0.1f;
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
        }
        else
        {
            state = WizardState.State.RUNNING;
        }
    }

}
