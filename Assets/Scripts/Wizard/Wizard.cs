using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Wizard : MonoBehaviour
{
    [Header("Movement Speed")]
    [SerializeField][Range(0,4)] int moveSpeed = 1;
    [SerializeField][Range(0,1)] float accelerationTimeGround = 0.1f;

    [Header("Jump Variables")]
    public int jumpHeight; public float timeToJumpApex;
    [SerializeField][Range(0,1)] float accelerationTimeAirborne = 0.2f;



    // VARIABLES:
    private float jumpVelocity;
    private float gravity = -20;
    Controller2D controller2D;
    private Vector2 movementVelocity; // Important variable, determine the velocity of game object

    float velocityXSmoothing;
    private void Awake() {
        controller2D = GetComponent<Controller2D>();
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

        Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));


        if (Input.GetButton("Jump") && controller2D.collisionInfo.below){
            Debug.Log("Jump");
            movementVelocity.y = jumpVelocity;
        }


        float targetVelocityX = input.x * moveSpeed;

		movementVelocity.x = Mathf.SmoothDamp(movementVelocity.x,targetVelocityX, ref velocityXSmoothing, (controller2D.collisionInfo.below) ? accelerationTimeGround : accelerationTimeAirborne );
		movementVelocity.y += gravity * Time.deltaTime;
        controller2D.Move(movementVelocity*Time.deltaTime);
    }
}
