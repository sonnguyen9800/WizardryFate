using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemFactory itemFactory;


    private void Awake()
    {
        //itemEffect = itemFactory.getItembyName("")
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.tag != "Player") return;
        // Check tag to detect collision with player
        Damageable damageable = collidedGameObject.GetComponent<Damageable>();
        
        CharacterStats characterStats = damageable.getStats();
        
        damageable.Heal(damageable.maxHP * (itemFactory.hpRecover / 100));
       
        if (damageable.isInvicible)
        {
            damageable.isInvicible = false;
        }
        if (itemFactory.hpLoss > 0)
        {
            damageable.TakeDamage(damageable.currentHP * (itemFactory.hpLoss / 100));
        }
        characterStats.baseDamage += itemFactory.damageIncrease * 10;
        characterStats.baseAmour += itemFactory.amourIncrease;
        characterStats.projectileSpeed += (itemFactory.increaseProjectileSpeed) / 10;

        // May be more to go
        Destroy(gameObject);
    }
}
