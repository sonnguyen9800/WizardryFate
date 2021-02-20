using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWizard : MonoBehaviour
{
    private Animator animator;

    public WizardState state = WizardState.IDLE;
    public WizardState State { get => state; set => state = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (state == WizardState.IDLE)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("lightAttack", false);
            return;

        }
        if (state == WizardState.JUMP)
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isRunning", false);
            return;
        }

        if (state == WizardState.FALL)
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isFalling", true);
            return;
        }

        if (state == WizardState.RUNNING)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isRunning", true);
            return;
        }
        if (state == WizardState.LIGHT_ATTACK)
        {
            animator.SetBool("lightAttack", true);
            return;
        }
    }
}
