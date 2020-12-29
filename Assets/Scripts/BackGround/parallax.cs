using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    float length, startPos;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float temp = cam.transform.position.x *(1-parallaxEffect);
        float dist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if (temp > startPos + length){
            startPos += length;
        } else if (temp < startPos - length){
            startPos -= length;
        }
    }
}
