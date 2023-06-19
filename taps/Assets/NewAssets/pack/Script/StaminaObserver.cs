using UnityEngine;

public class StaminaObserver : MonoBehaviour, IStaminaObserver
{
    public GameObject gameObjectOfStamina; // ���¹̳ʸ� ��Ÿ�� ���� ������Ʈ

    private StaminaSubject staminaSubjectOfStamina;

    private void Start()
    {
        StaminaStart(); // ���¹̳� �ʱ�ȭ
    }

    private void Update()
    {
        Debugging();
    }

    private void StaminaStart()
    {
        staminaSubjectOfStamina = FindObjectOfType<StaminaSubject>(); // StaminaSubject ������Ʈ ã��
        staminaSubjectOfStamina.RegisterObserver(this); // ������ ���

        UpdateStaminaState(); // ���¹̳� ���� ������Ʈ
    }

    public void OnStaminaChanged(float currentStamina, float maxStamina)
    {
        // ���� ���̸� ���¹̳� ��ġ�� �°� ����
        float scaleFactor = currentStamina / maxStamina; // ���� ���¹̳ʿ� �ִ� ���¹̳��� ���� ���
        Debug.Log(scaleFactor);
        Vector3 scale = gameObjectOfStamina.transform.localScale; // ���� ������Ʈ�� �������� ������
        scale.x = scaleFactor; // �������� x ���� ������ �°� ����
        gameObjectOfStamina.transform.localScale = scale; // �������� �����Ͽ� ���¹̳� ǥ�� ������Ʈ
    }

    private void UpdateStaminaState()
    {
        float currentStamina = staminaSubjectOfStamina.floatOfStamina; // ���� ���¹̳� �� ��������
        float maxStamina = staminaSubjectOfStamina.floatOfMaxStamina; // �ִ� ���¹̳� �� ��������

        // ���� ���̸� ���¹̳� ��ġ�� �°� ����
        float scaleFactor = currentStamina / maxStamina; // ���� ���¹̳ʿ� �ִ� ���¹̳��� ���� ���
        Vector3 scale = gameObjectOfStamina.transform.localScale; // ���� ������Ʈ�� �������� ������
        scale.x = scaleFactor; // �������� x ���� ������ �°� ����
        gameObjectOfStamina.transform.localScale = scale; // �������� �����Ͽ� ���¹̳� ǥ�� ������Ʈ
    }

    public void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            staminaSubjectOfStamina.ConsumeStamina(10); // ���¹̳� ����
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            staminaSubjectOfStamina.RestoreStamina(10); // ���¹̳� ����
        }
    }
}
