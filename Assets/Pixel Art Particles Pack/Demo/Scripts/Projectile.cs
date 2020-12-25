using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public GameObject ExplosionPrefab;
    public float DestroyExplosion = 4.0f;
    public Vector2 Velocity;

    Rigidbody2D rb;
    void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Velocity;

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        var exp = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(exp, DestroyExplosion);
        Destroy(gameObject);
    }
}
