using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public DamageManager damageManager;


    [field: SerializeField]
    public float Var { get; set; }

    public NoteManager NoteManager;

    /*
    #region notes
    /// <summary>
    /// 노트 오브젝트 관리
    /// </summary>
    public List<GameObject> notes;

    /// <summary>
    /// 노트들의 이론상 좌표값
    /// </summary>
    public List<float> noteTrans;

    /// <summary>
    /// 노트가 지나가는데 걸리는 시간
    /// </summary>
    public float noteTime;

    /// <summary>
    /// 노트가 정확히 체크되는 시간
    /// </summary>
    public float noteJust;

    /// <summary>
    /// 노트가 성공은 하는시간
    /// </summary>
    public float noteSafe;

    /// <summary>
    /// 노트가 지나다니는 길의 크기
    /// </summary>
    public float noteBase;

    #endregion
    */

    // 전투의 진행

    // - 개전

    // 적들과 아군이 등장


    // - 전투중

    // 적들의 공격이 표시
    // 아군의 공격을 선택
    // 적들의 공격을 실행
    // 아군의 공격을 실행

    // 정타 시간은 0.5f

    // 정타 시간  = 범위 /속도;




    /// <summary>
    /// 개전
    /// </summary>
    public void BattleStart()
    {

    }

    /// <summary>
    /// 적들의 공격을 표시함
    /// </summary>
    public void EnemyStart()
    {

    }
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NoteManager.Check();
        }
    }
}
