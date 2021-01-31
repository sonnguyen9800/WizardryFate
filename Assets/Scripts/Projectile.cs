using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] [Range(0, 40f)] private float flySpeed = 20f;
    public Vector3 TargetPosition { get; set; }
    private bool isFired = false;
    private void Update()
    {
        if (TargetPosition == null) return;
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, flySpeed * Time.deltaTime);

        if (Vector2.Distance(TargetPosition, transform.position) < 0.1f)
        {
            
            Destroy(gameObject);
        }
    }
    public void SetAngle(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }
}

