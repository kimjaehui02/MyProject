using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public DamageManager damageManager;
    #region notes
    /// <summary>
    /// 노트의 체크지점
    /// </summary>
    public GameObject lineOfNote;

    /// <summary>
    /// 노트의 체크지점 백분율
    /// </summary>
    public float noteCheckfloat;

    /// <summary>
    /// 노트 오브젝트 관리
    /// </summary>
    public List<GameObject> noteObject;

    /// <summary>
    /// 노트들의 이론상 좌표값
    /// </summary>
    public List<float> noteFloat;

    /// <summary>
    /// 루트의 길이
    /// </summary>
    public float routeOfNote;

    /// <summary>
    /// 루트에서 노트가 지속되는 시간
    /// </summary>
    public float remainOfNote;

    /// <summary>
    /// 노트가 정확히 체크되는 시간-초
    /// </summary>
    public float justOfNote;




    #endregion

    private void Update()
    {
        //Check();

        NoteImage();

    }

    private void FixedUpdate()
    {
        Notetransform();
    }

    // 벡터x = 노트의 좌표
    // = (현재지난시간 / 총시간) -0.5)* 베이스 크기*2
    // = ((noteTrans[0] / noteTime)-0.5f) * noteBase * 2
    // = 
    // 노트의 크기 = 초당 가는거리 * 저스트 시간
    // = (noteBase / noteTime) *noteJust; 

    /// <summary>
    /// 노트의 좌표를 계산
    /// </summary>
    public void Notetransform()
    {


        noteFloat[0] += Time.fixedDeltaTime;
        if (noteFloat[0] > remainOfNote)
        {
            noteObject[0].SetActive(true);
        }
        noteFloat[0] %= remainOfNote;

        // 길이를 시간으로 나누어서 노트가 이동하는 시간을 구함
        float speedOfNote = routeOfNote / remainOfNote;

        // 노트의 길이를 대입하기(노트의 속도 * 성공 시간)
        Vector3 size = new Vector3(speedOfNote * justOfNote, 100,0);
        noteObject[0].GetComponent<RectTransform>().sizeDelta = size;

        // 노트의 좌표를 대입하기(노트의 속도 * 노트가 지난 시간)
        float moving = speedOfNote * noteFloat[0] - (routeOfNote * 0.5f);
        noteObject[0].transform.localPosition = new Vector3(-moving, 0, 0);
        //noteCheck.transform.localPosition = new Vector3(speedOfNote * noteFloat[0]-750, 0, 0);

        float index = (noteCheckfloat-0.5f) * routeOfNote;
        lineOfNote.transform.localPosition = new Vector3(-index,0,0);

    }

    /// <summary>
    /// 노트의 이미지들을 재배치
    /// </summary>
    public void NoteImage()
    {

    }

    public void Check()
    {
        if(noteObject[0].activeSelf == false)
        {
            return;
        }

        //Debug.Log(121);
        // 앞뒤로 절반씩 나눠서 범위에 들어오면 정타효과를 낸다
        // 정확한 지점은 noteCheckfloat * remainOfNote
        float index = (noteCheckfloat) * remainOfNote;
        //Debug.Log(index);
        if (index + (justOfNote * 0.5f) > noteFloat[0] &&
            index - (justOfNote * 0.5f) < noteFloat[0])
        {
            damageManager.SwordClip(Random.Range(3, 4));
            noteObject[0].SetActive(false);
        }
        else 
        { }
    }
}
