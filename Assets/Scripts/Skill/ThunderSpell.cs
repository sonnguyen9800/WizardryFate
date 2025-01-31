﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class ThunderSpell : MonoBehaviour
{
    [SerializeField] public float _damage;
    public List<Damageable> damageables = new List<Damageable>{};
    [SerializeField] Transform hitBoxPosition;
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;
    [SerializeField] [Range(0.01f, 5f)] private float blastRadius;
    [SerializeField] LayerMask layersTobeHit;
    [SerializeField] public float DamageCausedOnDestroyed = 1.4f;

    public Vector3 TargetPosition {get; set;}
    public float FlySpeed {get;set;}


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
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (TargetPosition == null) return;

        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, FlySpeed * Time.deltaTime);
        if (Vector2.Distance(TargetPosition, transform.position) < 0.01f)
        {

            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
 
        //StunAllEnemies();
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
            damageable.TakeDamage(_damage*DamageCausedOnDestroyed);
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
