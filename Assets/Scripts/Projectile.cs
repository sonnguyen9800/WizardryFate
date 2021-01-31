using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{

    [SerializeField] [Range(0.5f, 1.0f)] public float flySpeed = 1.0f;
    public Vector3 TargetPosition { get; set; }
    private void Update()
    {
        print("Fly Speed: " + flySpeed);
        if (TargetPosition == null) return;
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, 0.8f * Time.deltaTime);

        if (Vector2.Distance(TargetPosition, transform.position) < 0.01f)
        {

            Destroy(gameObject);
        }
    }
    public void SetAngle(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}

