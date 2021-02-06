using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SoulStealer : MonoBehaviour
{
    [SerializeField] private SoulElement element = SoulElement.EMPTY;
    public Action<SoulElement> OnElementChanged { get; set; }
    public SoulElement Element
    {
        get => element;
        set
        {
            element = value;
            OnElementChanged?.Invoke(element);
        }
    }

}

