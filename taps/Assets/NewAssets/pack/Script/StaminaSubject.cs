using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaminaSubject : MonoBehaviour
{
    #region 痕呪採歳級

    public Camera camera;



    /// <summary>
    /// けいしけいしけいしけい
    /// </summary>
    public float floatOfMaxStamina;
    /// <summary>
    /// 薄仙 什殿耕格
    /// </summary>
    public float floatOfStamina;
    /// <summary>
    /// 什砺耕格級税 辛煽獄 軒什闘
    /// </summary>
    public List<StaminaObserver> listStaminaObserverOfObserver;


    #endregion


    /// <summary>
    /// 辛煽獄 軒什闘拭 去系獣典艦陥
    /// </summary>
    /// <param name="observer"></param>
    public void RegisterObserver(StaminaObserver observer)
    {
        // 辛煽獄 軒什闘拭 辛煽獄研 蓄亜杯艦陥
        listStaminaObserverOfObserver.Add(observer);
    }

    /// <summary>
    /// けいしいしけいしけいし
    /// </summary>
    /// <param name="observer">1231</param>
    public void RemoveObserver(StaminaObserver observer)
    {
        // 辛煽獄 軒什闘拭 辛煽獄研 薦暗杯艦陥
        listStaminaObserverOfObserver.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in listStaminaObserverOfObserver)
        {
            observer.OnStaminaChanged(floatOfStamina, floatOfMaxStamina); // 辛煽獄級拭惟 什殿耕格 痕井聖 硝顕
        }
    }

    public void ConsumeStamina(float amount)
    {
        floatOfStamina -= amount; // 什殿耕格 社搾
        if (floatOfStamina < 0)
        {
            floatOfStamina = 0; // 什殿耕格亜 0左陥 拙生檎 0生稽 竺舛
        }

        NotifyObservers(); // 辛煽獄級拭惟 什殿耕格 痕井聖 硝顕
    }

    public void RestoreStamina(float amount)
    {
        floatOfStamina += amount; // 什殿耕格 噺差
        if (floatOfStamina > floatOfMaxStamina)
        {
            floatOfStamina = floatOfMaxStamina; // 什殿耕格亜 置企 什殿耕格研 段引馬檎 置企 什殿耕格稽 竺舛
        }

        NotifyObservers(); // 辛煽獄級拭惟 什殿耕格 痕井聖 硝顕
    }


}

public interface IStaminaObserver
{
    void OnStaminaChanged(float floatOfStamina, float floatOfMaxStamina);
}