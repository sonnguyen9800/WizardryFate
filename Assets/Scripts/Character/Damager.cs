using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{

    [SerializeField][Range(0.01f, 2f)] private float attackRadius;
    [SerializeField] private float damage;
    [SerializeField] LayerMask damageableLayer;
    [SerializeField] Transform hitBoxPosition;
    private void Awake() {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void CheckAttackHitBox(){
        Collider2D[] objectCollided = Physics2D.OverlapCircleAll(hitBoxPosition.position, 
        attackRadius, damageableLayer );
        //print("Object Found:" + objectCollided.Length);
        //         Collider2D[] objectCollided2 = Physics2D.CircleCastAll(hitBoxPosition.position, 
        // attackRadius, new Vector2(0.1f, 0.1f),  damageableLayer );
        foreach (Collider2D collider in objectCollided){
            //print("Detect");
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
        Gizmos.DrawWireSphere(hitBoxPosition.position, attackRadius);

    }
}
