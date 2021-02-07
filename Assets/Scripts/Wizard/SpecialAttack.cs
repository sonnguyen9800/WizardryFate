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

        if (Input.GetKeyDown(fireKey))
        {
            GameObject skillPrefab = elementFactory.GetSkillPrefab(_soulStealer.Element);
            if (skillPrefab == null) return;
            if (_soulStealer.Element == SoulElement.THUNDER && getCooldownTime(SoulElement.THUNDER) <= 0)
            {
                CastSpell(skillPrefab);

            }
            else if (_soulStealer.Element == SoulElement.FIRE && getCooldownTime(SoulElement.FIRE) <= 0 )
            {
                CastSpell(skillPrefab);
            }
            else if (_soulStealer.Element == SoulElement.WATER && getCooldownTime(SoulElement.WATER) <= 0)
            {
                CastSpell(skillPrefab);
            }
            else if (_soulStealer.Element == SoulElement.EARTH && getCooldownTime(SoulElement.EARTH) <= 0)
            {
                CastDropSpell(skillPrefab);
            }
            setCooldownTime(_soulStealer.Element, elementFactory.getCooldownTime(_soulStealer.Element));
        }
        
    }


    // Spawn spell object from mouse drop
    void CastDropSpell(GameObject abilityPrefab){
        Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        GameObject thunderskill = Instantiate(abilityPrefab,
                    mousePosition, transform.rotation);
    }


    void CastSpell(GameObject abilityPrefab)
    {
        Vector3 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));

        GameObject thunderskill = Instantiate(abilityPrefab,
            mousePosition, transform.rotation);

        Projectile magicShoot = thunderskill.GetComponent<Projectile>();
        magicShoot.transform.position = _firepoint.position;
        magicShoot.TargetPosition = mousePosition;
        //magicShoot.flySpeed = 2.0f;
    }
}
