using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartManager : MonoBehaviour
{
    public GameObject gameObjectOfCamera;

    #region ������ ����
    /// <summary>
    /// ������ �Ŵ���
    /// </summary>
    public RealBattleManager realBattleManager;

    /// <summary>
    /// ȭ�鿡 ������ �Ʊ��� ��Ƽ��
    /// </summary>
    public List<GameObject> listGameObjectOfParty;

    /// <summary>
    /// ȭ�鿡 ������ ���� ��Ƽ��
    /// </summary>
    public List<GameObject> listGameObjectOfPartyEnemy;

    /// <summary>
    /// ȭ�� �߰��� ������ ui
    /// </summary>
    public GameObject gameObjectOfBattleUi;
    #endregion

    #region �Ϲ� Ui
    // ui�� �����ϴ� �Ŵ���

    /// <summary>
    /// �Ϲ�ui���� �����ϴ� �Ŵ���
    /// </summary>
    public RealUiManager realUiManager;

    /// <summary>
    /// �»���� ĳ���� âƲ
    /// </summary>
    public GameObject gameObjectOfBaseUi;

    /// <summary>
    /// ���콺�� �ø��� ������ Ui
    /// </summary>
    public GameObject gameObjectOfPopUpUi;
    #endregion

    [SerializeField]
    Vector2 mapSize;

    #region �⺻ ȭ�� ����
    /// <summary>
    /// ȭ���� ���� �Ϲ� ������Ʈ���� �����ϴ� �Ŵ���.
    /// </summary>
    public RealTownManager realTownManager;

    /// <summary>
    /// ȭ���� ����� �θ�
    /// </summary>
    public List<GameObject> listGameObjectOfBackGroundPrefab;

    /// <summary>
    /// ������ �ٴ�
    /// </summary>
    public List<GameObject> listGameObjectOfGroundPrefab;

    /// <summary>
    /// ȭ���� �Ϲ� ������Ʈ���� �θ�
    /// </summary>
    public List<GameObject> listGameObjectOfNomalObjectPrefab;
    #endregion

    #region ���� ĳ����
    // ī�޶� �� ĳ���� �Ŵ���

    /// <summary>
    /// ī�޶�� ĳ���� ����
    /// </summary>
    public RealCameraManager realCameraManager;

    /// <summary>
    /// �÷��̾ �����ϴ� ĳ�����Դϴ�
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
        // �÷��̾� ����
        GameObject playable = Instantiate(gameObjectOfPlayablePrefab);
        // �޹��, �ٴ�, �Ϲ� ������Ʈ�� ����
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
        // �Ŵ��� ����
    }
}
