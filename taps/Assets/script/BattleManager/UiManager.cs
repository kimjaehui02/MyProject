using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region UI
    /// <summary>
    /// ui�� ����Ű�� ȭ��ǥ
    /// </summary>
    public GameObject arrowOfPhase;

    /// <summary>
    /// ��ų�� uiâ�Դϴ�
    /// </summary>
    public List<GameObject> listOfSkills;

    /// <summary>
    /// �ش� �ڸ��� ��Ʈ���� �´ٰ� �˷��� ������Ʈ�Դϴ�
    /// </summary>
    public List<GameObject> listGameObjectOfNoteEach;

    #endregion

    #region ��Ƽ��

    /// <summary>
    /// �� ������Ʈ�� �ڽĵ��� ��Ƽ�� �ֽ��ϴ�
    /// </summary>
    public GameObject teamOfPlayer;
    public GameObject teamOfEnemy;



    /// <summary>
    /// ���÷� ������ �÷��̾� ����Ʈ�Դϴ�
    /// </summary>
    //public List<GameObject> exampleOfPlayerList;

    #endregion

    #region ��ƮUi





    public List<Color> colorOfNoteUi;


    #endregion

    #region ��ųâ

    public Transform parrentOfSkill;


    #endregion

    #region ���� ������
    /// <summary>
    /// ���� ���� Ƚ��
    /// </summary>
    [field: SerializeField]
    public float pointOfAttack { get; set; }



    #endregion

    #region �÷��̾� �ൿ ����



    #endregion

    #region �������

    public enum Phase { Standby, Main, BattleForEnemy, BattleForPlayer, End }

    public int intOfPhase;

    public List<Vector3> vector3OfPhase;

    #endregion

    #region ��Ƽ���� ��� ������

    /// <summary>
    /// �÷��̾ �� �� �ִ� ��ǥ�� �����մϴ�
    /// </summary>
    public List<Vector3> listOfPlayerTransform;




    public enum EnumOfUi
    {
        AllSkill, MoveSkill, TargetSkill ,Note, nonPlayable
    }


    /// <summary>
    /// Ui�� ������ ����Դϴ�
    /// </summary>
    public int intOfUi;


    /// <summary>
    /// ��ü�� ��ȣ�Դϴ�
    /// </summary>
    public int intOfChange;

    /// <summary>
    /// �÷��̾��� �ൿ����Ʈ�Դϴ�;
    /// </summary>
    public List<bool> vs;

    public int intOfAttaker;

    #endregion

    #region Ui���� ū ����

    public GameObject skillUis;
    public GameObject noteUis;

    #endregion



    #region ����

    #region ������

    public List<StruckOfAct> stackOfActs;

    /// <summary>
    /// ������ ���� ��Ʈ�� �����Դϴ�
    /// </summary>
    public List<int> listIntOfNoteEachP;

    /// <summary>
    /// ������ ���� ��Ʈ�� �����Դϴ�
    /// </summary>
    public List<int> listIntOfNoteEachE;

    #endregion


    #region �׷���

    public List<List<Image>> listListSpriteRendererOfNoteP;

    public List<List<Image>> listListSpriteRendererOfNoteE;

    /// <summary>
    /// ��ų�� ��ư������Ʈ��
    /// </summary>
    public List<List<GameObject>> listListGameObjectOfSkills;

    /// <summary>
    /// �� ������Ʈ���� ��ư ������Ʈ
    /// </summary>
    public List<List<Button>> listListButtonOfSkills;

    [field: SerializeField]
    /// <summary>
    /// �Ʊ� ��Ƽ
    /// </summary>
    public List<GameObject> PartyOfPlayer { get; set; }

    [field: SerializeField]
    /// <summary>
    /// ���� ��Ƽ
    /// </summary>
    public List<GameObject> PartyOfEnemy { get; set; }

    public GameObject focusing;

    public List<GameObject> target12; 

    #endregion


    #endregion

}
