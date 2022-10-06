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
        UpdateOfUiPhaseAndSkill();
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

            //UiManager.exampleOfPlayerList[i] = UiManager
                //.teamOfPlayer
                //.transform
                //.GetChild(i)
                //.gameObject;

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


        // �ʱ�ȭ�� ���ִ� �κ�
        UiManager.listListSpriteRendererOfNoteP = new();
        UiManager.listListSpriteRendererOfNoteE = new();

        // ��Ƽ�� ����ŭ ������ �ݺ�
        for (int i = 0; i < UiManager.noteUis.transform.GetChild(0).childCount; i++)
        {
            // ù �����ϵ�� �÷��̾�� ������ ���п�
            // �ι�° ���ϵ�� ��Ƽ������ 1234�� 1�� ���п�
            // ������ ���ϵ�� ����� �����߿��� ��Ʈ�� ������
            //UiManager.noteUis.transform.GetChild(0).GetChild(i);

            UiManager.listListSpriteRendererOfNoteP.Add(null);
            UiManager.listListSpriteRendererOfNoteP[i] = new();

            // ��Ʈ�� ����ŭ ������ �ݺ�
            for (int ii = 0; ii < UiManager.noteUis.transform.GetChild(0).GetChild(i).childCount; ii++)
            {
                UiManager.listListSpriteRendererOfNoteP[i].Add(
                    UiManager
                    .noteUis
                    .transform
                    .GetChild(0).GetChild(i).GetChild(ii)
                    .GetComponent<Image>());
            }
        }

        // ��Ƽ�� ����ŭ ������ �ݺ�
        for (int i = 0; i < UiManager.noteUis.transform.GetChild(1).childCount; i++)
        {
            // ù �����ϵ�� �÷��̾�� ������ ���п�
            // �ι�° ���ϵ�� ��Ƽ������ 1234�� 1�� ���п�
            // ������ ���ϵ�� ����� �����߿��� ��Ʈ�� ������
            //UiManager.noteUis.transform.GetChild(0).GetChild(i);

            UiManager.listListSpriteRendererOfNoteE.Add(null);
            UiManager.listListSpriteRendererOfNoteE[i] = new();

            // ��Ʈ�� ����ŭ ������ �ݺ�
            for (int ii = 0; ii < UiManager.noteUis.transform.GetChild(1).GetChild(i).childCount; ii++)
            {
                UiManager.listListSpriteRendererOfNoteE[i].Add(
                    UiManager
                    .noteUis
                    .transform
                    .GetChild(1).GetChild(i).GetChild(ii)
                    .GetComponent<Image>());
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
                NoteManager.List2StructOfFightOfCastEnemy.Add(ofFights);
                //Debug.Log(NoteManager.List2StructOfFightOfCastEnemy.Count);
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
        #region ������ ����
        // ������ ����� ui
        #region ĳ����, ��ƮUi, ��ųUi��ġ ����

        List<bool> vs = new();
        vs.Add(true);
        vs.Add(true);
        vs.Add(true);
        vs.Add(true);

        // �ڵ� ����ȭ�� ���� ������ ª�� �����մϴ�
        List<StruckOfAct> struckOfActs = new(UiManager.stackOfActs);

        // ���ÿ����� ������ ����Ʈ�� ����ϴ�
        List<GameObject> examples = new(UiManager.PartyOfPlayer);

        if (UiManager.intOfUi != (int)UiManager.EnumOfUi.Note)
        {

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
        }
        #endregion

        #region enum�� ��������� int������ ��ų������ Ȱ��ȭ ������
        // �⺻�� ��ų ��ư���Դϴ�
        bool nomallistListGameObjectOfSkills = false;
        // �̵��� �����ϴ� ��ų ��ư�Դϴ�
        bool movelistListGameObjectOfSkills = false;
        // ����� �����ϴ� ��ų ��ư�Դϴ�
        bool targetlistListGameObjectOfSkills = false;
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

                nomallistListGameObjectOfSkills = true;
                movelistListGameObjectOfSkills = false;
                targetlistListGameObjectOfSkills = false;
                break;

            case (int)UiManager.EnumOfUi.MoveSkill:

                nomallistListGameObjectOfSkills = false;
                movelistListGameObjectOfSkills = true;
                targetlistListGameObjectOfSkills = false;
                break;

            case (int)UiManager.EnumOfUi.TargetSkill:
                break;

            case (int)UiManager.EnumOfUi.Note:
                break;
            default:
                break;

        }
        #endregion

        #endregion
        UiManager.arrowOfPhase.transform.localPosition = UiManager.vector3OfPhase[UiManager.intOfPhase];


        #region Ui����

        #region �÷��̾��� ��ġ ����

        if (UiManager.intOfUi != (int)UiManager.EnumOfUi.Note)
        {

            // �̵����� ���ø� ���Դϴ�

            // ��ġ�� ����Ʈ ������ �°� �Ӵϴ�
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                examples[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
            //UiManager.exampleOfPlayerList = new(examples);
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
        #endregion

        #region ��ų������ ��ġ ����

        // Ui�߿����� ��ų���� �����մϴ�
        for (int i = 0; i < UiManager.listListGameObjectOfSkills.Count; i++)
        {
            // ���� �� ��ư�� �̵��� ��������� ���ϹǷ� ���ݴϴٴ�
            for (int ii = 0; ii < 4; ii++)
            {
                // �Ϲ����� ��ų�鿡 �����մϴ�
                UiManager.listListGameObjectOfSkills[i][ii].SetActive(nomallistListGameObjectOfSkills);
                // �Ϲ��� �������� �ൿ ���������� ���� ��ư�� Ȱ��ȭ �մϴ�
                UiManager.listListButtonOfSkills[i][ii].interactable = vs[i];

                //UiManager.exampleOfPlayerList[i].GetComponent<Player>().BoolOfPlayAble;
            }

            // �̵� ���ÿ� ������
            if(i != UiManager.intOfChange)
            {
                UiManager.listListGameObjectOfSkills[i][4].SetActive(movelistListGameObjectOfSkills);
            }


            // ��� ������ ������
            UiManager.listListGameObjectOfSkills[i][5].SetActive(targetlistListGameObjectOfSkills);
        }
        #endregion

        #region ��Ʈ�� �˷��ִ�Ui����

        // ������ �˷��ִ� ��ƮUi�� �����մϴ�
        for (int i =0; i < UiManager.listListSpriteRendererOfNoteP.Count; i++)
        {
            //Debug.Log(UiManager.listListSpriteRendererOfNoteP[i].Count + " " + UiManager.listIntOfNoteEachP[i]);

            for (int ii = 0; ii < UiManager.listIntOfNoteEachP[i]; ii++)
            {
                UiManager.listListSpriteRendererOfNoteP[i][ii].color = UiManager.colorOfNoteUi[1];
            }

            for (int ii = UiManager.listIntOfNoteEachP[i]; ii < UiManager.listListSpriteRendererOfNoteP[i].Count; ii++)
            {
                UiManager.listListSpriteRendererOfNoteP[i][ii].color = UiManager.colorOfNoteUi[0];
            }
        }
        #endregion

        #endregion

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

    public void Skill1(int input)
    {
        // ��� ������ �մϴ�
        UiManager.intOfUi = (int)UiManager.EnumOfUi.TargetSkill;
        // �����ڸ� ���մϴ�
        UiManager.intOfAttaker = input;

        UpdateOfUiPhaseAndSkill();
    }

    public void Skill2(int input)
    {
        // ��� ������ �մϴ�
        UiManager.intOfUi = (int)UiManager.EnumOfUi.AllSkill;
        



        // ���ο� ����ü�� ����ϴ�
        StruckOfAct struckOfAct = new(UiManager.intOfAttaker, input, (int)StruckOfAct.typeOfAct.attack);


        // �� ����ü�� �־��༭ ������ �߰���ŵ�ϴ�
        UiManager.stackOfActs.Add(struckOfAct);

        UpdateOfUiPhaseAndSkill();
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
