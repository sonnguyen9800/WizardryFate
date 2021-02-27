using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DropOnDead : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    DropFactory dropFactory;
    System.Random random;
    float randomValue;

    [SerializeField] public float forceOut = 7f;

    private Rigidbody2D itemBody;
    private GameObject prefab;
    private void Awake()
    {
        random = new System.Random();
    }
    private void DropItems()
    {
        foreach(var item in dropFactory.dropsList)
        {
            print("Item OUT:");
            randomValue = (float)random.NextDouble();
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
        DropItems();
    }
}
