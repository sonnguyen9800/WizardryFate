using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    private float length, startPos;
    [SerializeField]
    private float parallaxEffect;
    private Camera cam;
    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
    }
    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float dist = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if (temp > startPos + length)
        {
            startPos += length;
            return;
        }
        if (temp < startPos - length)
        {
            startPos -= length;
            return;
        }
    }
}
