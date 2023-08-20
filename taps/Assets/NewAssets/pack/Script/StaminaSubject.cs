using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StaminaSubject : MonoBehaviour
{
    #region �����κе�

    public Camera camera;



    /// <summary>
    /// ����������������������
    /// </summary>
    public float floatOfMaxStamina;
    /// <summary>
    /// ���� ���¹̳�
    /// </summary>
    public float floatOfStamina;
    /// <summary>
    /// ���׹̳ʵ��� ������ ����Ʈ
    /// </summary>
    public List<StaminaObserver> listStaminaObserverOfObserver;


    #endregion


    /// <summary>
    /// ������ ����Ʈ�� ��Ͻ�ŵ�ϴ�
    /// </summary>
    /// <param name="observer"></param>
    public void RegisterObserver(StaminaObserver observer)
    {
        // ������ ����Ʈ�� �������� �߰��մϴ�
        listStaminaObserverOfObserver.Add(observer);
    }

    /// <summary>
    /// ����������������������
    /// </summary>
    /// <param name="observer">1231</param>
    public void RemoveObserver(StaminaObserver observer)
    {
        // ������ ����Ʈ�� �������� �����մϴ�
        listStaminaObserverOfObserver.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in listStaminaObserverOfObserver)
        {
            observer.OnStaminaChanged(floatOfStamina, floatOfMaxStamina); // �������鿡�� ���¹̳� ������ �˸�
        }
    }

    public void ConsumeStamina(float amount)
    {
        floatOfStamina -= amount; // ���¹̳� �Һ�
        if (floatOfStamina < 0)
        {
            floatOfStamina = 0; // ���¹̳ʰ� 0���� ������ 0���� ����
        }

        NotifyObservers(); // �������鿡�� ���¹̳� ������ �˸�
    }

    public void RestoreStamina(float amount)
    {
        floatOfStamina += amount; // ���¹̳� ȸ��
        if (floatOfStamina > floatOfMaxStamina)
        {
            floatOfStamina = floatOfMaxStamina; // ���¹̳ʰ� �ִ� ���¹̳ʸ� �ʰ��ϸ� �ִ� ���¹̳ʷ� ����
        }

        NotifyObservers(); // �������鿡�� ���¹̳� ������ �˸�
    }


}

public interface IStaminaObserver
{
    void OnStaminaChanged(float floatOfStamina, float floatOfMaxStamina);
}