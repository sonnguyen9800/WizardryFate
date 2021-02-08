using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ElementFactory")]
public class ElementFactory : ScriptableObject
{
    [System.Serializable]
    public class ElementInfo
    {
        public SoulElement SoulElement;
        public GameObject NexusPrefab;
        public GameObject ElementVFX;
        public GameObject SkillPrefab;

        public float cooldownTime;

    }
    [SerializeField] private ElementInfo[] elementInfos;
    private Dictionary<SoulElement, ElementInfo> elementMap = new Dictionary<SoulElement, ElementInfo>();
    private void OnEnable()
    {
        foreach (var elementInfo in elementInfos)
        {
            elementMap[elementInfo.SoulElement] = elementInfo;
        }
    }
    public ElementInfo GetElementInfo(SoulElement soulElement)
    {
        return elementMap[soulElement];
    }
    public GameObject GetNexusPrefab(SoulElement soulElement)
    {
        return elementMap[soulElement].NexusPrefab;
    }
    public GameObject GetElementVFX(SoulElement soulElement)
    {
        return elementMap[soulElement].ElementVFX;
    }
    public GameObject GetSkillPrefab(SoulElement soulElement)
    {
        return elementMap[soulElement].SkillPrefab;
    }

    // Return cooldown time
    public float getCooldownTime(SoulElement soulElement){
        return elementMap[soulElement].cooldownTime;
    }

    public ElementInfo[] returnElementInfos(){
        return elementInfos;
    }
}
