using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpell : MonoBehaviour
{
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;
    [SerializeField] [Range(0.01f, 5f)] private float blastRadius;

    [SerializeField] private float damage;
    [SerializeField] LayerMask damageableLayer;
    [SerializeField] LayerMask explosionLayer; // Layer that destroy this game object on touch
    [SerializeField] Transform hitBoxPosition;
    private List<Damageable> _PastDamageables = new List<Damageable>();

    private Damager _damager;
    private void Awake() {
        _damager = GetComponent<Damager>();
    }


    private void CheckAttackHitBox(){
        Collider2D[] touchDestroyed = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, explosionLayer );
        if (touchDestroyed.Length > 0) {
            Destroy(gameObject);
            return; // End the loop
        }
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, damageableLayer );
        
        if (objectCollided.Length == 0) return;
        
        
        foreach (Collider2D collided in objectCollided)
        {
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();
            if (_PastDamageables.Contains(damageable)) continue;
            _PastDamageables.Add(damageable);

            damageable.TakeDamage(_damager.damage);
        }

        
    }

    private void Update() {
        CheckAttackHitBox();
    }

    private void OnDrawGizmos() {
        if (hitBoxPosition == null) return;
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);
        Gizmos.DrawWireSphere(hitBoxPosition.position, blastRadius);

    }

    private void OnDestroy()
    {
        StunAllEnemies();
    }

    private void StunAllEnemies()
    {
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, blastRadius, damageableLayer);

        if (objectCollided.Length == 0) return;
        foreach (Collider2D collided in objectCollided)
        {
            // Monster monsterAffected = collided.transform.GetComponentInParent<Monster>();
            // monsterAffected.stun = _stunTime;
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();
            damageable.TakeDamage(_damager.damage);
        }
    }
}
