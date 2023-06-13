using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSubject : MonoBehaviour
{
    public float maxStamina;
    public float currentStamina;
    private List<StaminaObserver> observers = new List<StaminaObserver>();

    public float MaxStamina
    {
        get { return maxStamina; }
        set { maxStamina = value; }
    }

    public float CurrentStamina
    {
        get { return currentStamina; }
        set { currentStamina = value; }
    }

    public void RegisterObserver(StaminaObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(StaminaObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnStaminaChanged(currentStamina, maxStamina);
        }
    }

    public void ConsumeStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
            currentStamina = 0;

        NotifyObservers();
    }

    public void RestoreStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;

        NotifyObservers();
    }
}

public interface IStaminaObserver
{
    void OnStaminaChanged(float currentStamina, float maxStamina);
}