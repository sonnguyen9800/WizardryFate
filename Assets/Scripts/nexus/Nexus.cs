using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    [SerializeField]
    enum Element{
    FIRE,
    WATER,
    THUNDER,
    EARTH
    };
    Light lt;
    void Start()
    {
        lt = GetComponent<Light>();
    }
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.X) & other.gameObject.name =="Wizard"){
            lt.enabled = false;
        }
    }
}
