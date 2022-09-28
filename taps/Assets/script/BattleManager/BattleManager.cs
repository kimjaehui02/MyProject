using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region �Ŵ�����
    public DamageManager damageManager;

    public NoteManager NoteManager;

    public SkillManager SkillManager;
    #endregion

    #region ������Ʈ��

    [field: SerializeField]
    /// <summary>
    /// �Ʊ� ��Ƽ
    /// </summary>
    public List<GameObject> PartyOfPlayer { get; set; }

    [field: SerializeField]
    /// <summary>
    /// ���� ��Ƽ
    /// </summary>
    public List<GameObject> PartyOfEnemy { get; set; }

    /// <summary>
    /// ui�� ����Ű�� ȭ��ǥ
    /// </summary>
    public GameObject arrowOfPhase;
    #endregion

    #region ������

    #region ���� ������
    /// <summary>
    /// ���� ���� Ƚ��
    /// </summary>
    [field: SerializeField]
    public float pointOfAttack { get; set; }



    #endregion

    #region �������

    public enum Phase { Standby, Main, Battle, End }

    public int intOfPhase;

    public List<Vector3> vector3OfPhase;

    #endregion

    #endregion

    #region Default
    private void Start()
    {
        StartCoroutine(Shake((int)Phase.Standby));
    }

    private void Update()
    {
        NoteManager.NoteManagerUpdate();
        Check();
        UiOfPhase();
    }

    private void FixedUpdate()
    {
        NoteManager.NoteManagerFixedUpdate();
    }

    #endregion

    #region �������
    /// <summary>
    /// ���� ����Ŭ ���۸��� �� ��
    /// </summary>
    public void StandbyOfPhase()
    {
        intOfPhase = (int)Phase.Standby;

        


        StartCoroutine(Shake((int)Phase.Main));
    }

    /// <summary>
    /// ���� ���� ���ϵ�
    /// </summary>
    public void MainOfPhase()
    {
        intOfPhase = (int)Phase.Main;

        //List<List<StructOfFight>> structOfFights = new List<List<StructOfFight>>();

        // ���� ���ݵ��� �󸶳� ���� ǥ���մϴ�
        for (int i = 0; i < PartyOfEnemy.Count; i++)
        {
            // ���� ��Ƽ�� �ִ� ��ũ��Ʈ�� ��������
            ParentsOfParty ofParty = PartyOfEnemy[i].GetComponent<ParentsOfParty>();
            // ofParty ��ũ��Ʈ�� ���� ������ ����ü ����Ʈ�� ��������
            List<StructOfDamage> ofDamages = ofParty.StructOfDamages;


            // �� �κ��� ������ �����մϴ� ����ġ ���� ������ ������ ���Ѵٸ� ���⸦ �ٲ�� �մϴ�

            // ofDamages ����ü����Ʈ �߿��� �������� 1�� ����ü�� ��� ��Ʈ�� �����մϴ�
            int randomattack = Random.Range(0, ofDamages.Count);


            // ofDamages ����ü ����Ʈ�� �����ϴ� randomattack��° ����ü�� numberOfNote�� �����ɴϴ�
            int notees = ofDamages[randomattack].numberOfNote;

            // notees ���ڸ�ŭ ��Ʈ�� �����ϱ����� �ݺ����� ������ 
            for (int ii = 0; ii < notees; ii++)
            {
                // StructOfFight ����ü�� ���� ��Ʈ ����Ʈ�� ��ġ�� �´� �����ڿ� �����, randomattack������ ������ ����ü�� �־��ݴϴ�
                StructOfFight ofFight = new(ofParty, PartyOfPlayer[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


                // �̺κ��� ��Ʈ 1���� ����ü 1���� �ִ� �κ��Դϴ�
                // ��Ʈ1���� �ٸ� ����ü�� �ְ� �ʹٸ� ������ �ݴϴ�

                // ofFight�� ����Ʈȭ �ؼ� �־��ݴϴ�
                List<StructOfFight> ofFights = new()
                {
                    ofFight
                };

                // �׸��� �ش� ofFight����ü�� �־��ݴϴ� 
                NoteManager.FightOfListList.Add(ofFights);
                //Debug.Log(NoteManager.FightOfListList.Count);
                // ����ü�� �־������� �־��� ��ŭ ��Ʈ�� Ȱ��ȭ ��ŵ�ϴ�

            }


        }
        NoteManager.NoteActive();
        //Debug.Log(223232323);

        //StartCoroutine(Shake((int)Phase.Battle));
    }
    
    /// <summary>
    /// ������ ����
    /// 
    /// </summary>
    public void BattleOfPhase()
    {
        intOfPhase = (int)Phase.Battle;
        StartCoroutine(Shake((int)Phase.End));
    }
    
    public void EndOfPhase()
    {
        intOfPhase = (int)Phase.End;
        StartCoroutine(Shake((int)Phase.Standby));
    }



    #endregion

    /// <summary>
    /// ����� �����ִ� ui�� ������
    /// </summary>
    public void UiOfPhase()
    {
        arrowOfPhase.transform.localPosition = vector3OfPhase[intOfPhase];
    }

    /// <summary>
    /// ������ ������ ǥ����
    /// </summary>
    public void EnemyStart()
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    public void BattleStart()
    {
        
    }

    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NoteManager.Check();

        }
    }

    IEnumerator Shake(int input)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        switch (input)
        {
            default:
            case (int)Phase.Standby:
                StandbyOfPhase();
                break;
            case (int)Phase.Main:
                MainOfPhase();
                break;
            case (int)Phase.Battle:
                BattleOfPhase();
                break;
            case (int)Phase.End:
                EndOfPhase();
                break;

        }

    }
}


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
