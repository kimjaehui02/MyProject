using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region 매니저들
    public DamageManager damageManager;

    public NoteManager NoteManager;

    public SkillManager SkillManager;
    #endregion

    #region 오브젝트들

    [field: SerializeField]
    /// <summary>
    /// 아군 파티
    /// </summary>
    public List<GameObject> PartyOfPlayer { get; set; }

    [field: SerializeField]
    /// <summary>
    /// 적군 파티
    /// </summary>
    public List<GameObject> PartyOfEnemy { get; set; }

    /// <summary>
    /// ui를 가리키는 화살표
    /// </summary>
    public GameObject arrowOfPhase;
    #endregion

    #region 변수들

    #region 전투 변수용
    /// <summary>
    /// 남은 공격 횟수
    /// </summary>
    [field: SerializeField]
    public float pointOfAttack { get; set; }



    #endregion

    #region 페이즈용

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

    #region 페이즈들
    /// <summary>
    /// 전투 사이클 시작마다 할 일
    /// </summary>
    public void StandbyOfPhase()
    {
        intOfPhase = (int)Phase.Standby;

        


        StartCoroutine(Shake((int)Phase.Main));
    }

    /// <summary>
    /// 전투 전에 할일들
    /// </summary>
    public void MainOfPhase()
    {
        intOfPhase = (int)Phase.Main;

        //List<List<StructOfFight>> structOfFights = new List<List<StructOfFight>>();

        // 적의 공격들이 얼마나 올지 표시합니다
        for (int i = 0; i < PartyOfEnemy.Count; i++)
        {
            // 먼저 파티에 있는 스크립트를 가져오고
            ParentsOfParty ofParty = PartyOfEnemy[i].GetComponent<ParentsOfParty>();
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
                StructOfFight ofFight = new(ofParty, PartyOfPlayer[i].GetComponent<ParentsOfParty>(), ofDamages[randomattack]);


                // 이부분이 노트 1개에 구조체 1개를 넣는 부분입니다
                // 노트1개에 다른 구조체를 넣고 싶다면 수정해 줍니다

                // ofFight를 리스트화 해서 넣어줍니다
                List<StructOfFight> ofFights = new()
                {
                    ofFight
                };

                // 그리고 해당 ofFight구조체를 넣어줍니다 
                NoteManager.FightOfListList.Add(ofFights);
                //Debug.Log(NoteManager.FightOfListList.Count);
                // 구조체를 넣어줬으니 넣어준 만큼 노트를 활성화 시킵니다

            }


        }
        NoteManager.NoteActive();
        //Debug.Log(223232323);

        //StartCoroutine(Shake((int)Phase.Battle));
    }
    
    /// <summary>
    /// 전투가 시작
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
    /// 페이즈를 보여주는 ui를 조절함
    /// </summary>
    public void UiOfPhase()
    {
        arrowOfPhase.transform.localPosition = vector3OfPhase[intOfPhase];
    }

    /// <summary>
    /// 적들의 공격을 표시함
    /// </summary>
    public void EnemyStart()
    {

    }

    /// <summary>
    /// 개전
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
/// 노트 오브젝트 관리
/// </summary>
public List<GameObject> notes;

/// <summary>
/// 노트들의 이론상 좌표값
/// </summary>
public List<float> noteTrans;

/// <summary>
/// 노트가 지나가는데 걸리는 시간
/// </summary>
public float noteTime;

/// <summary>
/// 노트가 정확히 체크되는 시간
/// </summary>
public float noteJust;

/// <summary>
/// 노트가 성공은 하는시간
/// </summary>
public float noteSafe;

/// <summary>
/// 노트가 지나다니는 길의 크기
/// </summary>
public float noteBase;

#endregion
*/

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
