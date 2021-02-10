﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : MonoBehaviour
{
   

    [SerializeField] private int _damage;
    public List<Damageable> damageables = new List<Damageable>{};
    [SerializeField] Transform hitBoxPosition;
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;
    [SerializeField] LayerMask layersTobeHit;
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
  
    private void FixedUpdate() {
        CheckAttackHitBox();
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);
    }
}