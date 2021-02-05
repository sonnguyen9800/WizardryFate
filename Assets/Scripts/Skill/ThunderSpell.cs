using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpell : MonoBehaviour
{
    // Start is called before the first frame update
    private ParticleSystem _ps;
    [SerializeField] LayerMask layersTobeHit;
    [SerializeField] private int _damage;
    //private Damageable[] listofObjects = new GameObject[]{};
    public List<ParticleCollisionEvent> collisionEvents;
    public List<Damageable> damageables = new List<Damageable>{};

    [SerializeField] Transform hitBoxPosition;
    [SerializeField][Range(0.01f, 5f)] private float attackRadius;


    private void CheckAttackHitBox(){
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, layersTobeHit );

        if (objectCollided.Length == 0) return;

        //print("COLLISION");
        
        foreach (Collider2D collided in objectCollided)
        {
            Damageable damageable = collided.transform.GetComponentInParent<Damageable>();

            if (damageables.Contains(damageable)) continue;
            damageables.Add(damageable);
            damageable.TakeDamage(_damage);
        }


        //Destroy(gameObject);
    }
    private List<GameObject> objectsCollided = new List<GameObject>();

    private void Awake() {
        _ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();

        var coll = _ps.collision;
        coll.enabled = true;
        //coll.bounce = 0.5f;
    }
    // private void OnTriggerEnter2D(Collision2D other) {
        
    // }
    private void FixedUpdate() {
        CheckAttackHitBox();
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);

    }
}
