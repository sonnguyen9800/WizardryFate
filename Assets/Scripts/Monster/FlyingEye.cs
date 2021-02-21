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
    public GameObject player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player"); // Get reference to player
    }

    void Start()
    {

    }

    void Update()
    {
       
    }

    void Die()
    {
        Destroy(gameObject);
    }






}
