using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [Header("Overall Status")]
    [SerializeField] public float stun = 0.0f; // Time to stun to pass
    [SerializeField] public float poison = 0.0f; // Toxic to reduce poison% hp per second
    [SerializeField] public float speed = 0.1f; // Speed of monster
    [SerializeField] public float fireRate = 0.1f; // Time between attack

    [SerializeField] public SoulElement element = SoulElement.EMPTY;
}
