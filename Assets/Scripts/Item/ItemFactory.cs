using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemFactory", menuName = "Item Factory", order = 1)]

public class ItemFactory : ScriptableObject
{
    [System.Serializable]
    public class Item{
        public string name;
        public GameObject prefab;
        public float hpRecover;
        public float amourIncrease;
        public float damageIncrease;
        public float increaseHealth;
        public float dropRate;

    }
    private Dictionary<string, Item> itemMap = new Dictionary<string, Item>();

    [SerializeField]
    public Item[] items;

    private void OnEnable()
    {
        foreach (var item in items)
        {
            itemMap[item.name] = item;
        }
    }



}
