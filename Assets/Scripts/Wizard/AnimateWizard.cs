using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWizard : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Movement movement;

    private WizardState.State state;

    void Awake()
    {   
        animator = gameObject.GetComponent<Animator>();
        movement = gameObject.GetComponent<Movement>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state = movement.state;
        if (state == WizardState.State.IDLE){
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", false);

        } 
        else if (state == WizardState.State.JUMP){
            
        } 
        
        else if (state == WizardState.State.RUNNING){
            animator.SetBool("isRunning", true);
        }
        
    }
}
