using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Damager : MonoBehaviour
{
    private CircleCollider2D circleCollider2D;

    [SerializeField][Range(0.01f, 2f)] private float attackRadius;
    [SerializeField] private float damage;
    [SerializeField] LayerMask damageableLayer;
    [SerializeField] Transform hitBoxPosition;
    private void Awake() {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CheckAttackHitBox(){
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, attackRadius, damageableLayer );
        foreach (Collider2D collider in objectCollided){
            Damageable damageable = collider.transform.GetComponentInParent<Damageable>();
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        CheckAttackHitBox();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }
}
