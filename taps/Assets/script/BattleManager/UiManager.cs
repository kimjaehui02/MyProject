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



    /// <summary>
    /// 예시로 보여줄 플레이어 리스트입니다
    /// </summary>
    //public List<GameObject> exampleOfPlayerList;

    #endregion

    #region 노트Ui





    public List<Color> colorOfNoteUi;


    #endregion

    #region 스킬창

    public Transform parrentOfSkill;


    #endregion

    #region 전투 변수용
    /// <summary>
    /// 남은 공격 횟수
    /// </summary>
    [field: SerializeField]
    public float pointOfAttack { get; set; }



    #endregion

    #region 플레이어 행동 스택



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

    public int intOfAttaker;

    #endregion

    #region Ui들의 큰 묶음

    public GameObject skillUis;
    public GameObject noteUis;

    #endregion



    #region 리폼


    public List<StruckOfAct> stackOfActs;

    /// <summary>
    /// 실제로 놓인 노트의 숫자입니다
    /// </summary>
    public List<int> listIntOfNoteEachP;

    /// <summary>
    /// 실제로 놓인 노트의 숫자입니다
    /// </summary>
    public List<int> listIntOfNoteEachE;

    /// <summary>
    /// 전투씬에서 확대하여 보여줄 대상입니다
    /// </summary>
    public int intOfFocus;

    #region 페이즈 전환시 나오는 화면효과 관리

    /// <summary>
    /// 이 값이 트루가 되어야 페이즈전환Ui가 시작됨
    /// </summary>
    public bool PhaseChagebool;
    /// <summary>
    /// 페이즈 UI과정은 이 플로트 변수를 따름
    /// </summary>
    public float PhaseChagefloat;

    /// <summary>
    /// 페이즈 전환때 나오는 글입니다
    /// </summary>
    public GameObject FadeOutOfText;

    /// <summary>
    /// 화면가리개입니다
    /// </summary>
    public Image FadeOutOfUi;
    #endregion



    /// <summary>
    /// 하단ui에 노트숫자를 알려주는건데 지금은 안씁니다
    /// </summary>
    public List<List<Image>> listListSpriteRendererOfNoteP;

    public List<List<Image>> listListSpriteRendererOfNoteE;

    /// <summary>
    /// 스킬의 버튼오브젝트들
    /// </summary>
    public List<List<GameObject>> listListGameObjectOfSkills;

    /// <summary>
    /// 그 오브젝트들의 버튼 컴포넌트
    /// </summary>
    public List<List<Button>> listListButtonOfSkills;

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
    /// 화면가리개입니다
    /// </summary>
    public GameObject FadeOutOfBattle;



    /// <summary>
    /// 물리엔진으로 이동하는 두 오브젝트입니다 전투할떄 캐릭터가 여기에 달려서 움직입니다
    /// </summary>
    public List<GameObject> target12; 

    #endregion

}
