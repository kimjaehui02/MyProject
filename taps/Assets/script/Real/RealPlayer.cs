using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPlayer : RealParty
{
    /// <summary>
    /// �÷��̾��� �ִ� ���¹̳�
    /// </summary>
    public float floatOfMaxStamina;

    /// <summary>
    /// �÷��̾��� ���� ���¹̳�
    /// </summary>
    public float floatOfStamina;

    /// <summary>
    /// 1. �÷��̾� 2. ��� 3. �繫���� 4. ������
    /// </summary>
    public int intOfType;

    public void NewPlayer(float hp, float sta, int type)
    {
        floatOfMaxHp = 4;
        floatOfMaxStamina = 8;
        floatOfHp = hp;
        floatOfStamina = sta;
        intOfType = type;

    }
}


// ���� ������

// �÷��̾�� �� 4���� ĳ���Ͱ� �ְ� ������ �ٸ���

// ���ΰ�   ĳ������ ������ ���� ����
// ���     ĳ������ ������ ���ӷ�
// �繫���� ĳ������ ������ ��������
// ������   ĳ������ ������ ȸ�� �ɷ�

// ĳ������ �ɷ� ���ִ� ũ�� 2�����̴�

// ���� ó�������� �ߵ�
// ��Ʈ�� ���� �� ������ �ߵ�

// ���ΰ� -    ���� ��� ���ظ� ����