using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject dropProjectile;

    [SerializeField] public float fireRate = 5f;
    Damageable _damageable;
    private float _currentTimer = 0;
    CharacterStats _stats;
    private void Awake()
    {
        _damageable = GetComponent<Damageable>();
        _stats = _damageable.getStats();
    }
    void Start()
    {
        fireRate = _stats.baseDamageFirerate;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTimer > 0) {  _currentTimer -= Time.deltaTime; return; }
        GameObject projectile =  Instantiate(dropProjectile, transform.position, Quaternion.identity);
        _currentTimer = fireRate;

        Damager damager = projectile.GetComponent<Damager>();
        if (damager == null) return;
        damager.damage = _stats.baseDamage;




    }
}
