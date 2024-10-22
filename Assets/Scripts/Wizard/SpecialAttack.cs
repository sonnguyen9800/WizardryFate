using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] CharacterStats playerStats;
    [Header("Special Skill")]
    [SerializeField] private ElementFactory elementFactory;
    [SerializeField] private Transform _firepoint;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;


    Wizard _wizard;
    [System.Serializable]
    public class SkillTimer{
        SoulElement element;
        public float currentTimer;

        public SkillTimer(SoulElement soul, float currentTimer){
            this.element = soul;
            this.currentTimer = currentTimer;
        }
        public void reduceTimer(float time){
            if (currentTimer <= 0) return;
            currentTimer -= time;
        }
    }
    Dictionary<SoulElement, SkillTimer> skillTimerMap = new Dictionary<SoulElement, SkillTimer>();
    // Get cooldown timer
    private float getCooldownTime(SoulElement soulElement){
        return skillTimerMap[soulElement].currentTimer;
    }
    private void setCooldownTime(SoulElement soulElement, float cooldown){
        skillTimerMap[soulElement].currentTimer = cooldown;
    }

    private Camera _cam;
    private Vector3 mousePosition;
    private Vector3 _direction;
    private SoulStealer _soulStealer;


    [Header("Sound")]
    private AudioSource _audioSource;
    private void Awake()
    {
        _cam = Camera.main;
        _soulStealer = GetComponent<SoulStealer>();
        // Initialize the map of skill and their timer
        foreach (var elementInfo in elementFactory.returnElementInfos())
        {
            SkillTimer skillTimer = new SkillTimer(elementInfo.SoulElement, elementInfo.cooldownTime);
            skillTimerMap[elementInfo.SoulElement] = skillTimer;
        }
        _wizard = GetComponent<Wizard>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        // Reduce cooldown each frame
        foreach(var item in skillTimerMap.Keys)
        {
            skillTimerMap[item].reduceTimer(Time.deltaTime);
        }
    }
    void Update()
    {
        // Initialize mouse first
        mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        _direction = mousePosition - _firepoint.position;
        _firepoint.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg);
        if (Input.GetKeyDown(fireKey))
        {
            GameObject skillPrefab = elementFactory.GetSkillPrefab(_soulStealer.Element);
            if (skillPrefab == null) return;
            if (_soulStealer.Element == SoulElement.THUNDER && getCooldownTime(SoulElement.THUNDER) <= 0)
            {
                CastSpell(mousePosition, skillPrefab, SoulElement.THUNDER);
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
                _audioSource.PlayOneShot(elementFactory.GetElementAudio(_soulStealer.Element));
            }
            else if (_soulStealer.Element == SoulElement.FIRE && getCooldownTime(SoulElement.FIRE) <= 0 )
            {
                GameObject skill = CastContinousSpell(mousePosition, skillPrefab, _firepoint, SoulElement.FIRE); // Set prefab and the firepoint
                Destroy(skill, elementFactory.getCooldownTime(_soulStealer.Element)*6f); // Destroy skill after preiod of time
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
                _audioSource.PlayOneShot(elementFactory.GetElementAudio(_soulStealer.Element));

            }
            else if (_soulStealer.Element == SoulElement.WATER && getCooldownTime(SoulElement.WATER) <= 0)
            {
                //Summon(elementFactory.GetSkillPrefab);
                //GameObject clone = elementFactory.GetSkillPrefab
                Summon(elementFactory.GetSkillPrefab(_soulStealer.Element));
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
                _audioSource.PlayOneShot(elementFactory.GetElementAudio(_soulStealer.Element));

            }
            else if (_soulStealer.Element == SoulElement.EARTH && getCooldownTime(SoulElement.EARTH) <= 0)
            {
                CastDropSpell(mousePosition, skillPrefab, SoulElement.EARTH);
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
                _audioSource.PlayOneShot(elementFactory.GetElementAudio(_soulStealer.Element));

            }
        }
        
    }


    // Spawn spell object from mouse drop
    private void CastDropSpell(Vector3 mousePosition, GameObject abilityPrefab, SoulElement soulElement){
       GameObject go = Instantiate(abilityPrefab,mousePosition, transform.rotation);
        Damager goDamager = go.GetComponent<Damager>();
        if (goDamager == null) return;
        goDamager.damage = _wizard.damage * elementFactory.GetElementDamageRate(soulElement); // Set damage
    }
    private GameObject CastContinousSpell(Vector3 mousePosition, GameObject abilityPrefab, Transform firepoint, SoulElement soulElement){
        GameObject skill = Instantiate(abilityPrefab,
            firepoint.position, transform.rotation);
        FollowFirePoint follow = skill.GetComponent<FollowFirePoint>();
        follow.setFirepoint(firepoint);

        // Set damage
        Damager damager = skill.GetComponent<Damager>();
        if (damager == null) return skill;
        damager.damage = _wizard.damage * elementFactory.GetElementDamageRate(soulElement);

        return skill;
    }


    private void CastSpell(Vector3 mousePosition, GameObject abilityPrefab, SoulElement soulElement)
    {
        GameObject skill = Instantiate(abilityPrefab, _firepoint.position, transform.rotation);
        // Projectile magicShoot = skill.GetComponent<Projectile>();
        // if (magicShoot == null) return;
        // magicShoot.transform.position = _firepoint.position;
        // magicShoot.TargetPosition = mousePosition;
        
        // magicShoot.flySpeed = _wizard.projectilespeed * 0.5f;
        // Set damage
        ThunderSpell damager = skill.GetComponent<ThunderSpell>();
        damager.TargetPosition = mousePosition;
        damager.FlySpeed = _wizard.projectilespeed * 0.5f;

        //if (damager == null) return;
        damager._damage = _wizard.damage * elementFactory.GetElementDamageRate(soulElement);
    }
    
    public void Summon(GameObject abilityPrefab)
    {

        Vector3 playerPos = transform.position;
        GameObject skill = Instantiate(abilityPrefab );
        skill.transform.position = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);
        GameObject skill2 = Instantiate(abilityPrefab );
        skill2.transform.position = new Vector3(playerPos.x - 1, playerPos.y, playerPos.z);
        Destroy(skill, 2);
        Destroy(skill2, 2);
    }
}
