using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaminaSubject : MonoBehaviour
{
    #region 변수부분들

    public Camera camera;



    /// <summary>
    /// ㅁㄴㅇㅁㄴㅇㅁㄴㅇㅁㄴ
    /// </summary>
    public float floatOfMaxStamina;
    /// <summary>
    /// 현재 스태미너
    /// </summary>
    public float floatOfStamina;
    /// <summary>
    /// 스테미너들의 옵저버 리스트
    /// </summary>
    public List<StaminaObserver> listStaminaObserverOfObserver;


    #endregion


    /// <summary>
    /// 옵저버 리스트에 등록시킵니다
    /// </summary>
    /// <param name="observer"></param>
    public void RegisterObserver(StaminaObserver observer)
    {
        // 옵저버 리스트에 옵저버를 추가합니다
        listStaminaObserverOfObserver.Add(observer);
    }

    /// <summary>
    /// ㅁㄴㅇㄴㅇㅁㄴㅇㅁㄴㅇ
    /// </summary>
    /// <param name="observer">1231</param>
    public void RemoveObserver(StaminaObserver observer)
    {
        // 옵저버 리스트에 옵저버를 제거합니다
        listStaminaObserverOfObserver.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in listStaminaObserverOfObserver)
        {
            observer.OnStaminaChanged(floatOfStamina, floatOfMaxStamina); // 옵저버들에게 스태미너 변경을 알림
        }
    }

    public void ConsumeStamina(float amount)
    {
        floatOfStamina -= amount; // 스태미너 소비
        if (floatOfStamina < 0)
        {
            floatOfStamina = 0; // 스태미너가 0보다 작으면 0으로 설정
        }

        NotifyObservers(); // 옵저버들에게 스태미너 변경을 알림
    }

    public void RestoreStamina(float amount)
    {
        floatOfStamina += amount; // 스태미너 회복
        if (floatOfStamina > floatOfMaxStamina)
        {
            floatOfStamina = floatOfMaxStamina; // 스태미너가 최대 스태미너를 초과하면 최대 스태미너로 설정
        }

        NotifyObservers(); // 옵저버들에게 스태미너 변경을 알림
    }


}

public interface IStaminaObserver
{
    void OnStaminaChanged(float floatOfStamina, float floatOfMaxStamina);
}