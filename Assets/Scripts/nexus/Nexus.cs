using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum SoulElement
{
    EMPTY,
    FIRE,
    WATER,
    THUNDER,
    EARTH
};
public class Nexus : MonoBehaviour
{

    [SerializeField] private SoulElement element;
    [SerializeField] private KeyCode activeKey = KeyCode.X;
    [SerializeField] private ElementFactory elementFactory;
    private Light _light;
    void Start()
    {
        _light = GetComponent<Light>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(activeKey))
        {
            SoulStealer soulStealer = other.GetComponent<SoulStealer>();
            if (soulStealer != null)
            {
                // Swap
                SoulElement soulStealerElement = soulStealer.Element;
                soulStealer.Element = element;
                element = soulStealerElement;

                Instantiate(elementFactory.GetNexusPrefab(element), transform.position, Quaternion.identity);
                Destroy(gameObject);


            }
        }
    }
}
