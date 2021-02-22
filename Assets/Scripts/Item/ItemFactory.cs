using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemFactory", menuName = "Item Factory", order = 1)]

public class ItemFactory : ScriptableObject
{
   
        public string ItemName;
        public float hpRecover;
        public float hpLoss;
        public float amourIncrease;
        public float damageIncrease;
        public float increaseHealth;
        public float increaseProjectileSpeed;
    public GameObject prefabVFX;


}
