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

    //[SerializeField]
    //Transform playerTransform;

    [SerializeField]
    Vector2 mapSize;

    #region �⺻ ȭ�� ����
    /// <summary>
    /// ȭ���� ���� �Ϲ� ������Ʈ���� �����ϴ� �Ŵ���
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

    #region


    #endregion


    private void Awake()
    {
        MakeObject();
    }

    private void MakeObject()
    {
        // �÷��̾� ����
        GameObject playable = Instantiate(gameObjectOfPlayablePrefab);
        // �޹�� ����
        GameObject background = Instantiate(listGameObjectOfBackGroundPrefab[Random.Range(0, listGameObjectOfBackGroundPrefab.Count)]);
        // �ٴ� ����
        GameObject ground = Instantiate(listGameObjectOfGroundPrefab[Random.Range(0, listGameObjectOfGroundPrefab.Count)]);
        // �Ϲ� ������Ʈ�� ����
        GameObject gameObjects = Instantiate(listGameObjectOfNomalObjectPrefab[Random.Range(0, listGameObjectOfNomalObjectPrefab.Count)]);


        realTownManager.SceneStart(playable.transform, mapSize, background.transform.GetChild(0).gameObject, background.transform.GetChild(1).gameObject);
        realCameraManager.SceneStart(playable.transform, mapSize, gameObjectOfCamera);

        ManagerSetting();
    }

    private void ManagerSetting()
    {

    }

}


// ����

// ��ŸƮ �Ŵ����� ���� ����� ������ ��

// ��ŸƮ �Ŵ����� �������� ������ ���� ����� ������

// ���������� ������ �� �Ŵ������� �ʱ�ȭ �ϴ� ������ ��ħ