using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    public UnityEvent<float> OnHealthChange;
    public UnityEvent OnDead;
    public UnityEvent OnHit;
    public UnityEvent OnHeal;

    public int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            //UI health
            OnHealthChange?.Invoke((float)CurrentHealth / maxHealth);
        }
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    internal void Hit(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            OnDead?.Invoke();
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    public void Heal(int healthBoost)
    {
        CurrentHealth += healthBoost;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, maxHealth);
        OnHeal?.Invoke();
    }
}
