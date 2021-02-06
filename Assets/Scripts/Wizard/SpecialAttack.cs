using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;


    [SerializeField] GameObject thunderskillPrefab;
    private Camera _cam;

    private SoulStealer _soulStealer;



    private void Awake() {
         
        _cam = Camera.main;
        _soulStealer = GetComponent<SoulStealer>();

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            CastSpellThunder();
        }
    }

    void CastSpellThunder(){
        Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        GameObject thunderskill = Instantiate(thunderskillPrefab, 
            mousePosition, transform.rotation);

        Projectile magicShoot = thunderskill.GetComponent<Projectile>();
        magicShoot.transform.position = transform.position;
        magicShoot.TargetPosition = mousePosition;
        //magicShoot.flySpeed = 2.0f;
    }


    void CastSpellEarth(){
        
    }
    void CastSpellWater(){

    }
    void CastSpellFire(){

    }

}
