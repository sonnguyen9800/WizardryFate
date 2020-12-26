using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Damageable))]
[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(CharacterType))]
[RequireComponent(typeof(SpriteRenderer))]

public class FlyingEye : MonoBehaviour
{
    // Start is called before the first frame update
    private Damageable damageable;


    public CharacterType type = CharacterType.MONSTER;
    private SpriteRenderer _spriteRenderer;


    private void Awake() {
        damageable = GetComponent<Damageable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
      // damageable.OnDead += Die;
    }

    void Die(){
        Destroy(gameObject);
    }





   
}
