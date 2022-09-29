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

    public GameObject teamOfPlayer;

    public GameObject teamOfEnemy;

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

    public enum Phase { Standby, Main, BattleForEnemy, BattleForPlayer, End }

    public int intOfPhase;

    public List<Vector3> vector3OfPhase;

    #endregion

    #endregion

    #region Default
    private void Awake()
    {
        for (int  i=0; i< 4; i++)
        {
            PartyOfPlayer[i] = teamOfPlayer.transform.GetChild(i).gameObject;
            PartyOfEnemy[i] = teamOfEnemy.transform.GetChild(i).gameObject;
        }
    }

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
    public void StandbyPhase()
    {
        

        


        StartCoroutine(Shake((int)Phase.Main));
    }

    /// <summary>
    /// ���� ���� ���ϵ�
    /// </summary>
    public void MainPhase()
    {
        

        //List<List<StructOfFight>> structOfFights = new List<List<StructOfFight>>();

        // ���� ���ݵ��� �󸶳� ���� ǥ���մϴ�
        for (int i = 0; i < PartyOfEnemy.Count; i++)
        {
            // ���� ��Ƽ�� �ִ� ��ũ��Ʈ�� ��������
            ParentsOfParty ofParty = PartyOfEnemy[i].GetComponent<ParentsOfParty>();

            // �ش� ����� �׾� �ִٸ� �Ѿ��
            if (ofParty.Dead == true)
            {
                continue;
            }

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
                NoteManager.AttackListOfEnemy.Add(ofFights);
                //Debug.Log(NoteManager.AttackListOfEnemy.Count);
                // ����ü�� �־������� �־��� ��ŭ ��Ʈ�� Ȱ��ȭ ��ŵ�ϴ�

            }


        }

        // �Ʊ����� �⺻ ���ݵ� �����մϴ�
        for (int i = 0; i < PartyOfEnemy.Count; i++)
        {
            // ���� ��Ƽ�� �ִ� ��ũ��Ʈ�� ��������
            ParentsOfParty ofParty = PartyOfPlayer[i].GetComponent<ParentsOfParty>();

            // �ش� ����� �׾� �ִٸ� �Ѿ��
            if (ofParty.Dead == true)
            {
                continue;
            }

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
                StructOfFight ofFight = new(ofParty, PartyOfEnemy[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


                // �̺κ��� ��Ʈ 1���� ����ü 1���� �ִ� �κ��Դϴ�
                // ��Ʈ1���� �ٸ� ����ü�� �ְ� �ʹٸ� ������ �ݴϴ�

                // ofFight�� ����Ʈȭ �ؼ� �־��ݴϴ�
                List<StructOfFight> ofFights = new()
                {
                    ofFight
                };

                // �׸��� �ش� ofFight����ü�� �־��ݴϴ� 
                NoteManager.AttackListOfPlayer.Add(ofFights);
                //Debug.Log(NoteManager.AttackListOfEnemy.Count);
                // ����ü�� �־������� �־��� ��ŭ ��Ʈ�� Ȱ��ȭ ��ŵ�ϴ�

            }


        }

        //Debug.Log(223232323);

        StartCoroutine(Shake((int)Phase.BattleForEnemy));
    }

    /// <summary>
    /// ������ ����
    /// 
    /// </summary>
    public void BattlePhase1()
    {
        


        NoteManager.NoteActive();

        StartCoroutine(Shake((int)Phase.End));
    }

    public void BattlePhase2()
    {
        


        NoteManager.NoteActive();



        StartCoroutine(Shake((int)Phase.End));
    }

    public void EndPhase()
    {
        
        //StartCoroutine(Shake((int)Phase.Standby));
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
                intOfPhase = (int)Phase.Standby;
                StandbyPhase();
                break;
            case (int)Phase.Main:
                intOfPhase = (int)Phase.Main;
                MainPhase();
                break;
            case (int)Phase.BattleForEnemy:
                intOfPhase = (int)Phase.BattleForEnemy;
                BattlePhase1();
                break;
            case (int)Phase.BattleForPlayer:
                intOfPhase = (int)Phase.BattleForPlayer;
                BattlePhase2();
                break;
            case (int)Phase.End:
                intOfPhase = (int)Phase.End;
                EndPhase();
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
