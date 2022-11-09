using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBattleManager : MonoBehaviour
{
    #region  오브젝트

    /// <summary>
    /// 사운드 매니저
    /// </summary>
    public GameObject gameObjectOfSoundManager;

    /// <summary>
    /// 메인 카메라
    /// </summary>
    public GameObject gameObjectOfMainCamera;

    /// <summary>
    /// 배경 오브젝트들
    /// </summary>
    public GameObject gameObjectOfBackGround;
    





    #endregion

    #region 용도별

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

    #endregion

    #endregion

    private void Update()
    {
        NomalUpdate();
        BattleUpdate();
    }


    #region 지속적인 처리
    public void NomalUpdate()
    {

    }

    public void BattleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            intOfPlayerFocus++;
            intOfPlayerFocus %= listGameObjectOfParty.Count;
        }
    }
    #endregion

    #region 단발적인 처리

    public void BattleStart()
    {


    }

    public void BattleEnd()
    {

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
