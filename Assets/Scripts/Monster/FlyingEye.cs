using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Damageable))]
[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(CharacterType))]
[RequireComponent(typeof(SpriteRenderer))]

public class FlyingEye : Monster
{
    // Start is called before the first frame update
    private Damageable damageable;



    [Header("Unique")]
    public CharacterType type = CharacterType.MONSTER;
    private SpriteRenderer _spriteRenderer;



    private void Awake() {
        damageable = GetComponent<Damageable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update() {
        if (this.stun > 0) return ;

    }
    void Start()
    {
      // damageable.OnDead += Die;
    }

    void Die(){
        Destroy(gameObject);
    }
}
