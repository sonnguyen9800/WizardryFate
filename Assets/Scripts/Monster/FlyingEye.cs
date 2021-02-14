using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Damageable))]
[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(CharacterType))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(EnemyAI))]
public class FlyingEye : MonoBehaviour
{
    // Start is called before the first frame update
    private Damageable damageable;
    public CharacterType type = CharacterType.MONSTER;
    private SpriteRenderer _spriteRenderer;

    public float speed;


    private float timeBtwShots;

    public GameObject projectile;

    public GameObject player;

    private void Awake() {
        damageable = GetComponent<Damageable>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindGameObjectWithTag("Player"); // Get reference to player
    }

    void Start()
    {
        // damageable.OnDead += Die;


    }

    void Update()
    {
       
    }

    void Die()
    {
        Destroy(gameObject);
    }






}
