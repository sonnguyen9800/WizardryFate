using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemFactory itemFactory;

    Damageable damageable;
    Wizard characterStats;
    GameObject prefabVFX;

    [Header("Sound")]
    private AudioSource _audioSource;
    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.tag != "Player") return;
        // Check tag to detect collision with player
        damageable = collidedGameObject.GetComponent<Damageable>();
        characterStats = collidedGameObject.GetComponent<Wizard>();


        prefabVFX = Instantiate(itemFactory.prefabVFX, collidedGameObject.transform.position, Quaternion.identity);
        _audioSource = prefabVFX.GetComponent<AudioSource>();
        _audioSource.PlayOneShot(itemFactory.sound);


        Destroy(prefabVFX, 4f);
        if (damageable == null) return;
        if (characterStats == null) return;
        damageable.Heal(damageable.maxHP * (itemFactory.hpRecover / 100));

        if (damageable.isInvicible)
        {
            damageable.isInvicible = false;
        }
        if (itemFactory.hpLoss > 0)
        {
            damageable.TakeDamage(damageable.currentHP * (itemFactory.hpLoss / 100));
        }

        if (itemFactory.damageIncrease > 0)
        {
            characterStats.damage += characterStats.damage * itemFactory.damageIncrease / 100;
            characterStats.RenderDamageIndicator();
        }
        characterStats.amour += itemFactory.amourIncrease;
        characterStats.projectilespeed += (itemFactory.increaseProjectileSpeed) / 10;



        // May be more to go
        Destroy(gameObject);
    }
}
