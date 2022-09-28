using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NoteManager : MonoBehaviour
{
    public DamageManager damageManager;


    #region notes
    /// <summary>
    /// 담을 함수들의 매개변수를 조절합니다 공격자, 방어자, 피해량, 방어시 스태미너, 전체공격유무
    /// </summary>
    /// <param name="parentsOfParty"></param>
    //public delegate void functionOfNote(ParentsOfParty attacker, ParentsOfParty defender, int damage, int damageOfStamina = 1, bool splash = false);

    /// <summary>
    /// 노트에 담길 함수
    /// </summary>
    //public List<functionOfNote> functionOfNotes;

    /// <summary>
    /// 노트의 체크지점
    /// </summary>
    public GameObject lineOfNote;

    /// <summary>
    /// 노트의 체크지점 백분율
    /// </summary>
    public float noteCheckfloat;

    /// <summary>
    /// 노트 오브젝트 관리
    /// </summary>
    public List<GameObject> objectOfNote;

    /// <summary>
    /// 노트들의 이론상 좌표값
    /// </summary>
    public List<float> floatOfNote;
    
    /// <summary>
    /// 적이 몇개의 노트로 공격을 오는지 알려줍니다
    /// </summary>
    public int numberOfAttack;

    /// <summary>
    /// 루트의 길이
    /// </summary>
    public float routeOfNote;

    /// <summary>
    /// 루트에서 노트가 지속되는 시간
    /// </summary>
    public float remainOfNote;

    /// <summary>
    /// 노트가 정확히 체크되는 시간-초
    /// </summary>
    public float justOfNote;

    /// <summary>
    /// 노트가 정확하진 않지만 체크되는 시간
    /// </summary>
    public float normalOfNote;

    /// <summary>
    /// 구조체 리스트리스트
    /// </summary>
    public List<List<StructOfFight>> FightOfListList { get; set; }

    public ParentsOfParty OfParty;
    public ParentsOfParty OfParty2;

    #endregion
    #region Default
    private void Start()
    {
        FightOfListList = new List<List<StructOfFight>>();
        //testStart();
    }




    public void NoteManagerUpdate()
    {



    }

    public void NoteManagerFixedUpdate()
    {
        Notetransform();
    }
    #endregion

    public void testStart(List<List<StructOfFight>> fightOfListList)
    {
        fightOfListList = new List<List<StructOfFight>>();
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList[0] = new List<StructOfFight>();
        fightOfListList[1] = new List<StructOfFight>();
        StructOfDamage @struct = new StructOfDamage(2, 3, false);
        StructOfDamage @struct2 = new StructOfDamage(200, 33, false);
        fightOfListList[0].Add(new StructOfFight(OfParty, OfParty2, @struct));
        fightOfListList[0].Add(new StructOfFight(OfParty, OfParty2, @struct2));
        fightOfListList[1].Add(new StructOfFight(OfParty, OfParty2, @struct));
        //test = gameObject.AddComponent<ParentsOfParty>();
        //InitializationOfNotefunction();
    }


    #region 인게임에서 노트들을 생성하거나 해주는 함수들
    #endregion

    #region 노트의 공학적인 내용들 

    // 벡터x = 노트의 좌표
    // = (현재지난시간 / 총시간) -0.5)* 베이스 크기*2
    // = ((noteTrans[0] / noteTime)-0.5f) * noteBase * 2
    // = 
    // 노트의 크기 = 초당 가는거리 * 저스트 시간
    // = (noteBase / noteTime) *noteJust; 

    /// <summary>
    /// 노트의 좌표를 계산
    /// </summary>
    public void Notetransform()
    {

        // 노트들을 훑어가며 이동을 시켜줍니다
        for (int i = 0; i < objectOfNote.Count; i++ )//floatOfNote.Count; i++)
        {
            // 노트가 없으면 일을 하지 않고 루프도 종료합니다
            if(objectOfNote[i].activeSelf == false)
            {
                continue;
            }
            // 노트들의 시간값을 더해 이동하게 만듭니다
            floatOfNote[i] += Time.fixedDeltaTime;

            // 미정용
            //if (floatOfNote[i] > remainOfNote)
            //{
            //    objectOfNote[i].SetActive(true);
            //}
            //floatOfNote[i] %= remainOfNote;
        }



        // 길이를 시간으로 나누어서 노트가 이동하는 시간을 구함
        float speedOfNote = routeOfNote / remainOfNote;

        // 노트의 길이를 대입하기(노트의 속도 * 성공 시간)
        Vector3 size = new Vector3(speedOfNote * justOfNote, 100, 0);
        Vector3 size2 = new Vector3(speedOfNote * normalOfNote, 100, 0);
        for (int i = 0; i < floatOfNote.Count; i++)
        {
            if (objectOfNote[i].activeSelf == false)
            {
                break;
            }
            // 범위가 좁은 정확한 성공범위를 표시합니다
            objectOfNote[i].transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = size;
            // 범위가 넓은 쉬운 성공범위를 표시합니다
            objectOfNote[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = size2;
        }


        // 노트의 좌표를 대입하기(노트의 속도 * 노트가 지난 시간)
        for (int i = 0; i < floatOfNote.Count; i++)
        {
            if (objectOfNote[i] == null)
            {
                break;
            }
            float moving = speedOfNote * floatOfNote[i] - (routeOfNote * 0.5f);
            objectOfNote[i].transform.localPosition = new Vector3(-moving, 0, 0);
        }

        //noteCheck.transform.localPosition = new Vector3(speedOfNote * floatOfNote[0]-750, 0, 0);

        // 체크지점 표시하기
        float index = (noteCheckfloat-0.5f) * routeOfNote;
        lineOfNote.transform.localPosition = new Vector3(-index,0,0);

    }

    public void NoteActive()
    {
        Debug.Log(FightOfListList.Count);
        for (int i = 0; i < FightOfListList.Count; i++)
        {
            objectOfNote[i].SetActive(true);
        }
    }

    /// <summary>
    /// 버튼을 눌렀을때 실행하여 노트의 위치를 체크합니다
    /// </summary>
    public void Check()
    {
        // 헛타를 쳤는지를 검사합니다
        bool missCheck = false;
        // 체크지점을 비교하기위해서 백분율과 곱해줍니다 지속시간의 75퍼센트 지점을 나타냅니다
        float index = (noteCheckfloat) * remainOfNote;

        // 노트들을 순회하면서 체크합니다
        for (int i = 0; i < objectOfNote.Count; i++)
        {
            // 비활성화되어서 나오지 않은 노트는 검사하지않습니다
            if(objectOfNote[i].activeSelf == false)
            {
                continue;
            }

            // 앞뒤로 절반씩 나눠서 범위에 들어오면 정타효과를 낸다
            // 정확한 지점은 noteCheckfloat * remainOfNote

            // 노트의 범위가 맞는지 체크를 합니다
            if (index + (justOfNote * 0.55f) > floatOfNote[i] &&
                index - (justOfNote * 0.55f) < floatOfNote[i] &&
                objectOfNote[i].activeSelf == true)
            {
                // 무작위로 소리클립을 재생합니다
                damageManager.SwordClip(Random.Range(3, 5));
                // 성공했으니 노트를 비활성화 합니다
                objectOfNote[i].SetActive(false);
                // 데미지를 계산하여 입힙니다
                damageManager.CalculationOfDamage(FightOfListList[i]);
                missCheck = true;
            }
            // 정확히 성공하지 못했다면 평범하게라도 성공했는지 뭍습니다
            else if(index + (normalOfNote * 0.55f) > floatOfNote[i] &&
                    index - (normalOfNote * 0.55f) < floatOfNote[i] &&
                    objectOfNote[i].activeSelf == true)
            {
                damageManager.SwordClip(Random.Range(3, 5));
                objectOfNote[i].SetActive(false);
                missCheck = true;

                // 데미지를 계산하여 입힙니다
                damageManager.CalculationOfDamage(FightOfListList[i]);

            }

        }

        if(missCheck == false)
        {
            damageManager.MissClip();
        }


    }
    #endregion
}

/// <summary>
/// 공격자와 방어자가 포함된 공방정보를 담은 구조체입니다
/// </summary>
public struct StructOfFight
{
    public ParentsOfParty attacker;
    public ParentsOfParty defender;

    public StructOfDamage structOfDamage;

    public StructOfFight(ParentsOfParty attacker, ParentsOfParty defender, StructOfDamage structOfDamage)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.structOfDamage = structOfDamage;
    }
}

/// <summary>
/// 데미지 정보만 간략히 담은 구조체 입니다
/// </summary>
public struct StructOfDamage
{
    public int damage;
    public int damageOfStamina;
    public bool splash;
    public int numberOfNote;

    public StructOfDamage(int a = 1, int b = 1, bool c = false, int d = 1)
    {
        damage = a;
        damageOfStamina = b;
        splash = c;
        numberOfNote = d;
    }
}
