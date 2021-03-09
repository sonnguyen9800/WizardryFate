
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateWizard))]
public class Attack : MonoBehaviour
{
    [Header("Character Stats")]



    [Header("Attack Stats")]
    [SerializeField] [Range(0.5f, 7.5f)] private float flySpeed;
    [SerializeField] private LayerMask notToHitLayer;
    [SerializeField] private GameObject magicShootPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;
    private Camera _cam;


    Wizard _wizard;

    [Header("Sound")]
    public AudioClip attackSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _cam = Camera.main;

        _wizard = GetComponent<Wizard>();

        _audioSource = GetComponent<AudioSource>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            //animateWizard.State = WizardState.LIGHT_ATTACK;
            Shoot();

            if (attackSound == null || _audioSource == null) return; // Check condition before play sound to avoid crash
            _audioSource.PlayOneShot(attackSound);
        }

    }

    private void Shoot()
    {
        Vector3 firepointsaved = new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z);
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
        magicShoot.Firepoint = firepointsaved;
        magicShoot.SetAngle(angle + 90);


        // Set status for prefab projectile
        if (_wizard == null) return; // Check if wizard exist or not
        magicShoot.flySpeed = _wizard.projectilespeed;
        Damager damager = projectileSpawn.GetComponent<Damager>();
        if (damager == null) return;
        damager.damage = _wizard.damage; // Set Damage to base damage
    }

}
