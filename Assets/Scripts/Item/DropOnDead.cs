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
    private void Awake()
    {
        random = new System.Random();
    }
    private void DropItems()
    {
        foreach(var item in dropFactory.dropsList)
        {
            randomValue = (float)random.NextDouble();
            if (randomValue < item.droprate)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
    private void OnDestroy()
    {
        DropItems();
    }
}
