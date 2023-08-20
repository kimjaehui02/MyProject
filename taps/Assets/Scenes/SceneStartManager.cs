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

    [SerializeField]
    Vector2 mapSize;

    #region 기본 화면 생성
    /// <summary>
    /// 화면의 배경과 일반 오브젝트들을 관리하는 매니저.
    /// </summary>
    public RealTownManager realTownManager;

    /// <summary>
    /// 화면의 배경의 부모
    /// </summary>
    public List<GameObject> listGameObjectOfBackGroundPrefab;

    /// <summary>
    /// 게임의 바닥
    /// </summary>
    public List<GameObject> listGameObjectOfGroundPrefab;

    /// <summary>
    /// 화면의 일반 오브젝트들의 부모
    /// </summary>
    public List<GameObject> listGameObjectOfNomalObjectPrefab;
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
    public GameObject gameObjectOfPlayablePrefab;
    #endregion

    private void Awake()
    {
        MakeObject();
        HpObserver hpObserver = new HpObserver();
    }

    private void MakeObject()
    {
        // 플레이어 생성
        GameObject playable = Instantiate(gameObjectOfPlayablePrefab);
        // 뒷배경, 바닥, 일반 오브젝트들 생성
        GameObject background = CreateObject(GameManager.instance.mapnumber, listGameObjectOfBackGroundPrefab);
        GameObject ground = CreateObject(GameManager.instance.mapnumber, listGameObjectOfGroundPrefab);
        GameObject gameObjects = CreateObject(GameManager.instance.mapnumber, listGameObjectOfNomalObjectPrefab);

        realTownManager.SceneStart(playable.transform, mapSize, background.transform.GetChild(0).gameObject, background.transform.GetChild(1).gameObject);
        realCameraManager.SceneStart(playable.transform, mapSize, gameObjectOfCamera);

        ManagerSetting();
    }

    private GameObject CreateObject(int mapNumber, List<GameObject> prefabList)
    {
        int prefabIndex = 0;

        switch (mapNumber)
        {
            case 2:
                prefabIndex = GameManager.instance.map1[0];
                break;
            case 3:
                prefabIndex = GameManager.instance.map2[0];
                break;
            case 4:
                prefabIndex = GameManager.instance.map3[0];
                break;
            case 5:
                prefabIndex = GameManager.instance.map4[0];
                break;
            case 6:
                prefabIndex = GameManager.instance.map5[0];
                break;
            default:
                prefabIndex = GameManager.instance.map1[0];
                break;
        }

        return Instantiate(prefabList[prefabIndex]);
    }

    private void ManagerSetting()
    {
        // 매니저 설정
    }
}
