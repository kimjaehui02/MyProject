using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSubject : MonoBehaviour
{
    /// <summary>
    /// 최대 체력
    /// </summary>
    public int intOfMaxHealth;
    /// <summary>
    /// 현재 체력
    /// </summary>
    public int intOftHealth;
    public List<IHealthObserver> ListOfobservers;


    public void RegisterObserver(IHealthObserver observer)
    {
        ListOfobservers.Add(observer);
    }

    public void RemoveObserver(IHealthObserver observer)
    {
        ListOfobservers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in ListOfobservers)
        {
            observer.OnHealthChanged(intOftHealth, intOfMaxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        intOftHealth -= damage;
        if (intOftHealth < 0)
        {
            intOftHealth = 0;
        }


        NotifyObservers();
    }

    public void Heal(int amount)
    {
        intOftHealth += amount;
        if (intOftHealth > intOfMaxHealth)
        {
            intOftHealth = intOfMaxHealth;
        }


        NotifyObservers();
    }
}

public interface IHealthObserver
{
    void OnHealthChanged(int currentHealth, int maxHealth);
}