using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameSkill : MonoBehaviour
{
    
    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Enemy")
        {
            //Debug.Log(other.name);
            Damageable damageable = other.GetComponentInParent<Damageable>();
            damageable.TakeDamage(30);
        }
        
    }
    
}
