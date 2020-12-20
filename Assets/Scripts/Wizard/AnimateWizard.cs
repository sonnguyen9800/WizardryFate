using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWizard : MonoBehaviour
{

    public enum AnimateState {
        IDLE,
        RUNNING,
        JUMP
    }



    // Start is called before the first frame update
    Animator animator;

    public AnimateState state = AnimateState.IDLE;

    void Awake()
    {   
        animator = gameObject.GetComponent<Animator>();
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (state ==  AnimateState.IDLE){
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", false);

        } 
        else if (state == AnimateState.JUMP){
            // animator.SetBool("isGrounded", false);
            // animator.SetBool("isRunning", false);
        } 
        
        else if (state == AnimateState.RUNNING){
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", true);
        }
        
    }
}
