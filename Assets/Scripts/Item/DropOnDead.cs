using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DropOnDead : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    DropFactory dropFactory;

    [SerializeField] public float forceOut = 7f;

    private Rigidbody2D itemBody;

    Damageable _damageble;
    private void Awake()
    {
        _damageble = GetComponent<Damageable>();
    }

    private void Start()
    {
        _damageble.OnDamageTaken += DropItems;
    }
    private void DropItems(float var)
    {
        System.Random random = new System.Random();
        foreach(var item in dropFactory.dropsList)
        {
            //print("Item OUT:");

            float randomValue = (float)random.NextDouble();
            if (randomValue < item.droprate)
            {
                GameObject prefab = Instantiate(item.itemPrefab, transform.position, Quaternion.identity);

                itemBody = prefab.GetComponent<Rigidbody2D>();
                itemBody.AddForce(new Vector3((float)random.NextDouble(), (float)random.NextDouble()
                                        , (float)random.NextDouble()) * forceOut, ForceMode2D.Impulse);
                //Item item1 = prefab.GetComponent<Item>();
                //item1.owner = gameObject.name;

                
                
            }
        }
    }
    private void OnDestroy()
    {
        
    }
}
