using UnityEngine;

public class HpObserver : MonoBehaviour, IHealthObserver
{
    public GameObject[] hpObjects; // Hp를 표시할 게임 오브젝트들

    private HealthSubject healthSubject;

    private void Awake()
    {
        healthSubject = FindObjectOfType<HealthSubject>();
        if (healthSubject != null)
        {
            healthSubject.RegisterObserver(this);
            UpdateHealthObjects(healthSubject.intOftHealth);
        }
    }

    public void OnHealthChanged(int currentHealth, int maxHealth)
    {
        UpdateHealthObjects(currentHealth);
    }

    private void UpdateHealthObjects(int currentHealth)
    {
        for (int i = 0; i < hpObjects.Length; i++)
        {
            if (i < currentHealth)
            {
                hpObjects[i].SetActive(true);
            }
            else
            {
                hpObjects[i].SetActive(false);
            }
        }
    }

    public void SetSubject(HealthSubject subject)
    {
        healthSubject = subject;
        if (healthSubject != null)
        {
            healthSubject.RegisterObserver(this);
            UpdateHealthObjects(healthSubject.intOftHealth);
        }
    }
}
