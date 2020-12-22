using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
[RequireComponent (typeof (AnimateWizard))]
public class Wizard : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField][Range(0,10)] int moveSpeed;
    [SerializeField][Range(0,1)] float accelerationTimeGround = 0.1f;

    [Header("Jump Variables")]
    public float jumpHeight; public float timeToJumpApex;
    [SerializeField][Range(0,1)] float accelerationTimeAirborne = 0.2f;

    [Header("Transition")]
    [SerializeField][Range(0,2)] float RunAndIdle = 0.5f;

    // VARIABLES:
    private float jumpVelocity;
    private float gravity = -20;
    Controller2D controller2D;



    public Vector2 movementVelocity; // Important variable, determine the velocity of game object
    // Animator:
    private AnimateWizard animateWizard;
    float velocityXSmoothing;



    private void Awake() {
        controller2D = GetComponent<Controller2D>();

        animateWizard = GetComponent<AnimateWizard>();

    }

    // Start is called before the first frame update
    void Start()
    {
        gravity = -(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    // Update is called once per frame
    void Update()
    {

        if (controller2D.collisionInfo.above || controller2D.collisionInfo.below){
            movementVelocity.y = 0;

        }

        Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), 
            Input.GetAxisRaw ("Vertical"));


        if (Input.GetButton("Jump") && controller2D.collisionInfo.below){
            //Debug.Log("Jump");
            movementVelocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
		movementVelocity.x = Mathf.SmoothDamp(movementVelocity.x,targetVelocityX, ref velocityXSmoothing, (controller2D.collisionInfo.below) ? accelerationTimeGround : accelerationTimeAirborne );
		movementVelocity.y += gravity * Time.deltaTime;

        controller2D.Move(movementVelocity*Time.deltaTime);

        UpdateAnimate();

       


    }



    private void UpdateAnimate(){




        if (movementVelocity.x > 1){
            transform.localScale = new Vector3(1,1,1);
        }else if (movementVelocity.x < -1){
            transform.localScale = new Vector3(-1,1,1);
        }

        if (!controller2D.collisionInfo.below){
            animateWizard.state = AnimateWizard.AnimateState.JUMP;
        }

        if (Mathf.Abs( movementVelocity.x) < RunAndIdle && controller2D.collisionInfo.below){
            animateWizard.state = AnimateWizard.AnimateState.IDLE;
        } else if (Mathf.Abs( movementVelocity.x) >= RunAndIdle && controller2D.collisionInfo.below){
            animateWizard.state = AnimateWizard.AnimateState.RUNNING;
        }
    }




}
