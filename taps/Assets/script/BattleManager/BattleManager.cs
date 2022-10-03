using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for (int i = 0; i < UiManager.PartyOfEnemy.Count; i++)
        {
            // 먼저 파티에 있는 스크립트를 가져오고
            ParentsOfParty ofParty = UiManager.PartyOfPlayer[i].GetComponent<ParentsOfParty>();

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
                StructOfFight ofFight = new(ofParty, UiManager.PartyOfEnemy[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


                // 이부분이 노트 1개에 구조체 1개를 넣는 부분입니다
                // 노트1개에 다른 구조체를 넣고 싶다면 수정해 줍니다

                // ofFight를 리스트화 해서 넣어줍니다
                List<StructOfFight> ofFights = new()
                {
                    ofFight
                };

                // 그리고 해당 ofFight구조체를 넣어줍니다 
                NoteManager.AttackListOfPlayer.Add(ofFights);
                //Debug.Log(NoteManager.AttackListOfEnemy.Count);
                // 구조체를 넣어줬으니 넣어준 만큼 노트를 활성화 시킵니다

            }


        }

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
                NoteManager.AttackListOfEnemy.Add(ofFights);
                //Debug.Log(NoteManager.AttackListOfEnemy.Count);
                // 구조체를 넣어줬으니 넣어준 만큼 노트를 활성화 시킵니다

            }


        }
    }

    /// <summary>
    /// 페이즈를 보여주는 ui를 조절함
    /// </summary>
    public void UiOfPhase()
    {
        UiManager.arrowOfPhase.transform.localPosition = UiManager.vector3OfPhase[UiManager.intOfPhase];
    }
    #endregion


    /// <summary>
    /// 리스트에 따라서 파티원을 좌표에 맞게 둡니다
    /// </summary>
    public void PlaceOfParty()
    {
        if(UiManager.boolOfExamplePlayer == true)
        {
            // 위치를 리스트 순서에 맞게 둡니다
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                UiManager.exampleOfPlayerList[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
        }
        else
        {
            // 위치를 리스트 순서에 맞게 둡니다
            for (int i = 0; i < UiManager.PartyOfPlayer.Count; i++)
            {
                UiManager.PartyOfPlayer[i].transform.localPosition = UiManager.listOfPlayerTransform[i];
            }
        }
    }

    /// <summary>
    /// ui에 달린 버튼이 작동할 함수입니다
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

    #region 플레이어 행동스택

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


    #region 코루틴


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
