using UnityEngine;

public class StaminaObserver : MonoBehaviour, IStaminaObserver
{
    public GameObject gameObjectOfStamina; // 스태미너를 나타낼 게임 오브젝트

    private StaminaSubject staminaSubjectOfStamina;

    private void Start()
    {
        StaminaStart(); // 스태미너 초기화
    }

    private void Update()
    {
        Debugging();
    }

    private void StaminaStart()
    {
        staminaSubjectOfStamina = FindObjectOfType<StaminaSubject>(); // StaminaSubject 컴포넌트 찾기
        staminaSubjectOfStamina.RegisterObserver(this); // 옵저버 등록

        UpdateStaminaState(); // 스태미너 상태 업데이트
    }

    public void OnStaminaChanged(float currentStamina, float maxStamina)
    {
        // 가로 길이를 스태미너 수치에 맞게 조절
        float scaleFactor = currentStamina / maxStamina; // 현재 스태미너와 최대 스태미너의 비율 계산
        Debug.Log(scaleFactor);
        Vector3 scale = gameObjectOfStamina.transform.localScale; // 게임 오브젝트의 스케일을 가져옴
        scale.x = scaleFactor; // 스케일의 x 값을 비율에 맞게 설정
        gameObjectOfStamina.transform.localScale = scale; // 스케일을 적용하여 스태미너 표시 업데이트
    }

    private void UpdateStaminaState()
    {
        float currentStamina = staminaSubjectOfStamina.floatOfStamina; // 현재 스태미너 값 가져오기
        float maxStamina = staminaSubjectOfStamina.floatOfMaxStamina; // 최대 스태미너 값 가져오기

        // 가로 길이를 스태미너 수치에 맞게 조절
        float scaleFactor = currentStamina / maxStamina; // 현재 스태미너와 최대 스태미너의 비율 계산
        Vector3 scale = gameObjectOfStamina.transform.localScale; // 게임 오브젝트의 스케일을 가져옴
        scale.x = scaleFactor; // 스케일의 x 값을 비율에 맞게 설정
        gameObjectOfStamina.transform.localScale = scale; // 스케일을 적용하여 스태미너 표시 업데이트
    }

    public void Debugging()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            staminaSubjectOfStamina.ConsumeStamina(10); // 스태미너 감소
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            staminaSubjectOfStamina.RestoreStamina(10); // 스태미너 증가
        }
    }
}
