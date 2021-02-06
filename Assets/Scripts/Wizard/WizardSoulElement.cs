using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WizardSoulElement : MonoBehaviour
{
    [SerializeField] private ElementFactory elementFactory;
    private SoulStealer _soulStealer;

    void Awake()
    {
        _soulStealer = GetComponentInParent<SoulStealer>();
    }
    private void Start()
    {
        _soulStealer.OnElementChanged += ChangeVFX;
    }
    public void ChangeVFX(SoulElement soulElement)
    {
        GameObject vfxPrefab = elementFactory.GetElementVFX(soulElement);
        if (vfxPrefab == null) return;
        Instantiate(vfxPrefab, transform.position, Quaternion.identity);
    }
}
