using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DropFactory", menuName = "Drop Factory", order = 1)]
public class DropFactory : ScriptableObject
{
    // Start is called before the first frame update
    [System.Serializable]
    public class ItemDrop
    {
        public GameObject itemPrefab;
        public float droprate;
    }

    public ItemDrop[] dropsList;
    

}
