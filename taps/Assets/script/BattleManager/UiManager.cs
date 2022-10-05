using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    #region UI
    /// <summary>
    /// ui를 가리키는 화살표
    /// </summary>
    public GameObject arrowOfPhase;

    /// <summary>
    /// 스킬의 ui창입니다
    /// </summary>
    public List<GameObject> listOfSkills;

    /// <summary>
    /// 해당 자리에 노트들이 온다고 알려줄 오브젝트입니다
    /// </summary>
    public List<GameObject> listGameObjectOfNoteEach;

    #endregion

    #region 파티들

    /// <summary>
    /// 이 오브젝트의 자식들을 파티에 넣습니다
    /// </summary>
    public GameObject teamOfPlayer;
    public GameObject teamOfEnemy;

    [field: SerializeField]
    /// <summary>
    /// 아군 파티
    /// </summary>
    public List<GameObject> PartyOfPlayer { get; set; }

    [field: SerializeField]
    /// <summary>
    /// 적군 파티
    /// </summary>
    public List<GameObject> PartyOfEnemy { get; set; }

    /// <summary>
    /// 예시로 보여줄 플레이어 리스트입니다
    /// </summary>
    public List<GameObject> exampleOfPlayerList;

    #endregion

    #region 노트Ui

    /// <summary>
    /// 실제로 놓인 노트의 숫자입니다
    /// </summary>
    public List<int> listIntOfNoteEach;

    /// <summary>
    /// 임시로 표현된 노트의 숫자입니다
    /// </summary>
    public List<int> exampleOfNoteEach;

    #endregion

    #region 스킬창

    public Transform parrentOfSkill;

    /// <summary>
    /// 스킬의 버튼오브젝트들
    /// </summary>
    public List<List<GameObject>> listListGameObjectOfSkills;

    /// <summary>
    /// 그 오브젝트들의 버튼 컴포넌트
    /// </summary>
    public List<List<Button>> listListButtonOfSkills;

    #endregion

    #region 전투 변수용
    /// <summary>
    /// 남은 공격 횟수
    /// </summary>
    [field: SerializeField]
    public float pointOfAttack { get; set; }



    #endregion

    #region 플레이어 행동 스택

    public List<StruckOfAct> stackOfActs;

    #endregion

    #region 페이즈용

    public enum Phase { Standby, Main, BattleForEnemy, BattleForPlayer, End }

    public int intOfPhase;

    public List<Vector3> vector3OfPhase;

    #endregion

    #region 파티원들 장소 지정용

    /// <summary>
    /// 플레이어가 갈 수 있는 좌표를 저장합니다
    /// </summary>
    public List<Vector3> listOfPlayerTransform;




    public enum EnumOfUi
    {
        AllSkill, MoveSkill, TargetSkill ,Note, nonPlayable
    }


    /// <summary>
    /// Ui를 보여줄 방식입니다
    /// </summary>
    public int intOfUi;


    /// <summary>
    /// 교체할 번호입니다
    /// </summary>
    public int intOfChange;

    /// <summary>
    /// 플레이어의 행동포인트입니다;
    /// </summary>
    public List<bool> vs;
    #endregion

    #region Ui들의 큰 묶음

    public GameManager skillUis;
    public GameObject noteUis;

    #endregion
}
