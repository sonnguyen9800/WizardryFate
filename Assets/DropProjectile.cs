using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject dropProjectile;

    [SerializeField] public float fireRate = 5f;
    private float _currentTimer = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTimer > 0) { print("WAITING"); _currentTimer -= Time.deltaTime; return; }
        GameObject projectile =  Instantiate(dropProjectile, transform.position, Quaternion.identity);
        _currentTimer = fireRate;

    }
}
