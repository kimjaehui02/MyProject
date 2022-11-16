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
        UpdateOfUiPhaseAndSkill();

        Part2(true);
        BattleStart();
    }


    private void Update()
    {
        NoteManager.NoteManagerUpdate();
        Check();

        ChagePhaseWorking();
        Part2();


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

    public GameObject Skills;

    public GameObject Lines;

    /// <summary>
    /// ����
    /// </summary>
    public void BattleStart()
    {
        ChageText();
        StartCoroutine(Shake((int)UiManager.Phase.Standby));

    }

    #region �������
    /// <summary>
    /// ���� ����Ŭ ���۸��� �� ��
    /// </summary>
    public void StandbyPhase()
    {
        // ��� �÷��̾ �ൿ������ ���·� �մϴ�
        foreach (var i in UiManager.PartyOfPlayer)
        {
            //i.GetComponent<Player>().BoolOfPlayAble = true;
        }




        ChageText("��������");
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

        ChageText("�����غ�");
        StartCoroutine(Shake((int)UiManager.Phase.BattleForEnemy));


    }

    /// <summary>
    /// ������ ����
    /// 
    /// </summary>
    public void BattlePhase1()
    {



        //NoteManager.NoteActive();
        ChageText("����");
        StartCoroutine(Shake((int)UiManager.Phase.End));
    }

    public void BattlePhase2()
    {



        NoteManager.NoteActive();



        //StartCoroutine(Shake((int)UiManager.Phase.End));
    }

    public void EndPhase()
    {

        //StartCoroutine(Shake((int)Phase.Standby));
    }


    // ReSharper disable Unity.PerformanceAnalysis
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
                // �̵��ൿ�� �ƴ϶�� ���⼭ ���ٰ� ������ �������� �ѱ�ϴ�
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


        // ���� ������Ʈ�� ��ġ�� ���Դϴ�
        // ��ġ�� ����Ʈ ������ �°� �Ӵϴ�
        for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
        {

            UiManager.PartyOfEnemy[i].transform.localPosition = -UiManager.listOfPlayerTransform[i];
        }


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
            if (i != UiManager.intOfChange)
            {
                UiManager.listListGameObjectOfSkills[i][4].SetActive(movelistListGameObjectOfSkills);
            }


            // ��� ������ ������
            UiManager.listListGameObjectOfSkills[i][5].SetActive(targetlistListGameObjectOfSkills);
        }
        #endregion

        #region ��Ʈ�� �˷��ִ�Ui����

        // ������ �˷��ִ� ��ƮUi�� �����մϴ�
        for (int i = 0; i < UiManager.listListSpriteRendererOfNoteP.Count; i++)
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

    /// <summary>
    /// �ϴ� ���� �Ʊ��� ��ü�ϱ����� �ӽÿ� �Լ��Դϴ�
    /// </summary>
    /// <param name="start"></param>
    public void Part2(bool start = false)
    {


        if (Input.GetKeyDown(KeyCode.Tab) || start == true)
        {
            UpdateOfUiPhaseAndSkill();
            UiManager.intOfFocus++;
            UiManager.intOfFocus %= 4;

            for (int i = 0; i < 4; i++)
            {
                UiManager.PartyOfPlayer[i].GetComponent<CharacterUi>().BoolOfFocus = false;

                UiManager.PartyOfEnemy[i].GetComponent<CharacterUi>().BoolOfFocus = false;
            }

            UiManager.PartyOfPlayer[UiManager.intOfFocus].GetComponent<CharacterUi>().BoolOfFocus = true;
            UiManager.PartyOfEnemy[UiManager.intOfFocus].GetComponent<CharacterUi>().BoolOfFocus = true;
        }

        if (UiManager.intOfPhase == (int)UiManager.Phase.BattleForEnemy)
        {
            UiManager.PartyOfPlayer[UiManager.intOfFocus].transform.position = UiManager.target12[0].transform.position;
            UiManager.PartyOfEnemy[UiManager.intOfFocus].transform.position = UiManager.target12[1].transform.position;
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




    /// <summary>
    /// ������ ��ȯ�� ȿ������ ����ϴ�
    /// </summary>
    public void ChagePhaseWorking()
    {
        // ������ �Ǿ�߸� �����
        if (UiManager.PhaseChagebool == false)
        {
            return;
        }
        UiManager.FadeOutOfUi.gameObject.SetActive(true);

        // ȭ����ȯ�� ���۵Ǵ� �ð�
        float start = 3.0f;
        // �Ʒ� �ð��� ���ļ� ȭ���� ��ο���
        float fadeintime = 3.5f;
        // ȭ���� ��ο��� ���¿��� �Ʒ� �ð���ŭ ����
        float none = 5.5f;


        // �������� ��ο����� �����Դϴ�
        float UiDark = 155f;

        // ����� �˷��ִ� �ؽ�Ʈ�� ������ ��ġ���Դϴ�
        //float textstart = 550;
        //float textend = 240;

        //float textcal = textstart - textend;

        // �ð� ������ ���ؼ� ��ŸŸ���� ����
        UiManager.PhaseChagefloat += Time.fixedDeltaTime;
        //Debug.Log(UiManager.PhaseChagefloat);

        // �ð��� ���� �ʾҴٸ� �������� ����
        if (UiManager.PhaseChagefloat < start)
        {
            return;
        }

        // ȭ���� ��Ӱ� ����
        if (start < UiManager.PhaseChagefloat && UiManager.PhaseChagefloat < start + fadeintime)
        {
            // ������¸� ������� ���߽��ϴ�
            float coloring = (UiManager.PhaseChagefloat - start) / fadeintime;

            // �������� ��⸦ �����մϴ�
            UiManager.FadeOutOfUi.color = new Color(0, 0, 0, (UiDark * coloring) / 255);

            UiManager.FadeOutOfText.GetComponent<Image>().color = new Color(255, 255, 255, (255 * coloring) / 255);
            UiManager.FadeOutOfText.transform.GetChild(0).GetComponent<Text>().color
                = new Color(255, 255, 255, (255 * coloring) / 255);
            // �ؽ�Ʈ�� ��ġ�� �����մϴ�
            //UiManager.FadeOutOfText.GetComponent<RectTransform>().localPosition 
            //    = new Vector3(0, textstart - (textend * coloring), 0) ;
        }

        // ����ó���� ª�� �ϱ����� ������
        float newstart = start + fadeintime + none;

        // ȭ���� ��� ����
        if (newstart < UiManager.PhaseChagefloat && UiManager.PhaseChagefloat < newstart + fadeintime)
        {
            //������¸� ������� ���߽��ϴ�
            float coloring = 1 - ((UiManager.PhaseChagefloat - newstart) / fadeintime);
            UiManager.FadeOutOfUi.color = new Color(0, 0, 0, (UiDark * coloring) / 255);


            UiManager.FadeOutOfText.GetComponent<Image>().color = new Color(255, 255, 255, (255 * coloring) / 255);
            UiManager.FadeOutOfText.transform.GetChild(0).GetComponent<Text>().color
                = new Color(255, 255, 255, (255 * coloring) / 255);
            // �ؽ�Ʈ�� ��ġ�� �����մϴ�1
            //UiManager.FadeOutOfText.GetComponent<RectTransform>().localPosition
            //    = new Vector3(0, textstart - (textend * coloring), 0);
        }   //

        // �ð��� ������ ������ �ʱ�ȭ�� ����
        if (newstart + fadeintime < UiManager.PhaseChagefloat)
        {
            UiManager.FadeOutOfUi.color = new Color(0, 0, 0, 0);
            UiManager.PhaseChagefloat = 0;
            UiManager.PhaseChagebool = false;

            // �ؽ�Ʈ�� ��ġ�� �����մϴ�
            //UiManager.FadeOutOfText.GetComponent<RectTransform>().localPosition
            //    = new Vector3(0, textstart, 0);
            UiManager.FadeOutOfText.GetComponent<Image>().color = new Color(255, 255, 255, (0) / 255);
            UiManager.FadeOutOfText.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, (0) / 255);

            UiManager.FadeOutOfUi.gameObject.SetActive(false);

            if (UiManager.intOfPhase ==(int)UiManager.Phase.Main)
            {
                SkillOn();
            }

            if (UiManager.intOfPhase == (int)UiManager.Phase.BattleForEnemy)
            {
                battleOn();
            }

            return;
        }



    }

    public void ChageText(string inputs = "����")
    {
        UiManager.FadeOutOfText.transform.GetChild(0).GetComponent<Text>().text = inputs;
        UiManager.PhaseChagebool = true;
    }

    #endregion


    public void Check()
    {
        int input = 0;
        if (Input.GetKeyDown(KeyCode.E))
        {
            input = NoteManager.Check();

        }

        if (input == 2)
        {
            UiManager.PartyOfPlayer[UiManager.intOfFocus].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            UiManager.PartyOfEnemy[UiManager.intOfFocus].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");

            StartCoroutine(Shaked2(UiManager.PartyOfPlayer[UiManager.intOfFocus].transform.GetChild(1).GetComponent<SpriteRenderer>()));
            StartCoroutine(Shaked(UiManager.PartyOfEnemy[UiManager.intOfFocus].transform.GetChild(1).GetComponent<SpriteRenderer>()));

            UiManager.target12[0].GetComponent<Rigidbody2D>().AddForce(new Vector3(-100, 0, 0));
            UiManager.target12[1].GetComponent<Rigidbody2D>().AddForce(new Vector3(300, 0, 0));
            DamageManager.SwordClip(0);
        }

        if (input == 1)
        {
            UiManager.PartyOfPlayer[UiManager.intOfFocus].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            UiManager.PartyOfEnemy[UiManager.intOfFocus].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");

            StartCoroutine(Shaked2(UiManager.PartyOfPlayer[UiManager.intOfFocus].transform.GetChild(1).GetComponent<SpriteRenderer>()));
            StartCoroutine(Shaked2(UiManager.PartyOfEnemy[UiManager.intOfFocus].transform.GetChild(1).GetComponent<SpriteRenderer>()));

            UiManager.target12[0].GetComponent<Rigidbody2D>().AddForce(new Vector3(-100, 0, 0));
            UiManager.target12[1].GetComponent<Rigidbody2D>().AddForce(new Vector3(100, 0, 0));
            DamageManager.SwordClip(1);
        }
    }

    #region �÷��̾� �ൿ����

    public void ESC()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UiManager.intOfUi == (int)UiManager.EnumOfUi.MoveSkill)
            {
                UiManager.intOfUi = (int)UiManager.EnumOfUi.AllSkill;
            }
            else
            {
                if (UiManager.stackOfActs.Count != 0)
                {
                    UiManager.stackOfActs.RemoveAt(UiManager.stackOfActs.Count - 1);
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

        //switch (input)
        //{
        //    default:
        //    case (int)UiManager.Phase.Standby:
        //        UiManager.intOfPhase = (int)UiManager.Phase.Main;
        //        //MainPhase();
        //        break;
        //    case (int)UiManager.Phase.Main:
        //        UiManager.intOfPhase = (int)UiManager.Phase.BattleForEnemy;
        //        //BattlePhase1();
        //        break;
        //    case (int)UiManager.Phase.BattleForEnemy:
        //        UiManager.intOfPhase = (int)UiManager.Phase.End;
        //        //EndPhase();
        //        break;
        //    //case (int)UiManager.Phase.BattleForPlayer:
        //    //    UiManager.intOfPhase = (int)UiManager.Phase.BattleForPlayer;
        //    //    BattlePhase2();
        //    //    break;
        //    case (int)UiManager.Phase.End:
        //        UiManager.intOfPhase = (int)UiManager.Phase.Standby;
        //        //StandbyPhase();
        //        break;
        //}

        yield return new WaitForSeconds(4.5f);
        //Debug.Log(123);
        //yield break;

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
            //case (int)UiManager.Phase.BattleForPlayer:
            //    UiManager.intOfPhase = (int)UiManager.Phase.BattleForPlayer;
            //    BattlePhase2();
            //    break;
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

        if (UiManager.intOfUi == (int)UiManager.EnumOfUi.MoveSkill)
        {
            color.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            yield break;
        }

        if (dark == true)
        {
            // �����ϰ� ����ϴ�
            input -= 12;


            // �ʹ� �����ϴٸ� �ǵ����ϴ�
            if (input < 100)
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

    IEnumerator Shaked(SpriteRenderer sprite, int shakeint = 0)
    {

        shakeint++;
        if (shakeint % 2 == 1)
        {
            sprite.gameObject.transform.localPosition = new Vector2(0.2f, 0);
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(-0.2f, 0);
        }

        sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 100 / 255f, 100 / 255f);



        if (shakeint != 5)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            StartCoroutine(Shaked(sprite, shakeint));
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(0, 0);
            sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            //shakeint = 0;

        }

    }

    IEnumerator Shaked2(SpriteRenderer sprite, int shakeint = 0)
    {

        shakeint++;
        if (shakeint % 2 == 1)
        {
            sprite.gameObject.transform.localPosition = new Vector2(0.2f, 0);
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(-0.2f, 0);
        }

        sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 100 / 255f);



        if (shakeint != 5)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            StartCoroutine(Shaked2(sprite, shakeint));
        }
        else
        {
            sprite.gameObject.transform.localPosition = new Vector2(0, 0);
            sprite.GetComponent<SpriteRenderer>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            //shakeint = 0;

        }

    }

    #endregion

    private void SkillOn()
    {
        Debug.Log(33322);
        Skills.SetActive(true);
    }

    public GameObject wrwwr2;

    private void battleOn()
    {

        Skills.SetActive(false);
        Lines.SetActive(true);
        wrwwr2.SetActive(true);
        StartCoroutine(waits());
    }

    IEnumerator waits()
    {
        Debug.Log(3312);
        yield return new WaitForSecondsRealtime(1.5f);

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(3311231232);

            NoteManager.NoteActive();
            yield return new WaitForSecondsRealtime(8.5f);
            Part2(true);

        }

    }
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
