using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class ThunderSpell : MonoBehaviour
{
    [SerializeField] private float _damage;
    public List<Damageable> damageables = new List<Damageable>{};
    [SerializeField] Transform hitBoxPosition;
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;
    [SerializeField] [Range(0.01f, 5f)] private float blastRadius;
    [SerializeField] LayerMask layersTobeHit;

    private Damager _damager;

    private void Awake()
    {
        _damager = GetComponent<Damager>();

    }
    private void Update()
    {
       
    }
    private void CheckAttackHitBox(){
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, layersTobeHit );

        if (objectCollided.Length == 0) return;        
        foreach (Collider2D collided in objectCollided)
        {
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();

            if (damageables.Contains(damageable)) continue;
            damageables.Add(damageable);
            damageable.TakeDamage(_damage);
        }
    }
    private void OnDestroy()
    {
        StunAllEnemies();
    }

    private void StunAllEnemies()
    {
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, blastRadius, layersTobeHit);

        if (objectCollided.Length == 0) return;
        foreach (Collider2D collided in objectCollided)
        {
            // Monster monsterAffected = collided.transform.GetComponentInParent<Monster>();
            // monsterAffected.stun = _stunTime;
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();
            damageable.TakeDamage(_damager.damage);
        }
    }

    private void FixedUpdate() {
        CheckAttackHitBox();
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);
        Gizmos.DrawWireSphere(hitBoxPosition.position, blastRadius);

    }
}
