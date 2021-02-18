using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character Stats", menuName = "Character Stats", order = 1)]

public class CharacterStats : ScriptableObject {
    [SerializeField] CharacterType characterType;
    [SerializeField] public float maxHitpoints;
    [SerializeField] public float baseDamage;
    [SerializeField] public float baseCooldown;
    [SerializeField] public float baseDamageFirerate; //For those who need
    [SerializeField] public float projectileSpeed; //For those who need

}
