using UnityEngine;

public class StaminaObserver : MonoBehaviour, IStaminaObserver
{
    public GameObject staminaObject; // 스태미너를 나타낼 게임 오브젝트

    private StaminaSubject staminaSubject;

    private void Start()
    {
        staminaSubject = FindObjectOfType<StaminaSubject>();
        staminaSubject.RegisterObserver(this);

        UpdateStaminaState();
    }

    public void OnStaminaChanged(float currentStamina, float maxStamina)
    {
        // 가로 길이를 스태미너 수치에 맞게 조절
        float scaleFactor = currentStamina / maxStamina;
        Vector3 scale = staminaObject.transform.localScale;
        scale.x = scaleFactor;
        staminaObject.transform.localScale = scale;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            staminaSubject.ConsumeStamina(10); // 스태미너 감소
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            staminaSubject.RestoreStamina(10); // 스태미너 증가
        }
    }

    private void UpdateStaminaState()
    {
        float currentStamina = staminaSubject.CurrentStamina;
        float maxStamina = staminaSubject.MaxStamina;

        // 가로 길이를 스태미너 수치에 맞게 조절
        float scaleFactor = currentStamina / maxStamina;
        Vector3 scale = staminaObject.transform.localScale;
        scale.x = scaleFactor;
        staminaObject.transform.localScale = scale;
    }
}
