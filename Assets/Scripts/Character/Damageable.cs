using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    // Health of the instance
    [Header("Set max HP")]
    public float maxHP;
    
    public float currentHP;


    [SerializeField]
    private int life = 1;
    
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public int Life { get => life; set => life = value; }
    public Action OnLifeChanged { get; set; }
    public Action OnHealthChanged { get; set; }
    public Action<float> OnDamageTaken = delegate { };
    public Action<float> OnHeal = delegate { };

    public Action OnDead = delegate { };
    private bool isAlive = true;
    public bool isInvicible = false;

    // Start is called before the first frame update
    private void Start()
    {
        ResetHP();
    }
    private void Update()
    {
        if (currentHP <= 0) ReduceLife();
    }
    public void TakeDamage(float amount)
    {
        if (!isInvicible){
            currentHP -= amount;
            ClampHP();
            OnHealthChanged?.Invoke();
        }

        OnDamageTaken?.Invoke(amount);

    }
    public void Heal(float amount)
    {
        currentHP += amount;
        ClampHP();
        OnHeal?.Invoke(amount);
        OnHealthChanged?.Invoke();
    }
    private void ReduceLife()
    {
        Life--;
        OnLifeChanged?.Invoke();
        if (Life > 0)
        {
            ResetHP();
        }
        else
        {
            Die();
        }
    }
    private void ResetHP()
    {
        currentHP = maxHP;
        OnHealthChanged?.Invoke();
    }
    private void ClampHP()
    {
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }
    public void Die()
    {
        if(!isAlive) return;
        OnDead?.Invoke();
        isAlive = false;
    }
    public float Percentage => currentHP / maxHP;
}
