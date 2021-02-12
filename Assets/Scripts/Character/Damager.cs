using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;
    [SerializeField] private float damage;
    [SerializeField] LayerMask damageableLayer;
    [SerializeField] Transform hitBoxPosition;
    


    private void CheckAttackHitBox(){
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, damageableLayer );
        if (objectCollided.Length == 0) return;
        
        
        foreach (Collider2D collided in objectCollided)
        {
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();
            damageable.TakeDamage(damage);
        }
        // Damageable damageable = objectCollided.transform.GetComponentInParent<Damageable>();
        // damageable.TakeDamage(damage);
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        CheckAttackHitBox();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);

    }
}
