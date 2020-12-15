using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Wizard : MonoBehaviour
{

    [SerializeField][Range(0,-40)] float gravity = -20;
    Controller2D controller2D;

    Vector2 movementVelocity ; // Important variable, determine the velocity of game object


    private void Awake() {
        controller2D = GetComponent<Controller2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementVelocity.y += gravity*Time.deltaTime;
        controller2D.Move(movementVelocity*Time.deltaTime);
    }
}
