using System.Collections.Generic;
using UnityEngine;

public class HpObserver : MonoBehaviour, IHealthObserver
{
    public GameObject[] hpObjects; // Hp�� ǥ���� ���� ������Ʈ��
    public GameObject healthObject; // ü�� ������Ʈ

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
            healthSubject.TakeDamage(1); // ü�� ����
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            healthSubject.Heal(1); // ü�� ����
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

        // ü�� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ�� ����
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


