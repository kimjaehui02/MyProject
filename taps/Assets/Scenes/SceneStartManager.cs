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
    public List<GameObject> listGameObjectOfBackGround;


    /// <summary>
    /// ������ �ٴ�
    /// </summary>
    public List<GameObject> listGameObjectOfGround;

    /// <summary>
    /// ȭ���� �Ϲ� ������Ʈ���� �θ�
    /// </summary>
    public List<GameObject> listGameObjectOfNomalObject;

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
    public GameObject gameObjectOfPlayable;

    #endregion


    private void Awake()
    {
        MakeObject();
    }

    private void MakeObject()
    {
        // �÷��̾� ����
        GameObject playable = Instantiate(gameObjectOfPlayable);
        // �޹�� ����
        GameObject background = Instantiate(listGameObjectOfBackGround[Random.Range(0, listGameObjectOfBackGround.Count)]);
        // �ٴ� ����
        GameObject ground = Instantiate(listGameObjectOfGround[Random.Range(0, listGameObjectOfGround.Count)]);
        // �Ϲ� ������Ʈ�� ����
        GameObject gameObjects = Instantiate(listGameObjectOfNomalObject[Random.Range(0, listGameObjectOfNomalObject.Count)]);


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