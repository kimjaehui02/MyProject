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

    /// <summary>
    /// ���÷� ������ �÷��̾� ����Ʈ�Դϴ�
    /// </summary>
    public List<GameObject> exampleOfPlayerList;

    #endregion

    #region ��ƮUi

    /// <summary>
    /// ������ ���� ��Ʈ�� �����Դϴ�
    /// </summary>
    public List<int> listIntOfNoteEach;

    /// <summary>
    /// �ӽ÷� ǥ���� ��Ʈ�� �����Դϴ�
    /// </summary>
    public List<int> exampleOfNoteEach;

    #endregion

    #region ��ųâ

    public Transform parrentOfSkill;

    /// <summary>
    /// ��ų�� ��ư������Ʈ��
    /// </summary>
    public List<List<GameObject>> listListGameObjectOfSkills;

    /// <summary>
    /// �� ������Ʈ���� ��ư ������Ʈ
    /// </summary>
    public List<List<Button>> listListButtonOfSkills;

    #endregion

    #region ���� ������
    /// <summary>
    /// ���� ���� Ƚ��
    /// </summary>
    [field: SerializeField]
    public float pointOfAttack { get; set; }



    #endregion

    #region �÷��̾� �ൿ ����

    public List<StruckOfAct> stackOfActs;

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
    #endregion

    #region Ui���� ū ����

    public GameManager skillUis;
    public GameObject noteUis;

    #endregion
}
