using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBattleManager : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    #region  오브젝트

    /// <summary>
    /// 사운드 매니저
    /// </summary>
    //public GameObject gameObjectOfSoundManager;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    //public GameObject gameObjectOfMainCamera;

    /// <summary>
    /// 배경 오브젝트들
    /// </summary>
    //public GameObject gameObjectOfBackGround;






    #endregion

    #region 용도별

    #region 개전

    /// <summary>
    /// 시작페이즈가 끝난지 알려주는 불입니다
    /// </summary>
    public bool boolOfStartEnd;

    #endregion

    #region 전투 화면 오브젝트들

    /// <summary>
    /// 전투떄 카메라 자식오브젝트인 배틀 오브젝트들입니다
    /// </summary>
    public GameObject gameObjectOfBattleObject;

    /// <summary>
    /// 전투떄 자신의 자식 오브젝트에 있는 캔버스입니다
    /// </summary>
    public GameObject gameObjectOfBattleUi;

    #endregion

    #region 상단 페이즈 ui

    /// <summary>
    /// 상단의 페이즈를 보여주는 ui
    /// </summary>
    public GameObject gameObjectOfPhaseUi;

    /// <summary>
    /// 페이즈를 나타낸다 0= 개전 1= 
    /// </summary>
    public int intOfPhase;



    #endregion

    #region 중단 노트 ui

    /// <summary>
    /// 중단의 노트들을 보여주는 Ui
    /// </summary>
    public GameObject gameObjectOfNoteUi;

    /// <summary>
    /// 노트들입니다
    /// </summary>
    public List<GameObject> listGameObjectOfNote;

    /// <summary>
    /// 체크선을 표시하는 오브젝트입니다
    /// </summary>
    public GameObject gameObjectOfCheck;

    /// <summary>
    /// 노트가 지나다니는 길의 길이
    /// </summary>
    public float floatOfRouteOfNote;

    /// <summary>
    /// 노트가 남아있는 시간
    /// </summary>
    public float floatOfRemainOfNote;

    /// <summary>
    /// 노트가 정확히 터치되는 시간
    /// </summary>
    public float floatOfJustTimeOfNote;

    /// <summary>
    /// 노트가 보통 터치되는 시간
    /// </summary>
    public float floatOfNomalTimeOfNote;

    /// <summary>
    /// 시간값을 저장하여 공격당하는 시간을 체크할때 씁니다
    /// </summary>
    public float floatOfCheckTime;

    /// <summary>
    /// 노트의 위치를 데이터화 한 것을 리스트로 만들었습니다
    /// </summary>
    public List<float> listFloatOfNotePosition;

    #endregion

    #region 플레이어와 적들
    /// <summary>
    /// 플레이어 파티
    /// </summary>
    public List<GameObject> listGameObjectOfParty;

    /// <summary>
    /// 적 파티
    /// </summary>
    public List<GameObject> listGameObjectOfEnemyParty;

    /// <summary>
    /// 포커싱하는 플레이어입니다
    /// </summary>
    public int intOfPlayerFocus;

    /// <summary>
    /// 포커싱하는 적캐릭터입니다
    /// </summary>
    public int intOfEnemyFocus;

    /// <summary>
    /// 아군의 위치
    /// </summary>
    public List<Vector3> listVector3OfPlayerPosition;



    /// <summary>
    /// 플레이어 캐릭터 프리펩
    /// </summary>
    public List<GameObject> listGameObjectOfPlayerPrefab;

    /// <summary>
    /// 적 캐릭터들 프리펩
    /// </summary>
    public List<GameObject> listGameObjectOfEnemyPrefab;

    #endregion

    #endregion

    private void Update()
    {
        if(boolOfStartEnd == true)
        {
            NomalUpdate();
            BattleUpdate();
            Notetransform();
            NoteOutCheck();
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            listGameObjectOfParty[intOfPlayerFocus].GetComponent<PlayerObjectManager>().Animinput();

            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Check();
        }
    }


    #region 지속적인 처리

    /// <summary>
    /// 기본적인 플레이어 캐릭터들을 배치합니다
    /// </summary>
    private void NomalUpdate()
    {
        // 플레이어들을 위치에 따라서 확대 크기를 정합니다
        for (int i = 0; i < listGameObjectOfParty.Count; i++)
        {
            // 캐릭터들을 배치합니다
            listGameObjectOfParty[(i + intOfPlayerFocus) % listGameObjectOfParty.Count].transform.localPosition = listVector3OfPlayerPosition[i];

            // 위치가 0이면 확대하고 아니면 축소합니다
            if (i == 0)
            {
                listGameObjectOfParty[(i + intOfPlayerFocus) % listGameObjectOfParty.Count].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                listGameObjectOfParty[(i + intOfPlayerFocus) % listGameObjectOfParty.Count].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            }

        }

        // 적 캐릭터들 역시 해줍니다
        for (int i = 0; i < listGameObjectOfEnemyParty.Count; i++)
        {
            // 캐릭터들을 배치합니다
            listGameObjectOfEnemyParty[(i + intOfPlayerFocus) % listGameObjectOfEnemyParty.Count].transform.localPosition = listVector3OfPlayerPosition[i];


            if (i == 0)
            {
                listGameObjectOfEnemyParty[(i + intOfPlayerFocus) % listGameObjectOfEnemyParty.Count].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                listGameObjectOfEnemyParty[(i + intOfPlayerFocus) % listGameObjectOfEnemyParty.Count].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            }

        }


    }

    /// <summary>
    /// 키를 눌러서 캐릭터를 교체합니다
    /// </summary>
    private void BattleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            intOfPlayerFocus++;
            intOfPlayerFocus %= listGameObjectOfParty.Count;
        }
    }

    #region 노트들의 처리

    #region 노트중에서도 지속적인 애들

    /// <summary>
    /// 범위 밖으로 나간 노트를 처치합니다
    /// </summary>
    private void NoteOutCheck()
    {
        for (int i = 0; i < listFloatOfNotePosition.Count; i++)
        {
            if(listFloatOfNotePosition[i] > floatOfRemainOfNote)
            {
                listGameObjectOfNote[i].SetActive(false);
                listFloatOfNotePosition[i] = 0f;
            }
        }
    }



    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 노트의 좌표를 계산
    /// </summary>
    private void Notetransform()
    {

        // 노트들을 훑어가며 이동을 시켜줍니다
        for (var i = 0; i < listGameObjectOfNote.Count; i++)//floatOfNote.Count; i++)
        {
            if (listGameObjectOfNote[i].activeSelf == false)
            {
                continue;
            }


            // 노트들의 시간값을 더해 이동하게 만듭니다
            listFloatOfNotePosition[i] += Time.fixedDeltaTime;


        }



        // 길이를 시간으로 나누어서 노트가 이동하는 시간을 구함
        var speedOfNote = floatOfRouteOfNote / floatOfRemainOfNote;

        // 노트의 길이를 대입하기(노트의 속도 * 성공 시간)
        var size = new Vector3(speedOfNote* floatOfJustTimeOfNote, 100, 0);
        var size2 = new Vector3(speedOfNote* floatOfNomalTimeOfNote, 100, 0);

        for (var i = 0; i < listFloatOfNotePosition.Count; i++)
        {
            if (listGameObjectOfNote[i].activeSelf == false)
            {
                continue;
            }
            // 범위가 좁은 정확한 성공범위를 표시합니다
            listGameObjectOfNote[i].transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = size;
            // 범위가 넓은 쉬운 성공범위를 표시합니다
            listGameObjectOfNote[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = size2;
        }


        // 노트의 좌표를 대입하기(노트의 속도 * 노트가 지난 시간)
        for (int i = 0; i < listFloatOfNotePosition.Count; i++)
        {
            if (listGameObjectOfNote[i] == null)
            {
                continue;
            }
            var moving = speedOfNote * listFloatOfNotePosition[i] - (floatOfRouteOfNote * 0.5f);
            listGameObjectOfNote[i].transform.localPosition = new Vector3(-moving, 0, 0);
        }

        //noteCheck.transform.localPosition = new Vector3(speedOfNote * floatOfNote[0]-750, 0, 0);

        // 체크지점 표시하기
        float index = (floatOfCheckTime - 0.5f) * floatOfRouteOfNote;
        gameObjectOfCheck.transform.localPosition = new Vector3(-index, 0, 0);

    }


    #endregion
    /// <summary>
    /// 노트들을 생성합니다
    /// </summary>
    public void NoteOn()
    {
        foreach (GameObject i in listGameObjectOfNote)
        {
            if (i.activeSelf == false)
            {
                i.SetActive(true);

                i.GetComponent<RectTransform>().localPosition = new Vector3(500, 0, 0);

                break;
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 버튼을 눌렀을때 실행하여 노트의 위치를 체크합니다
    /// </summary>
    public int Check()
    {
        // 성공을 리턴합니다
        int able = 0;

        // 헛타를 쳤는지를 검사합니다
        bool missCheck = false;
        // 체크지점을 비교하기위해서 백분율과 곱해줍니다 지속시간의 75퍼센트 지점을 나타냅니다
        float index = (floatOfCheckTime) * floatOfRemainOfNote;

        // 노트들을 순회하면서 체크합니다
        for (int i = 0; i < listGameObjectOfNote.Count; i++)
        {
            // 비활성화되어서 나오지 않은 노트는 검사하지않습니다
            if (listGameObjectOfNote[i].activeSelf == false)
            {
                continue;
            }

            // 앞뒤로 절반씩 나눠서 범위에 들어오면 정타효과를 낸다
            // 정확한 지점은 noteCheckfloat * remainOfNote

            // 노트의 범위가 맞는지 체크를 합니다
            if (index + (floatOfJustTimeOfNote * 0.55f) > listFloatOfNotePosition[i] &&
                index - (floatOfJustTimeOfNote * 0.55f) < listFloatOfNotePosition[i] &&
                listGameObjectOfNote[i].activeSelf == true)
            {
                // 무작위로 소리클립을 재생합니다
                //damageManager.SwordClip(Random.Range(3, 5));

                NoteAttack();
                // 위치 원상복구

                listGameObjectOfNote[i].GetComponent<RectTransform>().localPosition = new Vector3(500, 0, 0);
                listFloatOfNotePosition[i] = 0;
                // 성공했으니 노트를 비활성화 합니다
                listGameObjectOfNote[i].SetActive(false);
                // 데미지를 계산하여 입힙니다
                //damageManager.CalculationOfDamage(List2StructOfFightOfCastEnemy[i]);
                able = 2;
                missCheck = true;

                break;
            }
            // 정확히 성공하지 못했다면 평범하게라도 성공했는지 뭍습니다
            else if (index + (floatOfNomalTimeOfNote * 0.55f) > listFloatOfNotePosition[i] &&
                    index - (floatOfNomalTimeOfNote * 0.55f) < listFloatOfNotePosition[i] &&
                    listGameObjectOfNote[i].activeSelf == true)
            {
                //damageManager.SwordClip(Random.Range(3, 5));
                NoteAttack();
                // 위치 원상복구
                listGameObjectOfNote[i].GetComponent<RectTransform>().localPosition = new Vector3(500, 0, 0);
                listFloatOfNotePosition[i] = 0;

                listGameObjectOfNote[i].SetActive(false);
                missCheck = true;

                // 데미지를 계산하여 입힙니다
                //damageManager.CalculationOfDamage(List2StructOfFightOfCastEnemy[i]);
                able = 1;

                break;
            }

        }

        if (missCheck == false)
        {
            //damageManager.MissClip();
        }

        return able;

    }


    /// <summary>
    /// 노트를 성공했을때 할 작업들입니다
    /// </summary>
    /// <param name="checks"></param>
    public void NoteAttack(int checks = 0)
    {
        listGameObjectOfParty[intOfPlayerFocus].GetComponent<PlayerObjectManager>().Animinput();

        switch (checks)
        {
            case 2:

                break;

            case 1:
                break;

            default:

                break;

        }
        listGameObjectOfParty[intOfPlayerFocus].GetComponent<PlayerObjectManager>().StaDamage();
    }

    #endregion

    #endregion

    #region 단발적인 처리

    /// <summary>
    /// 이 함수를 호출하면 전투 화면이 나옵니다
    /// </summary>
    public void BattleStarter()
    {

        var gm = GameManager.instance.listRealPlayer;


        // 배틀용 오브젝트와 ui를 활성화 합니다
        gameObjectOfBattleObject.SetActive(true);
        gameObjectOfBattleUi.SetActive(true);




        // 플레이어측을 생성합니다
        for (int i = 0; i < gm.Count; i++)
        {
            var party = Instantiate(listGameObjectOfPlayerPrefab[gm[i].intOfType]);
            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(0);

            party.GetComponent<PlayerObjectManager>().Setting(gm[i].floatOfHp, gm[i].floatOfStamina);

            listGameObjectOfParty.Add(party);
        }


        // 적군측을 생성합니다
        for (int i = 0; i < 4; i++)
        {
            var party = Instantiate(listGameObjectOfEnemyPrefab[i]);



            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(1);
            listGameObjectOfEnemyParty.Add(party);
        }

        NomalUpdate();

        boolOfStartEnd = true;
    }

    /// <summary>
    /// 이 함수를 호출하면 전투가 끝납니다
    /// </summary>
    public void BattleEnder()
    {

        gameObjectOfBattleObject.SetActive(false);
        gameObjectOfBattleUi.SetActive(false);

        foreach (var i in listGameObjectOfParty)
        {
            Destroy(i);
        }

        foreach (var i in listGameObjectOfEnemyParty)
        {
            Destroy(i);
        }

        listGameObjectOfParty.Clear();
        listGameObjectOfEnemyParty.Clear();

    }

    #endregion











    #region 고찰

    // 새로운 씬으로 화면이 전환되는건 속도감이 줄어드낟
    // 그리고 만들어놓은 씬과 스크립트도 이미 지저분해서 상당히 곤란하다
    // 그래서 새롭게 만든 스크립트이다
    // 필요한 것들을 정리해 보자

    // 오브젝트들

    // -    주인공 파티들
    // -    적 파티들

    // 기본적인 캐릭터들의 공방을 위해서 얘네가 필요하다

    // -    (상단) 전투 진행상황 ui
    // -    (중단) 전투 노트 Ui

    // 상단의 ui는 전투 진행을 보기위해서 필요하고
    // 중단의 ui는 전투할때 노트를 보고 전투하기위해서 필요하다


    // 기본적인 전투

    // 적은 적별로 노트의 갯수를 가지고 있다
    // 적은 전투에 나설경우 자신의 노트만큼 노트를 생성한다
    // 중앙에 생성한 노트들이 보이는데
    // 생성된 노트에 위치가 지정된다
    // 플레이어가 위치에 맞게 노트를 누르면 해당 노트는 파괴되고
    // 모든 노트가 파괴되면 언제든지 버튼을 눌러 적을 처지가 가능하다

    // 노트를 누르는 행동을 할때마다 플레이어에게 피로가 쌓인다
    // 피로는 데미지를 입을때 해당 수치만큼 추가피해를 입게 만든다
    // 피로가 과하게 누적되면 피해를 입지 않아도 기절하며 무방비 상태가 된다
    // 이를 막기위해 피로가 쌓이면 교체를 해야 한다

    #endregion
}
