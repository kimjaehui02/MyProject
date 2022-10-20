using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    #region 매니저들
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

        Part2(true);
    }

    int tset = 0;

    private void Update()
    {
        NoteManager.NoteManagerUpdate();
        Check();

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
    /// 여러가지 초기화를 담당합니다
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


        // 초기화를 해주는 부분
        UiManager.listListSpriteRendererOfNoteP = new();
        UiManager.listListSpriteRendererOfNoteE = new();

        // 파티원 수만큼 포문을 반복
        for (int i = 0; i < UiManager.noteUis.transform.GetChild(0).childCount; i++)
        {
            // 첫 겟차일드는 플레이어와 적꺼를 구분용
            // 두번째 차일드는 파티에서도 1234중 1개 구분용
            // 세버내 차일드는 대상의 공격중에서 노트의 갯수용
            //UiManager.noteUis.transform.GetChild(0).GetChild(i);

            UiManager.listListSpriteRendererOfNoteP.Add(null);
            UiManager.listListSpriteRendererOfNoteP[i] = new();

            // 노트의 수만큼 포문을 반복
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

        // 파티원 수만큼 포문을 반복
        for (int i = 0; i < UiManager.noteUis.transform.GetChild(1).childCount; i++)
        {
            // 첫 겟차일드는 플레이어와 적꺼를 구분용
            // 두번째 차일드는 파티에서도 1234중 1개 구분용
            // 세버내 차일드는 대상의 공격중에서 노트의 갯수용
            //UiManager.noteUis.transform.GetChild(0).GetChild(i);

            UiManager.listListSpriteRendererOfNoteE.Add(null);
            UiManager.listListSpriteRendererOfNoteE[i] = new();

            // 노트의 수만큼 포문을 반복
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
    /// 개전
    /// </summary>
    public void BattleStart()
    {

    }

    #region 페이즈들
    /// <summary>
    /// 전투 사이클 시작마다 할 일
    /// </summary>
    public void StandbyPhase()
    {
        // 모든 플레이어를 행동가능한 상태로 합니다
        foreach(var i in UiManager.PartyOfPlayer)
        {
            //i.GetComponent<Player>().BoolOfPlayAble = true;
        }


        


        StartCoroutine(Shake((int)UiManager.Phase.Main));
    }

    /// <summary>
    /// 전투 전에 할일들
    /// </summary>
    public void MainPhase()
    {


        //List<List<StructOfFight>> structOfFights = new List<List<StructOfFight>>();

        EnemyStart();

        // 아군들의 기본 공격도 저장합니다

        //Debug.Log(223232323);

        StartCoroutine(Shake((int)UiManager.Phase.BattleForEnemy));
    }

    /// <summary>
    /// 전투가 시작
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
    /// 적들의 공격을 표시함
    /// </summary>
    public void EnemyStart()
    {
        // 적의 공격들이 얼마나 올지 표시합니다
        for (int i = 0; i < UiManager.PartyOfEnemy.Count; i++)
        {
            // 먼저 파티에 있는 스크립트를 가져오고
            ParentsOfParty ofParty = UiManager.PartyOfEnemy[i].GetComponent<ParentsOfParty>();

            // 해당 대상이 죽어 있다면 넘어가고
            if (ofParty.Dead == true)
            {
                continue;
            }

            // ofParty 스크립트가 가진 데미지 구조체 리스트를 가져오고
            List<StructOfDamage> ofDamages = ofParty.StructOfDamages;


            // 이 부분이 공격을 지정합니다 가중치 등의 이유로 수정을 원한다면 여기를 바꿔야 합니다

            // ofDamages 구조체리스트 중에서 랜덤으로 1개 구조체를 골라 인트로 저장합니다
            int randomattack = Random.Range(0, ofDamages.Count);


            // ofDamages 구조체 리스트에 존재하는 randomattack번째 구조체가 numberOfNote를 가져옵니다
            int notees = ofDamages[randomattack].numberOfNote;

            // notees 숫자만큼 노트를 생성하기위해 반복문을 돌리고 
            for (int ii = 0; ii < notees; ii++)
            {
                // StructOfFight 구조체를 만들어서 노트 리스트의 위치에 맞는 공격자와 방어자, randomattack번쨰의 데미지 구조체를 넣어줍니다
                StructOfFight ofFight = new(ofParty, UiManager.PartyOfPlayer[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


                // 이부분이 노트 1개에 구조체 1개를 넣는 부분입니다
                // 노트1개에 다른 구조체를 넣고 싶다면 수정해 줍니다

                // ofFight를 리스트화 해서 넣어줍니다
                List<StructOfFight> ofFights = new()
                {
                    ofFight
                };

                // 그리고 해당 ofFight구조체를 넣어줍니다 
                NoteManager.List2StructOfFightOfCastEnemy.Add(ofFights);
                //Debug.Log(NoteManager.List2StructOfFightOfCastEnemy.Count);
                // 구조체를 넣어줬으니 넣어준 만큼 노트를 활성화 시킵니다

            }


        }
    }


    #endregion

    #region 오브젝트 및 Ui들 위치를 설정용


    /// <summary>
    /// 페이즈를 보여주는 ui와 캐릭터의 스킬선택지들을 관리함
    /// </summary>
    public void UpdateOfUiPhaseAndSkill()
    {
        #region 데이터 조작
        // 페이즈 보기용 ui
        #region 캐릭터, 노트Ui, 스킬Ui위치 조정

        List<bool> vs = new();
        vs.Add(true);
        vs.Add(true);
        vs.Add(true);
        vs.Add(true);

        // 코드 간략화를 위해 스택을 짧게 정의합니다
        List<StruckOfAct> struckOfActs = new(UiManager.stackOfActs);

        // 예시용으로 보여줄 리스트를 만듭니다
        List<GameObject> examples = new(UiManager.PartyOfPlayer);

        if (UiManager.intOfUi != (int)UiManager.EnumOfUi.Note)
        {

            // 쌓인 구조체에 따라서 예시를 바꿉니다
            for (int i = 0; i < struckOfActs.Count; i++)
            {
                // 이동한 파티원의 행동포인트를 소모시킵니다
                vs[struckOfActs[i].attacker] = false;

                // 이동행동이라면 반영시킵니다
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

        #region enum을 기반으로한 int값으로 스킬선택지 활성화 관리용
        // 기본적 스킬 버튼들입니다
        bool nomallistListGameObjectOfSkills = false;
        // 이동을 결정하는 스킬 버튼입니다
        bool movelistListGameObjectOfSkills = false;
        // 대상을 지정하는 스킬 버튼입니다
        bool targetlistListGameObjectOfSkills = false;
        // 스킬들 Ui

        // 3가지 종류로 나뉩니다

        // 1. 아무것도 하지 않은 기본상태
        //      스킬을 지정할 수 있도록 모든 선택지가 활성화 되어있습니다

        // 2. 이동 버튼을 누른 상태
        //      이동이 가능하도록 이동가능한 버튼만 활성화 되어있습니다

        // 3. 전투 상태로 넘어간 상태
        //      스킬 선택창 자체가 사라져서 이제 공격 처리용 노트들이 나오기 시작합니다
        switch (UiManager.intOfUi)
        {
            // 기본
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


        #region Ui조작

        #region 플레이어의 위치 조정


        // 실제 오브젝트의 위치를 보입니다
        // 위치를 리스트 순서에 맞게 둡니다
        for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
        {

            UiManager.PartyOfEnemy[i].transform.localPosition = -UiManager.listOfPlayerTransform[i];
        }


        if (UiManager.intOfUi != (int)UiManager.EnumOfUi.Note)
        {

            // 이동중인 예시를 보입니다

            // 위치를 리스트 순서에 맞게 둡니다
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                examples[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
            //UiManager.exampleOfPlayerList = new(examples);
        }
        else
        {
            // 실제 오브젝트의 위치를 보입니다
            // 위치를 리스트 순서에 맞게 둡니다
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {

                UiManager.PartyOfPlayer[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }

        }
        #endregion

        #region 스킬선택지 위치 조정

        // Ui중에서도 스킬들을 관리합니다
        for (int i = 0; i < UiManager.listListGameObjectOfSkills.Count; i++)
        {
            // 뒤의 두 버튼은 이동과 대상지정을 위하므로 빼줍니다다
            for (int ii = 0; ii < 4; ii++)
            {
                // 일반적인 스킬들에 적용합니다
                UiManager.listListGameObjectOfSkills[i][ii].SetActive(nomallistListGameObjectOfSkills);
                // 일반적 선택지는 행동 가능유무에 따라서 버튼을 활성화 합니다
                UiManager.listListButtonOfSkills[i][ii].interactable = vs[i];

                //UiManager.exampleOfPlayerList[i].GetComponent<Player>().BoolOfPlayAble;
            }

            // 이동 선택용 선택지
            if(i != UiManager.intOfChange)
            {
                UiManager.listListGameObjectOfSkills[i][4].SetActive(movelistListGameObjectOfSkills);
            }


            // 대상 지정용 선택지
            UiManager.listListGameObjectOfSkills[i][5].SetActive(targetlistListGameObjectOfSkills);
        }
        #endregion

        #region 노트를 알려주는Ui조정

        // 공격을 알려주는 노트Ui를 수정합니다
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

    public void Part2(bool start = false)
    {
        if (Input.GetKeyDown(KeyCode.Tab) || start == true)
        {
            UpdateOfUiPhaseAndSkill();
            tset++;
            tset %= 4;

            for (int i = 0; i < 4; i++)
            {
                UiManager.PartyOfPlayer[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
                UiManager.PartyOfPlayer[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -2;

                UiManager.PartyOfEnemy[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = -1;
                UiManager.PartyOfEnemy[i].transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = -2;
            }


            UiManager.PartyOfPlayer[tset].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4;
            UiManager.PartyOfPlayer[tset].transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 3;

            UiManager.PartyOfEnemy[tset].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4;
            UiManager.PartyOfEnemy[tset].transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = 3;
        }
        UiManager.PartyOfPlayer[tset].transform.position = UiManager.target12[0].transform.position;
        UiManager.PartyOfEnemy[tset].transform.position = UiManager.target12[1].transform.position;
    }

    #region 구조체 생성파트

    /// <summary>
    /// ui에 달린 버튼이 작동할 함수입니다
    /// </summary>
    /// <param name="input"></param>
    public void ChangeButton1(int input)
    {

        // 자리 바꿀 준비를 한다고 알려줍니다
        UiManager.intOfUi = (int)UiManager.EnumOfUi.MoveSkill;
        // 자리를 바꾸는 사람을 알려줍니다
        UiManager.intOfChange = input;

        UpdateOfUiPhaseAndSkill();
    }

    /// <summary>
    /// 다시 눌러서 이동시킵니다
    /// </summary>
    /// <param name="input"></param>
    public void ChangeButton2(int input)
    {
        // 바꿈이 완료되었다고 표시합니다
        UiManager.intOfUi = (int)UiManager.EnumOfUi.AllSkill;
        // 새로운 구조체를 만듭니다
        StruckOfAct struckOfAct = new(UiManager.intOfChange, input, (int)StruckOfAct.typeOfAct.move);
        // 그 구조체를 넣어줘서 스택을 추가시킵니다
        UiManager.stackOfActs.Add(struckOfAct);


        UpdateOfUiPhaseAndSkill();

        //Debug.Log(UiManager.stackOfActs.Count);
    }

    public void Skill1(int input)
    {
        // 대상 지정을 합니다
        UiManager.intOfUi = (int)UiManager.EnumOfUi.TargetSkill;
        // 공격자를 정합니다
        UiManager.intOfAttaker = input;

        UpdateOfUiPhaseAndSkill();
    }

    public void Skill2(int input)
    {
        // 대상 지정을 합니다
        UiManager.intOfUi = (int)UiManager.EnumOfUi.AllSkill;
        



        // 새로운 구조체를 만듭니다
        StruckOfAct struckOfAct = new(UiManager.intOfAttaker, input, (int)StruckOfAct.typeOfAct.attack);


        // 그 구조체를 넣어줘서 스택을 추가시킵니다
        UiManager.stackOfActs.Add(struckOfAct);

        UpdateOfUiPhaseAndSkill();
    }

    #endregion
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
            UiManager.PartyOfPlayer[tset].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            UiManager.PartyOfEnemy[tset].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");

            StartCoroutine(Shaked2(UiManager.PartyOfPlayer[tset].transform.GetChild(1).GetComponent<SpriteRenderer>()));
            StartCoroutine(Shaked(UiManager.PartyOfEnemy[tset].transform.GetChild(1).GetComponent<SpriteRenderer>()));

            UiManager.target12[0].GetComponent<Rigidbody2D>().AddForce(new Vector3(-100, 0, 0));
            UiManager.target12[1].GetComponent<Rigidbody2D>().AddForce(new Vector3(300, 0, 0));
            DamageManager.SwordClip(0);
        }

        if (input == 1)
        {
            UiManager.PartyOfPlayer[tset].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            UiManager.PartyOfEnemy[tset].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");

            StartCoroutine(Shaked2(UiManager.PartyOfPlayer[tset].transform.GetChild(1).GetComponent<SpriteRenderer>()));
            StartCoroutine(Shaked2(UiManager.PartyOfEnemy[tset].transform.GetChild(1).GetComponent<SpriteRenderer>()));

            UiManager.target12[0].GetComponent<Rigidbody2D>().AddForce(new Vector3(-100, 0, 0));
            UiManager.target12[1].GetComponent<Rigidbody2D>().AddForce(new Vector3(100, 0, 0));
            DamageManager.SwordClip(1);
        }
    }

    #region 플레이어 행동스택

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


    #region 코루틴


    IEnumerator Shake(int input)
    {
        yield return new WaitForSecondsRealtime(0.5f);

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
            // 투명하게 만듭니다
            input -= 12;


            // 너무 투명하다면 되돌립니다
            if(input < 100)
            {
                dark = !dark;
            }


        }
        else
        {
            // 투명하게 만듭니다
            input += 12;


            // 너무 투명하다면 되돌립니다
            if (input > 255)
            {
                dark = !dark;
            }
        }
        // 투명한 색을 적용시킵니다
        color.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, input / 255f);

        // 재실행합니다
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


// 전투의 진행

// - 개전

// 적들과 아군이 등장


// - 전투중

// 적들의 공격이 표시
// 아군의 공격을 선택
// 적들의 공격을 실행
// 아군의 공격을 실행

// 정타 시간은 0.5f

// 정타 시간  = 범위 /속도;
