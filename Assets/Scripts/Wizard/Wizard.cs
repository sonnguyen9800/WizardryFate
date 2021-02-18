using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
[RequireComponent(typeof(AnimateWizard))]
public class Wizard : MonoBehaviour
{
    [Header("Stats")]
    //CharacterStats stats = 
    public CharacterStats CharacterStats;

    [Header("Movement Speed")]
    [SerializeField] [Range(0, 10)] private int moveSpeed;
    [SerializeField] [Range(0, 1)] private float accelerationTimeGround = 0.1f;

    [Header("Jump Variables")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float timeToJumpApex;
    [SerializeField] [Range(0, 1)] private float accelerationTimeAirborne = 0.2f;

    [Header("Transition")]
    [SerializeField] [Range(0, 2)] private float runAndIdle = 0.5f;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    private float jumpVelocity;
    private float gravity = -20;
    private Controller2D controller2D;
    private Vector2 movementVelocity; // Important variable, determine the velocity of game object
    private AnimateWizard animateWizard;
    private float velocityXSmoothing;

    private void Awake()
    {
        controller2D = GetComponent<Controller2D>();
        animateWizard = GetComponent<AnimateWizard>();
    }
    void Start()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }
    void Update()
    {

        if (controller2D.CollisionInfo.above || controller2D.CollisionInfo.below)
        {
            movementVelocity.y = 0;

        }

        if (Input.GetKeyDown(jumpKey) && controller2D.CollisionInfo.below)
        {
            movementVelocity.y = jumpVelocity;
        }

        float targetVelocityX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        movementVelocity.x = Mathf.SmoothDamp(movementVelocity.x, targetVelocityX, ref velocityXSmoothing, (controller2D.CollisionInfo.below) ? accelerationTimeGround : accelerationTimeAirborne);
        movementVelocity.y += gravity * Time.deltaTime;

        controller2D.Move(movementVelocity * Time.deltaTime);

        UpdateAnimate();
    }
    private void UpdateAnimate()
    {

        if (movementVelocity.x > 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (movementVelocity.x < -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        bool isGrounded = controller2D.CollisionInfo.below;
        if (!isGrounded && jumpVelocity >= 0)
        {
            animateWizard.State = WizardState.JUMP;
            return;
        }

        if (!isGrounded && jumpVelocity < 0)
        {
            animateWizard.State = WizardState.FALL;
            return;
        }

        if (Mathf.Abs(movementVelocity.x) < runAndIdle)
        {
            animateWizard.State = WizardState.IDLE;
            return;
        }
        animateWizard.State = WizardState.RUNNING;
    }




}
