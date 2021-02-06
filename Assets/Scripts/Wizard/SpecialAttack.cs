using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField] private ElementFactory elementFactory;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;
    private Camera _cam;
    private SoulStealer _soulStealer;
    private void Awake()
    {
        _cam = Camera.main;
        _soulStealer = GetComponent<SoulStealer>();
    }
    void Update()
    {
        if (Input.GetKeyDown(fireKey))
        {
            GameObject skillPrefab = elementFactory.GetSkillPrefab(_soulStealer.Element);
            if (skillPrefab == null) return;
            if (_soulStealer.Element == SoulElement.THUNDER)
            {
                CastSpell(skillPrefab);
            }
            else if (_soulStealer.Element == SoulElement.FIRE)
            {
                CastSpell(skillPrefab);
            }
            else if (_soulStealer.Element == SoulElement.WATER)
            {
                CastSpell(skillPrefab);
            }
            else if (_soulStealer.Element == SoulElement.EARTH)
            {
                CastSpell(skillPrefab);
            }
        }
    }
    void CastSpell(GameObject abilityPrefab)
    {
        Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        GameObject thunderskill = Instantiate(abilityPrefab,
            mousePosition, transform.rotation);

        Projectile magicShoot = thunderskill.GetComponent<Projectile>();
        magicShoot.transform.position = transform.position;
        magicShoot.TargetPosition = mousePosition;
        //magicShoot.flySpeed = 2.0f;
    }
}
