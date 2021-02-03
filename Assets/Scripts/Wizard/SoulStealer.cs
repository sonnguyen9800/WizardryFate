using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulStealer : MonoBehaviour
{
    [SerializeField] private SoulElement element = SoulElement.EMPTY;
    public SoulElement Element { get => element; set => element = value; }

    
}

                