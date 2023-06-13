using System.Collections.Generic;
using UnityEngine;

public class HpObserver : MonoBehaviour, IHealthObserver
{
    public GameObject[] hpObjects; // Hp를 표시할 게임 오브젝트들
    public GameObject healthObject; // 체력 오브젝트

    private HealthSubject healthSubject;

    private void Start()
    {
        healthSubject = FindObjectOfType<HealthSubject>();
        healthSubject.RegisterObserver(this);
        UpdateHealthObjects(healthSubject.CurrentHealth);
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        UpdateHealthObjects(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            healthSubject.TakeDamage(1); // 체력 감소
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            healthSubject.Heal(1); // 체력 증가
        }
    }

    private void UpdateHealthObjects(int currentHealth)
    {
        for (int i = 0; i < hpObjects.Length; i++)
        {
            if (i < currentHealth)
                hpObjects[i].SetActive(true);
            else
                hpObjects[i].SetActive(false);
        }

        // 체력 오브젝트의 활성화/비활성화를 조절
        if (healthObject != null)
        {
            if (currentHealth > 0)
                healthObject.SetActive(true);
            else
                healthObject.SetActive(false);
        }
    }
}


public interface IHealthObserver
{
    void OnHealthChanged(int currentHealth, int maxHealth);
}


