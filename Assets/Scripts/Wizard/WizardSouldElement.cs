using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WizardSouldElement : MonoBehaviour
{
    [SerializeField] ElementToNexus[] ElementVFX;
    private SoulStealer soulStealer;
    private SoulElement oldElement = SoulElement.EMPTY;
    void Awake()
    {
        soulStealer = this.GetComponentInParent<SoulStealer>();
    }

    void Update()
    {
        if(oldElement != soulStealer.Element)
        {
            ChangeVFX();
        }
    }
    public void ChangeVFX()
    {
        ElementToNexus elementToNexus = Array.Find(ElementVFX, etn => etn.soulElement == soulStealer.Element);
        // Destroy();
        Instantiate(elementToNexus.nexusPrefab, transform.position, Quaternion.identity);
        oldElement = soulStealer.Element;
    }
}
