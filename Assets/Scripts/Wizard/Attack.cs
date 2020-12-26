using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimateWizard))]
public class Attack : MonoBehaviour
{
   public float fireRate = 0;
   public float Damage = 10;
   public LayerMask nottoHit;

   float TimeToFire =0;

    Transform firepoint;

    public GameObject magicShootPrefab;
    private AnimateWizard animateWizard;
    private void Awake() {
        firepoint = transform.Find("ShootingPoint");
        if (firepoint == null){
            Debug.Log("No firepoint");
        }else {
            //Debug.Log("Find the firepoint");
        }
        
        animateWizard = GetComponent<AnimateWizard>();
    }

    private void Update() {
        
                

        if (fireRate == 0){
            if (Input.GetButtonDown("Fire1")){
                Shoot();
            }
        } else {
            if (Input.GetButtonDown("Fire1") && Time.time > TimeToFire){
                    TimeToFire = Time.time+ 1/fireRate;
                    Shoot();
            }


        }
    }

    private void Shoot(){
        //print("Shoot");


        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        Vector2 mousePos2d = new Vector2(mousePosition.x, mousePosition.y);
        Vector2 firePointPos = new Vector2(firepoint.transform.position.x, firepoint.transform.position.y);
        Vector3 mouse_pos;
        mouse_pos.x = mousePosition.x - firePointPos.x;
        mouse_pos.y = mousePosition.y - firePointPos.y;


        float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x)*Mathf.Rad2Deg;
        firepoint.rotation = Quaternion.Euler(0,0,angle);

        GameObject shootMagic = Instantiate(magicShootPrefab);
        shootMagic.transform.position = firepoint.transform.position;
        Origin.Projectile gameProjectile = shootMagic.GetComponent<Origin.Projectile>();
        gameProjectile.setTarget(mousePosition);
        gameProjectile.setAngle(angle + 90);


    }

}
