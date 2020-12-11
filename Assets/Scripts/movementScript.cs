using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    float control = 0f;
    float jumpCount = 0f;
    Rigidbody2D mybody;

    public WizardState.State state = WizardState.State.IDLE;

    private Camera main ;
    void Awake()
    {
        mybody = gameObject.GetComponent<Rigidbody2D>();
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

    private void FlipOnMouse(){
        //Vector3 pos = Input.mousePosition;
        Vector3 mouseposition = Input.mousePosition; //Find Mouse Position
        mouseposition.z = main.nearClipPlane;

        Vector3 pos2 = main.ScreenToWorldPoint(mouseposition);
        Debug.Log("DATA" + pos2);
        if(pos2.x >= gameObject.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1,1);
        }
        else if(pos2.x < gameObject.transform.position.x)
        {
            //transform.rotation = Quaternion.Euler(0,0,0);
            transform.localScale = new Vector3(-1, 1,1);
           
        }
    }

    private void Jumping(){
        if ( Input.GetKeyDown(KeyCode.Space)){
            mybody.AddForce(new Vector2(mybody.velocity.x, 5f), ForceMode2D.Impulse);
            
        }
    }

    private void Running(){
        float control = Input.GetAxis("Horizontal") ;
        if (control !=  0) {
            //acceleration
            speed += 0.1f;
        } else {
            // deceleration
            speed -= 0.1f;
            if (speed < 0) {
                speed = 0;
            }
        }
        speed = Mathf.Clamp(speed, -2.5f, 2.5f);
        Vector2 playerVector = new Vector2(control*speed, mybody.velocity.y);
        mybody.velocity = playerVector;


        if (control*speed == 0){
            state = WizardState.State.IDLE;
        } else {
            state = WizardState.State.RUNNING;
        }
    }

}
