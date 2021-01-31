using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateWizard))]
public class Attack : MonoBehaviour
{
    //[SerializeField] private float fireRate = 0;
    //[SerializeField] private float damage = 10;
    [SerializeField] private LayerMask notToHitLayer;
    [SerializeField] private GameObject magicShootPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;
    private Camera cam;
    //private float timeToFire = 0;
    private AnimateWizard animateWizard;
    private void Awake()
    {
        cam = Camera.main;
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
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        print(mousePosition);
        Vector3 direction = mousePosition - firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, direction, 200, notToHitLayer);
        Debug.DrawLine(firePoint.position, mousePosition);


        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0, 0, angle);

        Projectile magicShoot = Instantiate(magicShootPrefab).GetComponent<Projectile>();
        magicShoot.transform.position = firePoint.transform.position;
        magicShoot.TargetPosition = mousePosition;
        magicShoot.flySpeed = 4.0f;
        magicShoot.SetAngle(angle + 90);
    }

}
