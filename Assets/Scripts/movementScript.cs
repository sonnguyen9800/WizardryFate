using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementScript : MonoBehaviour
{
    [SerializeField]
    float speed = 10f;
    float control = 0f;
    Rigidbody2D mybody;

    void Awake()
    {
        mybody = gameObject.GetComponent<Rigidbody2D>();
        // if (mybody == null){
        //     print("Error");
        // }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
