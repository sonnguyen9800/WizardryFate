using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireballs : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float DestroyExplosion = 4.0f;

    void OnCollisionEnter2D (Collision2D col)
    {
        var exp = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        Destroy(exp, DestroyExplosion);
        Destroy(gameObject);
    }
}
