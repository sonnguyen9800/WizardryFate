using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpellExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform hitBoxPosition;
    [SerializeField] [Range(0.01f, 5f)] private float attackRadius;
    [SerializeField] LayerMask layersTobeHit;
    [SerializeField] private float _stunTime;
    [SerializeField] private float _damage;

    [Header("Options")]
    public bool causeDamage = true;

    [Header("Sound")]
    private AudioSource _audioSource;
    public AudioClip soundOndestroyed;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

    }
    private void Start()
    {

        AudioSource.PlayClipAtPoint(soundOndestroyed, transform.position);
    }
    private void OnDestroy() {

        StunAllEnemies();
    }

    private void StunAllEnemies(){
        if (causeDamage == false) return;
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, layersTobeHit );

        if (objectCollided.Length == 0) return;        
        foreach (Collider2D collided in objectCollided)
        {
            // Monster monsterAffected = collided.transform.GetComponentInParent<Monster>();
            // monsterAffected.stun = _stunTime;
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();
            damageable.TakeDamage(_damage);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);
    }
}
