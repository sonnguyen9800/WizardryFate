using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WizardSouldElement : MonoBehaviour
{
    [SerializeField] ElementToNexus[] ElementVFX;
    private SoulStealer _soulStealer;
    private SoulElement _oldElement = SoulElement.EMPTY;
    private GameObject _currentElementVFX = null;

    void Awake()
    {

        _soulStealer = this.GetComponentInParent<SoulStealer>();
    }

    void Update()
    {
        if(_oldElement != _soulStealer.Element)
        {
            ChangeVFX();
        }
    }
    public void ChangeVFX()
    {   
        _oldElement = _soulStealer.Element; // Update old element
        if (_soulStealer.Element == SoulElement.EMPTY) return;
        ElementToNexus elementToNexus = Array.Find(ElementVFX, etn => etn.soulElement == _soulStealer.Element);
        _currentElementVFX =  Instantiate(elementToNexus.nexusPrefab, transform.position, Quaternion.identity);

        
    }
}
