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
[System.Serializable]
public class ElementToNexus
{
    public SoulElement soulElement;
    public GameObject nexusPrefab;
}
public class Nexus : MonoBehaviour
{

    [SerializeField] private SoulElement element;
    [SerializeField] private KeyCode activeKey = KeyCode.X;
    [SerializeField] private ElementToNexus[] nexusFactory;

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

                ElementToNexus elementToNexus = Array.Find(nexusFactory, etn => etn.soulElement == soulStealerElement);
                Instantiate(elementToNexus.nexusPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);

                
            }
        }
    }
}
