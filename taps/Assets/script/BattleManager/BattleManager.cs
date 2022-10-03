using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region �Ŵ�����
    public DamageManager DamageManager;

    public NoteManager NoteManager;

    public SkillManager SkillManager;

    public UiManager UiManager;


    #endregion


    #region Default
    private void Awake()
    {
        for (int  i=0; i< 4; i++)
        {
            UiManager.PartyOfPlayer[i] = UiManager.teamOfPlayer.transform.GetChild(i).gameObject;
            UiManager.exampleOfPlayerList[i] = UiManager.teamOfPlayer.transform.GetChild(i).gameObject;
            UiManager.PartyOfEnemy[i] = UiManager.teamOfEnemy.transform.GetChild(i).gameObject;
        }

    }

    private void Start()
    {
        StartCoroutine(Shake((int)UiManager.Phase.Standby));
    }

    private void Update()
    {
        NoteManager.NoteManagerUpdate();
        Check();
        UiOfPhase();
        PlaceOfParty();
    }

    private void FixedUpdate()
    {
        NoteManager.NoteManagerFixedUpdate();
    }

    #endregion

    /// <summary>
    /// ����
    /// </summary>
    public void BattleStart()
    {

    }

    #region �������
    /// <summary>
    /// ���� ����Ŭ ���۸��� �� ��
    /// </summary>
    public void StandbyPhase()
    {
        

        


        StartCoroutine(Shake((int)UiManager.Phase.Main));
    }

    /// <summary>
    /// ���� ���� ���ϵ�
    /// </summary>
    public void MainPhase()
    {


        //List<List<StructOfFight>> structOfFights = new List<List<StructOfFight>>();

        EnemyStart();

        // �Ʊ����� �⺻ ���ݵ� �����մϴ�
        for (int i = 0; i < UiManager.PartyOfEnemy.Count; i++)
        {
            // ���� ��Ƽ�� �ִ� ��ũ��Ʈ�� ��������
            ParentsOfParty ofParty = UiManager.PartyOfPlayer[i].GetComponent<ParentsOfParty>();

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
                StructOfFight ofFight = new(ofParty, UiManager.PartyOfEnemy[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


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

        StartCoroutine(Shake((int)UiManager.Phase.BattleForEnemy));
    }

    /// <summary>
    /// ������ ����
    /// 
    /// </summary>
    public void BattlePhase1()
    {
        


        NoteManager.NoteActive();

        StartCoroutine(Shake((int)UiManager.Phase.End));
    }

    public void BattlePhase2()
    {
        


        NoteManager.NoteActive();



        StartCoroutine(Shake((int)UiManager.Phase.End));
    }

    public void EndPhase()
    {
        
        //StartCoroutine(Shake((int)Phase.Standby));
    }


    /// <summary>
    /// ������ ������ ǥ����
    /// </summary>
    public void EnemyStart()
    {
        // ���� ���ݵ��� �󸶳� ���� ǥ���մϴ�
        for (int i = 0; i < UiManager.PartyOfEnemy.Count; i++)
        {
            // ���� ��Ƽ�� �ִ� ��ũ��Ʈ�� ��������
            ParentsOfParty ofParty = UiManager.PartyOfEnemy[i].GetComponent<ParentsOfParty>();

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
                StructOfFight ofFight = new(ofParty, UiManager.PartyOfPlayer[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


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
    }

    /// <summary>
    /// ����� �����ִ� ui�� ������
    /// </summary>
    public void UiOfPhase()
    {
        UiManager.arrowOfPhase.transform.localPosition = UiManager.vector3OfPhase[UiManager.intOfPhase];
    }
    #endregion


    /// <summary>
    /// ����Ʈ�� ���� ��Ƽ���� ��ǥ�� �°� �Ӵϴ�
    /// </summary>
    public void PlaceOfParty()
    {
        if(UiManager.boolOfExamplePlayer == true)
        {
            // ��ġ�� ����Ʈ ������ �°� �Ӵϴ�
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                UiManager.exampleOfPlayerList[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
        }
        else
        {
            // ��ġ�� ����Ʈ ������ �°� �Ӵϴ�
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                UiManager.PartyOfPlayer[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
        }
    }

    /// <summary>
    /// ui�� �޸� ��ư�� �۵��� �Լ��Դϴ�
    /// </summary>
    /// <param name="input"></param>
    public void ChangeButton1(int input)
    {
    }

    public void ChangeButton2(int input)
    {

    }

    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NoteManager.Check();

        }
    }

    #region �÷��̾� �ൿ����

    public void ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiManager.stackOfActs.Pop();
        }

    }

    public void PushOfStack()
    {


    }


    #endregion


    #region �ڷ�ƾ


    IEnumerator Shake(int input)
    {
        yield return new WaitForSecondsRealtime(0.5f);

        switch (input)
        {
            default:
            case (int)UiManager.Phase.Standby:
                UiManager.intOfPhase = (int)UiManager.Phase.Standby;
                StandbyPhase();
                break;
            case (int)UiManager.Phase.Main:
                UiManager.intOfPhase = (int)UiManager.Phase.Main;
                MainPhase();
                break;
            case (int)UiManager.Phase.BattleForEnemy:
                UiManager.intOfPhase = (int)UiManager.Phase.BattleForEnemy;
                BattlePhase1();
                break;
            case (int)UiManager.Phase.BattleForPlayer:
                UiManager.intOfPhase = (int)UiManager.Phase.BattleForPlayer;
                BattlePhase2();
                break;
            case (int)UiManager.Phase.End:
                UiManager.intOfPhase = (int)UiManager.Phase.End;
                EndPhase();
                break;

        }

    }

    IEnumerator flicker(SpriteRenderer color, int input = 255, bool dark = true)
    {
        yield return new WaitForSeconds(0.05f);
        //Debug.Log(123);

        if(UiManager.boolOfExamplePlayer == false)
        {
            color.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            yield break;
        }

        if(dark == true)
        {
            // �����ϰ� ����ϴ�
            input -= 12;


            // �ʹ� �����ϴٸ� �ǵ����ϴ�
            if(input < 100)
            {
                dark = !dark;
            }


        }
        else
        {
            // �����ϰ� ����ϴ�
            input += 12;


            // �ʹ� �����ϴٸ� �ǵ����ϴ�
            if (input > 255)
            {
                dark = !dark;
            }
        }
        // ������ ���� �����ŵ�ϴ�
        color.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, input / 255f);

        // ������մϴ�
        StartCoroutine(flicker(color, input, dark));

    }

    #endregion
}


public struct StruckOfAct
{
    public enum typeOfAct
    {
        move, attack
    }
    public int type;

    public int attacker;

    public int defender;



    public StruckOfAct(int att, int def, int typ)
    {
        attacker = att;
        defender = def;
        type = typ;
    }

}


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
