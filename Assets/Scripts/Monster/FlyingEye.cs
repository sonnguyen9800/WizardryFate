using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Damageable))]
[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(CharacterType))]
[RequireComponent(typeof(CapsuleCollider2D))]


public class FlyingEye : MonoBehaviour
{
    // Start is called before the first frame update
    private Damageable damageable;
    public CharacterType type = CharacterType.MONSTER;
    private void Awake() {
        damageable = GetComponent<Damageable>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
