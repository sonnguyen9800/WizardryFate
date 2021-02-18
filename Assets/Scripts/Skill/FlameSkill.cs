using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class FlameSkill : MonoBehaviour
{
    private Damager _damager;
    private void Awake()
    {
        _damager = GetComponent<Damager>();
    }
    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            //Debug.Log(other.name);
            Damageable damageable = other.GetComponentInParent<Damageable>();
            damageable.TakeDamage(_damager.damage);
        }
        
    }
    
}
