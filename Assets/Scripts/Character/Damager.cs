using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;
    public float damage;
    [SerializeField] LayerMask damageableLayer;
    [SerializeField] Transform hitBoxPosition;
    [SerializeField] LayerMask tobeDestroyedLayer; // Layer that destroy this object on hit
    [SerializeField] bool destroyedAfterHit = true;

    [SerializeField] bool causeDamage = true; // If false -> This does cause damage, provide reference to damage instead


    private void Awake()
    {

    }

    private void Start()
    {
        
    }


    private void CheckAttackHitBox(){
        if (!causeDamage) return;
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, damageableLayer );
        if (objectCollided.Length == 0) return;
        
        foreach (Collider2D collided in objectCollided)
        {
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();
            damageable.TakeDamage(damage);
        }


        if (!destroyedAfterHit) return;
        Destroy(gameObject);
    }

    private void CheckDestroyHitbox()
    {
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, tobeDestroyedLayer);
        if (objectCollided.Length > 0) 
        Destroy(gameObject);
    }

    private void FixedUpdate() {
        CheckAttackHitBox();
        CheckDestroyHitbox();
    }

    private void OnDrawGizmos() {
        if (hitBoxPosition == null) return;
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);

    }
}
