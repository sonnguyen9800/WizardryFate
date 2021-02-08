using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [SerializeField] private ElementFactory elementFactory;

    [SerializeField] private Transform _firepoint;
    [SerializeField] private KeyCode fireKey = KeyCode.Mouse0;

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
    [SerializeField]
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
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        _firepoint.rotation = Quaternion.Euler(0, 0, angle);
        if (Input.GetKeyDown(fireKey))
        {
            GameObject skillPrefab = elementFactory.GetSkillPrefab(_soulStealer.Element);
            if (skillPrefab == null) return;
            if (_soulStealer.Element == SoulElement.THUNDER && getCooldownTime(SoulElement.THUNDER) <= 0)
            {
                CastSpell(mousePosition, skillPrefab);
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
            }
            else if (_soulStealer.Element == SoulElement.FIRE && getCooldownTime(SoulElement.FIRE) <= 0 )
            {
                CastContinousSpell(mousePosition, skillPrefab, _firepoint); // Set prefab and the firepoint
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
            }
            else if (_soulStealer.Element == SoulElement.WATER && getCooldownTime(SoulElement.WATER) <= 0)
            {
                CastSpell(mousePosition, skillPrefab);
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
            }
            else if (_soulStealer.Element == SoulElement.EARTH && getCooldownTime(SoulElement.EARTH) <= 0)
            {
                CastDropSpell(mousePosition, skillPrefab);
                setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
            }
        }
        
    }


    // Spawn spell object from mouse drop
    void CastDropSpell(Vector3 mousePosition, GameObject abilityPrefab){
        Instantiate(abilityPrefab,mousePosition, transform.rotation);
    }

    void CastContinousSpell(Vector3 mousePosition, GameObject abilityPrefab, Transform firepoint){
        GameObject skill = Instantiate(abilityPrefab,
            mousePosition, transform.rotation);
        FollowFirePoint follow = skill.GetComponent<FollowFirePoint>();
        follow.setFirepoint(firepoint);
        Destroy(skill, 3);
    }


    void CastSpell(Vector3 mousePosition, GameObject abilityPrefab)
    {
        GameObject skill = Instantiate(abilityPrefab, mousePosition, transform.rotation);
        Projectile magicShoot = skill.GetComponent<Projectile>();
        magicShoot.transform.position = _firepoint.position;
        magicShoot.TargetPosition = mousePosition;
        //magicShoot.flySpeed = 2.0f;
    }
}
