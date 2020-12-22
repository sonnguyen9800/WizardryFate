using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Rigidbody2D Bullet;

    void Start()
    {
        Create();
    }

    void Create()
    {
        Instantiate(Bullet, transform.position, transform.rotation);
    }
}
