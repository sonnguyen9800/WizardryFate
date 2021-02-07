using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFirepoint : MonoBehaviour
{
    // Start is called before the first frame update
    private AnimateWizard _animator;

    [SerializeField] public Transform idlePoint;
    [SerializeField] public Transform runPoint;
    [SerializeField] public Transform jumpPoint;
    [SerializeField] public Transform fallPoint;


    private void Awake() {
        _animator = GetComponentInParent<AnimateWizard>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (_animator.state == WizardState.IDLE){
        //     gameObject.transform.position = idlePoint.transform.position;
        // } else if (_animator.state )

        switch(_animator.state){
            case WizardState.IDLE:
                gameObject.transform.position = idlePoint.transform.position;
                break;
            case WizardState.JUMP:
                gameObject.transform.position = jumpPoint.transform.position;
                break;
            case WizardState.FALL:
                gameObject.transform.position = fallPoint.transform.position;
                break;
            case WizardState.RUNNING:
                gameObject.transform.position = runPoint.transform.position;
                break;
            default:
                
                break;
        }
    }
}
