using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        BattleAwake();
    }

    private void Start()
    {
        StartCoroutine(Shake((int)UiManager.Phase.Standby));
    }

    private void Update()
    {
        NoteManager.NoteManagerUpdate();
        Check();


        //UpdateOfUiPhaseAndSkill();
        ESC();
    }

    private void FixedUpdate()
    {
        NoteManager.NoteManagerFixedUpdate();
    }

    #endregion

    /// <summary>
    /// �������� �ʱ�ȭ�� ����մϴ�
    /// </summary>
    public void BattleAwake()
    {
        UiManager.listListGameObjectOfSkills = new List<List<GameObject>>();
        UiManager.listListButtonOfSkills = new List<List<Button>>();
        UiManager.stackOfActs = new List<StruckOfAct>();

        UiManager.vs = new();

        for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
        {
            UiManager.vs.Add(true);
            UiManager.PartyOfPlayer[i] = UiManager
                .teamOfPlayer
                .transform
                .GetChild(i)
                .gameObject;

            UiManager.exampleOfPlayerList[i] = UiManager
                .teamOfPlayer
                .transform
                .GetChild(i)
                .gameObject;

            UiManager.PartyOfEnemy[i] = UiManager
                .teamOfEnemy
                .transform
                .GetChild(i)
                .gameObject;

            UiManager.listListGameObjectOfSkills.Add(null);
            UiManager.listListButtonOfSkills.Add(null);
            UiManager.listListGameObjectOfSkills[i] = new List<GameObject>();
            UiManager.listListButtonOfSkills[i] = new List<Button>();

            for (int ii = 0; ii < UiManager.parrentOfSkill.GetChild(i).transform.childCount; ii++)
            {

                UiManager.listListGameObjectOfSkills[i]
                    .Add(
                    UiManager
                    .parrentOfSkill
                    .GetChild(i)
                    .transform
                    .GetChild(ii)
                    .gameObject);

                UiManager.listListButtonOfSkills[i]
                    .Add(
                    UiManager
                    .listListGameObjectOfSkills[i][ii]
                    .GetComponent<Button>());
            }

        }

    }

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
        // ��� �÷��̾ �ൿ������ ���·� �մϴ�
        foreach(var i in UiManager.PartyOfPlayer)
        {
            //i.GetComponent<Player>().BoolOfPlayAble = true;
        }


        


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


    #endregion

    #region ������Ʈ �� Ui�� ��ġ�� ������


    /// <summary>
    /// ����� �����ִ� ui�� ĳ������ ��ų���������� ������
    /// </summary>
    public void UpdateOfUiPhaseAndSkill()
    {
        // ������ ����� ui
        UiManager.arrowOfPhase.transform.localPosition = UiManager.vector3OfPhase[UiManager.intOfPhase];


        List<bool> vs = new();
        vs.Add(true);
        vs.Add(true);
        vs.Add(true);
        vs.Add(true);

        if (UiManager.intOfUi != (int)UiManager.EnumOfUi.Note)
        {
            // �ڵ� ����ȭ�� ���� ������ ª�� �����մϴ�
            List<StruckOfAct> struckOfActs = new(UiManager.stackOfActs);
            // ���� ���� �÷��̾��Ʈ�� ª�� �����մϴ�
            List<GameObject> examples = new(UiManager.PartyOfPlayer);



            // ���� ����ü�� ���� ���ø� �ٲߴϴ�
            for (int i = 0; i < struckOfActs.Count; i++)
            {
                // �̵��� ��Ƽ���� �ൿ����Ʈ�� �Ҹ��ŵ�ϴ�
                vs[struckOfActs[i].attacker] = false;

                // �̵��ൿ�̶�� �ݿ���ŵ�ϴ�
                if (struckOfActs[i].type != (int)StruckOfAct.typeOfAct.move)
                {
                    continue;
                }

                int A = struckOfActs[i].attacker;
                int B = struckOfActs[i].defender;

                var tmp = examples[A];
                examples[A] = examples[B];
                examples[B] = tmp;

                var tmp2 = vs[A];
                vs[A] = vs[B];
                vs[B] = tmp2;

                
            }


            // �̵����� ���ø� ���Դϴ�

            // ��ġ�� ����Ʈ ������ �°� �Ӵϴ�
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                examples[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
            UiManager.exampleOfPlayerList = new(examples);
        }
        else
        {
            // ���� ������Ʈ�� ��ġ�� ���Դϴ�
            // ��ġ�� ����Ʈ ������ �°� �Ӵϴ�
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                UiManager.PartyOfPlayer[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
        }

        // ��ų�� Ui

        // 3���� ������ �����ϴ�

        // 1. �ƹ��͵� ���� ���� �⺻����
        //      ��ų�� ������ �� �ֵ��� ��� �������� Ȱ��ȭ �Ǿ��ֽ��ϴ�

        // 2. �̵� ��ư�� ���� ����
        //      �̵��� �����ϵ��� �̵������� ��ư�� Ȱ��ȭ �Ǿ��ֽ��ϴ�

        // 3. ���� ���·� �Ѿ ����
        //      ��ų ����â ��ü�� ������� ���� ���� ó���� ��Ʈ���� ������ �����մϴ�
        switch (UiManager.intOfUi)
        {
            // �⺻
            case (int)UiManager.EnumOfUi.AllSkill:

                // Ui�߿����� ��ų���� �����մϴ�
                for (int i = 0; i < UiManager.listListGameObjectOfSkills.Count; i++)
                {
                    // ������ �������� �ٸ��� ����Ǿ� �ϹǷ� -1�� ���ݴϴ�
                    for (int ii = 0; ii < UiManager.listListGameObjectOfSkills[i].Count - 1; ii++)
                    {
                        UiManager.listListGameObjectOfSkills[i][ii].SetActive(true);
                        // �Ϲ��� �������� �ൿ ���������� ���� ��ư�� Ȱ��ȭ �մϴ�
                        UiManager.listListButtonOfSkills[i][ii].interactable = vs[i];
                            //UiManager.exampleOfPlayerList[i].GetComponent<Player>().BoolOfPlayAble;
                    }

                    // �̵� ���ÿ� �������� ��Ȱ��ȭ
                    UiManager.listListGameObjectOfSkills[i][^2].SetActive(false);
                    // ��� ������ �������� ��Ȱ��ȭ
                    UiManager.listListGameObjectOfSkills[i][^1].SetActive(false);
                }


                break;
            case (int)UiManager.EnumOfUi.MoveSkill:

                // Ui�߿����� ��ų���� �����մϴ�
                for (int i = 0; i < UiManager.listListGameObjectOfSkills.Count; i++)
                {
                    // ������ �������� �ٸ��� ����Ǿ� �ϹǷ� -1�� ���ݴϴ�
                    for (int ii = 0; ii < UiManager.listListGameObjectOfSkills[i].Count - 1; ii++)
                    {
                        // �Ϲ��� �������� Ȱ��ȭ
                        UiManager.listListGameObjectOfSkills[i][ii].SetActive(false);
                    }

                    // �̹� �� ��ȣ�� ��ü�� ��Ȱ��ȭ���·� �Ӵϴ�
                    if (i == UiManager.intOfChange)
                    {
                        continue;
                    }

                    // �̵� ���ÿ� �������� ��Ȱ��ȭ
                    UiManager.listListGameObjectOfSkills[i][^2].SetActive(true);
                    // ��� ������ �������� ��Ȱ��ȭ
                    UiManager.listListGameObjectOfSkills[i][^1].SetActive(false);
                }


                break;

            case (int)UiManager.EnumOfUi.TargetSkill:
                break;

            case (int)UiManager.EnumOfUi.Note:
                break;
            default:
                break;

        }



        

    }


    #region ����ü ������Ʈ

    /// <summary>
    /// ui�� �޸� ��ư�� �۵��� �Լ��Դϴ�
    /// </summary>
    /// <param name="input"></param>
    public void ChangeButton1(int input)
    {
        // �ڸ� �ٲ� �غ� �Ѵٰ� �˷��ݴϴ�
        UiManager.intOfUi = (int)UiManager.EnumOfUi.MoveSkill;
        // �ڸ��� �ٲٴ� ����� �˷��ݴϴ�
        UiManager.intOfChange = input;

        UpdateOfUiPhaseAndSkill();
    }

    /// <summary>
    /// �ٽ� ������ �̵���ŵ�ϴ�
    /// </summary>
    /// <param name="input"></param>
    public void ChangeButton2(int input)
    {
        // �ٲ��� �Ϸ�Ǿ��ٰ� ǥ���մϴ�
        UiManager.intOfUi = (int)UiManager.EnumOfUi.AllSkill;
        // ���ο� ����ü�� ����ϴ�
        StruckOfAct struckOfAct = new(UiManager.intOfChange, input, (int)StruckOfAct.typeOfAct.move);
        // �� ����ü�� �־��༭ ������ �߰���ŵ�ϴ�
        UiManager.stackOfActs.Add(struckOfAct);


        UpdateOfUiPhaseAndSkill();

        //Debug.Log(UiManager.stackOfActs.Count);
    }

    public void Skill()
    {

    }

    #endregion
    #endregion


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
            if(UiManager.intOfUi == (int)UiManager.EnumOfUi.MoveSkill)
            {
                UiManager.intOfUi = (int)UiManager.EnumOfUi.AllSkill;
            }
            else
            {
                if(UiManager.stackOfActs.Count != 0)
                {
                    UiManager.stackOfActs.RemoveAt(UiManager.stackOfActs.Count-1);
                }
                
            }
            UpdateOfUiPhaseAndSkill();
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

    IEnumerator Flicker(SpriteRenderer color, int input = 255, bool dark = true)
    {
        yield return new WaitForSeconds(0.05f);
        //Debug.Log(123);

        if(UiManager.intOfUi == (int)UiManager.EnumOfUi.MoveSkill)
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
        StartCoroutine(Flicker(color, input, dark));

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
