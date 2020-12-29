using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField]
    enum element
    {
        Fire,
        Water,
        Earth
    }
    
    void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            print("collide with player "+ col.gameObject.name);
        }
    }
}
