using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public DamageManager damageManager;


    [field: SerializeField]
    public float Var { get; set; }

    public NoteManager NoteManager;

    /*
    #region notes
    /// <summary>
    /// ��Ʈ ������Ʈ ����
    /// </summary>
    public List<GameObject> notes;

    /// <summary>
    /// ��Ʈ���� �̷л� ��ǥ��
    /// </summary>
    public List<float> noteTrans;

    /// <summary>
    /// ��Ʈ�� �������µ� �ɸ��� �ð�
    /// </summary>
    public float noteTime;

    /// <summary>
    /// ��Ʈ�� ��Ȯ�� üũ�Ǵ� �ð�
    /// </summary>
    public float noteJust;

    /// <summary>
    /// ��Ʈ�� ������ �ϴ½ð�
    /// </summary>
    public float noteSafe;

    /// <summary>
    /// ��Ʈ�� �����ٴϴ� ���� ũ��
    /// </summary>
    public float noteBase;

    #endregion
    */

    // ������ ����

    // - ����

    // ����� �Ʊ��� ����


    // - ������

    // ������ ������ ǥ��
    // �Ʊ��� ������ ����
    // ������ ������ ����
    // �Ʊ��� ������ ����

    // ��Ÿ �ð��� 0.5f

    // ��Ÿ �ð�  = ���� /�ӵ�;




    /// <summary>
    /// ����
    /// </summary>
    public void BattleStart()
    {

    }

    /// <summary>
    /// ������ ������ ǥ����
    /// </summary>
    public void EnemyStart()
    {

    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NoteManager.Check();
        }
    }
}
