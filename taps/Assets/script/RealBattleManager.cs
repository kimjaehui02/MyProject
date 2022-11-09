using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBattleManager : MonoBehaviour
{
    #region  ������Ʈ

    /// <summary>
    /// ���� �Ŵ���
    /// </summary>
    public GameObject gameObjectOfSoundManager;

    /// <summary>
    /// ���� ī�޶�
    /// </summary>
    public GameObject gameObjectOfMainCamera;

    /// <summary>
    /// ��� ������Ʈ��
    /// </summary>
    public GameObject gameObjectOfBackGround;
    





    #endregion

    #region �뵵��

    #region ��� ������ ui

    /// <summary>
    /// ����� ����� �����ִ� ui
    /// </summary>
    public GameObject gameObjectOfPhaseUi;

    /// <summary>
    /// ����� ��Ÿ���� 0= ���� 1= 
    /// </summary>
    public int intOfPhase;



    #endregion

    #region �ߴ� ��Ʈ ui

    /// <summary>
    /// �ߴ��� ��Ʈ���� �����ִ� Ui
    /// </summary>
    public GameObject gameObjectOfNoteUi;

    /// <summary>
    /// ��Ʈ�� �����ִ� �ð�
    /// </summary>
    public float floatOfRemainOfNote;

    /// <summary>
    /// ��Ʈ�� ��Ȯ�� ��ġ�Ǵ� �ð�
    /// </summary>
    public float floatOfJustTimeOfNote;

    /// <summary>
    /// ��Ʈ�� ���� ��ġ�Ǵ� �ð�
    /// </summary>
    public float floatOfNomalTimeOfNote;

    /// <summary>
    /// �ð����� �����Ͽ� ���ݴ��ϴ� �ð��� üũ�Ҷ� ���ϴ�
    /// </summary>
    public float floatOfCheckTime;

    /// <summary>
    /// ��Ʈ�� ��ġ�� ������ȭ �� ���� ����Ʈ�� ��������ϴ�
    /// </summary>
    public List<float> listFloatOfNotePosition;

    #endregion

    #region �÷��̾�� ����
    /// <summary>
    /// �÷��̾� ��Ƽ
    /// </summary>
    public List<GameObject> listGameObjectOfParty;

    /// <summary>
    /// �� ��Ƽ
    /// </summary>
    public List<GameObject> listGameObjectOfEnemyParty;

    /// <summary>
    /// ��Ŀ���ϴ� �÷��̾��Դϴ�
    /// </summary>
    public int intOfPlayerFocus;

    /// <summary>
    /// ��Ŀ���ϴ� ��ĳ�����Դϴ�
    /// </summary>
    public int intOfEnemyFocus;

    #endregion

    #endregion

    private void Update()
    {
        NomalUpdate();
        BattleUpdate();
    }


    #region �������� ó��
    public void NomalUpdate()
    {

    }

    public void BattleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            intOfPlayerFocus++;
            intOfPlayerFocus %= listGameObjectOfParty.Count;
        }
    }
    #endregion

    #region �ܹ����� ó��

    public void BattleStart()
    {


    }

    public void BattleEnd()
    {

    }

    #endregion











    #region ����

    // ���ο� ������ ȭ���� ��ȯ�Ǵ°� �ӵ����� �پ�峮
    // �׸��� �������� ���� ��ũ��Ʈ�� �̹� �������ؼ� ����� ����ϴ�
    // �׷��� ���Ӱ� ���� ��ũ��Ʈ�̴�
    // �ʿ��� �͵��� ������ ����

    // ������Ʈ��

    // -    ���ΰ� ��Ƽ��
    // -    �� ��Ƽ��

    // �⺻���� ĳ���͵��� ������ ���ؼ� ��װ� �ʿ��ϴ�

    // -    (���) ���� �����Ȳ ui
    // -    (�ߴ�) ���� ��Ʈ Ui

    // ����� ui�� ���� ������ �������ؼ� �ʿ��ϰ�
    // �ߴ��� ui�� �����Ҷ� ��Ʈ�� ���� �����ϱ����ؼ� �ʿ��ϴ�


    // �⺻���� ����

    // ���� ������ ��Ʈ�� ������ ������ �ִ�
    // ���� ������ ������� �ڽ��� ��Ʈ��ŭ ��Ʈ�� �����Ѵ�
    // �߾ӿ� ������ ��Ʈ���� ���̴µ�
    // ������ ��Ʈ�� ��ġ�� �����ȴ�
    // �÷��̾ ��ġ�� �°� ��Ʈ�� ������ �ش� ��Ʈ�� �ı��ǰ�
    // ��� ��Ʈ�� �ı��Ǹ� �������� ��ư�� ���� ���� ó���� �����ϴ�

    // ��Ʈ�� ������ �ൿ�� �Ҷ����� �÷��̾�� �Ƿΰ� ���δ�
    // �Ƿδ� �������� ������ �ش� ��ġ��ŭ �߰����ظ� �԰� �����
    // �Ƿΰ� ���ϰ� �����Ǹ� ���ظ� ���� �ʾƵ� �����ϸ� ����� ���°� �ȴ�
    // �̸� �������� �Ƿΰ� ���̸� ��ü�� �ؾ� �Ѵ�

    #endregion
}
