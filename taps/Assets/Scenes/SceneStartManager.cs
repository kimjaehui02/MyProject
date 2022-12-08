using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartManager : MonoBehaviour
{

    public GameObject gameObjectOfCamera;

    #region 전투쪽 전반
    /// <summary>
    /// 전투쪽 매니저
    /// </summary>
    public RealBattleManager realBattleManager;


    /// <summary>
    /// 화면에 나오는 아군의 파티들
    /// </summary>
    public List<GameObject> listGameObjectOfParty;

    /// <summary>
    /// 화면에 나오는 적의 파티들
    /// </summary>
    public List<GameObject> listGameObjectOfPartyEnemy;



    /// <summary>
    /// 화면 중간의 전투용 ui
    /// </summary>
    public GameObject gameObjectOfBattleUi;

    #endregion

    #region 일반 Ui

    // ui를 관리하는 매니저

    /// <summary>
    /// 일반ui들을 관리하는 매니저
    /// </summary>
    public RealUiManager realUiManager;


    /// <summary>
    /// 좌상단의 캐릭터 창틀
    /// </summary>
    public GameObject gameObjectOfBaseUi;

    /// <summary>
    /// 마우스를 올리면 나오는 Ui
    /// </summary>
    public GameObject gameObjectOfPopUpUi;

    #endregion

    //[SerializeField]
    //Transform playerTransform;

    [SerializeField]
    Vector2 mapSize;

    #region 기본 화면 생성
    /// <summary>
    /// 화면의 배경과 일반 오브젝트들을 관리하는 매니저
    /// </summary>
    public RealTownManager realTownManager;

    /// <summary>
    /// 화면의 배경의 부모
    /// </summary>
    public List<GameObject> listGameObjectOfBackGround;


    /// <summary>
    /// 게임의 바닥
    /// </summary>
    public List<GameObject> listGameObjectOfGround;

    /// <summary>
    /// 화면의 일반 오브젝트들의 부모
    /// </summary>
    public List<GameObject> listGameObjectOfNomalObject;

    #endregion

    #region 조작 캐릭터

    // 카메라 및 캐릭터 매니저

    /// <summary>
    /// 카메라와 캐릭터 관리
    /// </summary>
    public RealCameraManager realCameraManager;

    /// <summary>
    /// 플레이어가 조작하는 캐릭터입니다
    /// </summary>
    public GameObject gameObjectOfPlayable;

    #endregion


    private void Awake()
    {
        MakeObject();
    }

    private void MakeObject()
    {
        // 플레이어 생성
        GameObject playable = Instantiate(gameObjectOfPlayable);
        // 뒷배경 생성
        GameObject background = Instantiate(listGameObjectOfBackGround[Random.Range(0, listGameObjectOfBackGround.Count)]);
        // 바닥 생성
        GameObject ground = Instantiate(listGameObjectOfGround[Random.Range(0, listGameObjectOfGround.Count)]);
        // 일반 오브젝트들 생성
        GameObject gameObjects = Instantiate(listGameObjectOfNomalObject[Random.Range(0, listGameObjectOfNomalObject.Count)]);


        realTownManager.SceneStart(playable.transform, mapSize, background.transform.GetChild(0).gameObject, background.transform.GetChild(1).gameObject);
        realCameraManager.SceneStart(playable.transform, mapSize, gameObjectOfCamera);

        ManagerSetting();
    }

    private void ManagerSetting()
    {

    }

}


// 정리

// 스타트 매니저가 맵을 만들어 줬으면 함

// 스타트 매니저는 프리팹을 가지고 맵을 만들기 시작함

// 프리팹으로 생성한 뒤 매니저들을 초기화 하는 과정을 거침