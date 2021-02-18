
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateWizard))]
public class Attack : MonoBehaviour
{
    [Header("Character Stats")]

    [SerializeField] CharacterStats stats;


    [Header("Attack Stats")]
    [SerializeField] [Range(0.5f, 7.5f)] private float flySpeed;
    [SerializeField] private LayerMask notToHitLayer;
    [SerializeField] private GameObject magicShootPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;
    private Camera _cam;
    private AnimateWizard animateWizard;

    private void Awake()
    {
        _cam = Camera.main;
        animateWizard = GetComponent<AnimateWizard>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            animateWizard.State = WizardState.LIGHT_ATTACK;
            Shoot();
        }

    }

    private void Shoot()
    {
        Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        //print(mousePosition);
        Vector3 direction = mousePosition - firePoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        //Projectile magicShoot = Instantiate(magicShootPrefab).GetComponent<Projectile>();

        GameObject projectileSpawn = Instantiate(magicShootPrefab);

        Projectile magicShoot = projectileSpawn.GetComponent<Projectile>();
        magicShoot.transform.position = firePoint.transform.position;
        magicShoot.TargetPosition = mousePosition;
        magicShoot.SetAngle(angle + 90);

        // Set status for prefab projectile
        magicShoot.flySpeed = stats.projectileSpeed;
        Damager damager = projectileSpawn.GetComponent<Damager>();
        damager.damage = stats.baseDamage; // Set Damage to base damage
    }

}
