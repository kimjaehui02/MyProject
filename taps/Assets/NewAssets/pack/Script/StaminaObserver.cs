using UnityEngine;

public class StaminaObserver : MonoBehaviour, IStaminaObserver
{
    public GameObject staminaObject; // ���¹̳ʸ� ��Ÿ�� ���� ������Ʈ

    private StaminaSubject staminaSubject;

    private void Start()
    {
        staminaSubject = FindObjectOfType<StaminaSubject>();
        staminaSubject.RegisterObserver(this);

        UpdateStaminaState();
    }

    public void OnStaminaChanged(float currentStamina, float maxStamina)
    {
        // ���� ���̸� ���¹̳� ��ġ�� �°� ����
        float scaleFactor = currentStamina / maxStamina;
        Vector3 scale = staminaObject.transform.localScale;
        scale.x = scaleFactor;
        staminaObject.transform.localScale = scale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            staminaSubject.ConsumeStamina(10); // ���¹̳� ����
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            staminaSubject.RestoreStamina(10); // ���¹̳� ����
        }
    }

    private void UpdateStaminaState()
    {
        float currentStamina = staminaSubject.CurrentStamina;
        float maxStamina = staminaSubject.MaxStamina;

        // ���� ���̸� ���¹̳� ��ġ�� �°� ����
        float scaleFactor = currentStamina / maxStamina;
        Vector3 scale = staminaObject.transform.localScale;
        scale.x = scaleFactor;
        staminaObject.transform.localScale = scale;
    }
}
