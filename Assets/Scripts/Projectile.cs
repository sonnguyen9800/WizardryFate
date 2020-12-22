using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Origin {

public class Projectile : MonoBehaviour
{

    [SerializeField][Range(0,0.6f)] public float step;
    private Vector3 targetPosition;
    private bool isFired = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == null) return;
        //transform.Translate(Vector3.right* Time.deltaTime*10);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition){
        Destroy(gameObject);

        }

    }

    public void setTarget(Vector3 target){
        this.targetPosition = target;
    }

    public void setAngle(float angle){
        this.transform.rotation = Quaternion.Euler(0,0,angle - 90);
    }
}

}