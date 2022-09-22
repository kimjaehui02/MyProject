using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteManager : MonoBehaviour
{
    public DamageManager damageManager;
    #region notes
    /// <summary>
    /// ��Ʈ�� üũ����
    /// </summary>
    public GameObject lineOfNote;

    /// <summary>
    /// ��Ʈ�� üũ���� �����
    /// </summary>
    public float noteCheckfloat;

    /// <summary>
    /// ��Ʈ ������Ʈ ����
    /// </summary>
    public List<GameObject> noteObject;

    /// <summary>
    /// ��Ʈ���� �̷л� ��ǥ��
    /// </summary>
    public List<float> noteFloat;

    /// <summary>
    /// ��Ʈ�� ����
    /// </summary>
    public float routeOfNote;

    /// <summary>
    /// ��Ʈ���� ��Ʈ�� ���ӵǴ� �ð�
    /// </summary>
    public float remainOfNote;

    /// <summary>
    /// ��Ʈ�� ��Ȯ�� üũ�Ǵ� �ð�-��
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

    // ����x = ��Ʈ�� ��ǥ
    // = (���������ð� / �ѽð�) -0.5)* ���̽� ũ��*2
    // = ((noteTrans[0] / noteTime)-0.5f) * noteBase * 2
    // = 
    // ��Ʈ�� ũ�� = �ʴ� ���°Ÿ� * ����Ʈ �ð�
    // = (noteBase / noteTime) *noteJust; 

    /// <summary>
    /// ��Ʈ�� ��ǥ�� ���
    /// </summary>
    public void Notetransform()
    {


        noteFloat[0] += Time.fixedDeltaTime;
        if (noteFloat[0] > remainOfNote)
        {
            noteObject[0].SetActive(true);
        }
        noteFloat[0] %= remainOfNote;

        // ���̸� �ð����� ����� ��Ʈ�� �̵��ϴ� �ð��� ����
        float speedOfNote = routeOfNote / remainOfNote;

        // ��Ʈ�� ���̸� �����ϱ�(��Ʈ�� �ӵ� * ���� �ð�)
        Vector3 size = new Vector3(speedOfNote * justOfNote, 100,0);
        noteObject[0].GetComponent<RectTransform>().sizeDelta = size;

        // ��Ʈ�� ��ǥ�� �����ϱ�(��Ʈ�� �ӵ� * ��Ʈ�� ���� �ð�)
        float moving = speedOfNote * noteFloat[0] - (routeOfNote * 0.5f);
        noteObject[0].transform.localPosition = new Vector3(-moving, 0, 0);
        //noteCheck.transform.localPosition = new Vector3(speedOfNote * noteFloat[0]-750, 0, 0);

        float index = (noteCheckfloat-0.5f) * routeOfNote;
        lineOfNote.transform.localPosition = new Vector3(-index,0,0);

    }

    /// <summary>
    /// ��Ʈ�� �̹������� ���ġ
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
        // �յڷ� ���ݾ� ������ ������ ������ ��Ÿȿ���� ����
        // ��Ȯ�� ������ noteCheckfloat * remainOfNote
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
