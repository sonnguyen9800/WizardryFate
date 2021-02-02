using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nexus : MonoBehaviour
{
    private enum Element
    {
        FIRE,
        WATER,
        THUNDER,
        EARTH
    };
    [SerializeField] private Element element;
    [SerializeField] private KeyCode activeKey = KeyCode.X;
    [SerializeField] private GameObject EmptyNexus;
    private Light _light;
    void Start()
    {
        _light = GetComponent<Light>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(activeKey) && other.GetComponent<Wizard>() != null)
        {
            EmptyNexus.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Instantiate(EmptyNexus);
            
        }
    }
}
