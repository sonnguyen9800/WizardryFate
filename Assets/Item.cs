﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public ItemFactory itemFactory;

    Damageable damageable;
    Wizard characterStats;
    GameObject prefabVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedGameObject = collision.gameObject;
        if (collidedGameObject.tag != "Player") return;
        // Check tag to detect collision with player
        damageable = collidedGameObject.GetComponent<Damageable>();
        characterStats = collidedGameObject.GetComponent<Wizard>();


        prefabVFX =  Instantiate(itemFactory.prefabVFX, collidedGameObject.transform.position, Quaternion.identity);
        Destroy(prefabVFX, 0.65f);
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

        characterStats.damage += itemFactory.damageIncrease * 10;
        characterStats.amour += itemFactory.amourIncrease;
        characterStats.projectilespeed += (itemFactory.increaseProjectileSpeed) / 10;



        // May be more to go
        Destroy(gameObject);
    }
}