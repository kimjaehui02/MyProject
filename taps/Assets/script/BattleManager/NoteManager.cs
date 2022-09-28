using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NoteManager : MonoBehaviour
{
    public DamageManager damageManager;


    #region notes
    /// <summary>
    /// ���� �Լ����� �Ű������� �����մϴ� ������, �����, ���ط�, ���� ���¹̳�, ��ü��������
    /// </summary>
    /// <param name="parentsOfParty"></param>
    //public delegate void functionOfNote(ParentsOfParty attacker, ParentsOfParty defender, int damage, int damageOfStamina = 1, bool splash = false);

    /// <summary>
    /// ��Ʈ�� ��� �Լ�
    /// </summary>
    //public List<functionOfNote> functionOfNotes;

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
    public List<GameObject> objectOfNote;

    /// <summary>
    /// ��Ʈ���� �̷л� ��ǥ��
    /// </summary>
    public List<float> floatOfNote;
    
    /// <summary>
    /// ���� ��� ��Ʈ�� ������ ������ �˷��ݴϴ�
    /// </summary>
    public int numberOfAttack;

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

    /// <summary>
    /// ��Ʈ�� ��Ȯ���� ������ üũ�Ǵ� �ð�
    /// </summary>
    public float normalOfNote;

    /// <summary>
    /// ����ü ����Ʈ����Ʈ
    /// </summary>
    public List<List<StructOfFight>> FightOfListList { get; set; }

    public ParentsOfParty OfParty;
    public ParentsOfParty OfParty2;

    #endregion
    #region Default
    private void Start()
    {
        FightOfListList = new List<List<StructOfFight>>();
        //testStart();
    }




    public void NoteManagerUpdate()
    {



    }

    public void NoteManagerFixedUpdate()
    {
        Notetransform();
    }
    #endregion

    public void testStart(List<List<StructOfFight>> fightOfListList)
    {
        fightOfListList = new List<List<StructOfFight>>();
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList.Add(null);
        fightOfListList[0] = new List<StructOfFight>();
        fightOfListList[1] = new List<StructOfFight>();
        StructOfDamage @struct = new StructOfDamage(2, 3, false);
        StructOfDamage @struct2 = new StructOfDamage(200, 33, false);
        fightOfListList[0].Add(new StructOfFight(OfParty, OfParty2, @struct));
        fightOfListList[0].Add(new StructOfFight(OfParty, OfParty2, @struct2));
        fightOfListList[1].Add(new StructOfFight(OfParty, OfParty2, @struct));
        //test = gameObject.AddComponent<ParentsOfParty>();
        //InitializationOfNotefunction();
    }


    #region �ΰ��ӿ��� ��Ʈ���� �����ϰų� ���ִ� �Լ���
    #endregion

    #region ��Ʈ�� �������� ����� 

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

        // ��Ʈ���� �Ⱦ�� �̵��� �����ݴϴ�
        for (int i = 0; i < objectOfNote.Count; i++ )//floatOfNote.Count; i++)
        {
            // ��Ʈ�� ������ ���� ���� �ʰ� ������ �����մϴ�
            if(objectOfNote[i].activeSelf == false)
            {
                continue;
            }
            // ��Ʈ���� �ð����� ���� �̵��ϰ� ����ϴ�
            floatOfNote[i] += Time.fixedDeltaTime;

            // ������
            //if (floatOfNote[i] > remainOfNote)
            //{
            //    objectOfNote[i].SetActive(true);
            //}
            //floatOfNote[i] %= remainOfNote;
        }



        // ���̸� �ð����� ����� ��Ʈ�� �̵��ϴ� �ð��� ����
        float speedOfNote = routeOfNote / remainOfNote;

        // ��Ʈ�� ���̸� �����ϱ�(��Ʈ�� �ӵ� * ���� �ð�)
        Vector3 size = new Vector3(speedOfNote * justOfNote, 100, 0);
        Vector3 size2 = new Vector3(speedOfNote * normalOfNote, 100, 0);
        for (int i = 0; i < floatOfNote.Count; i++)
        {
            if (objectOfNote[i].activeSelf == false)
            {
                break;
            }
            // ������ ���� ��Ȯ�� ���������� ǥ���մϴ�
            objectOfNote[i].transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = size;
            // ������ ���� ���� ���������� ǥ���մϴ�
            objectOfNote[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = size2;
        }


        // ��Ʈ�� ��ǥ�� �����ϱ�(��Ʈ�� �ӵ� * ��Ʈ�� ���� �ð�)
        for (int i = 0; i < floatOfNote.Count; i++)
        {
            if (objectOfNote[i] == null)
            {
                break;
            }
            float moving = speedOfNote * floatOfNote[i] - (routeOfNote * 0.5f);
            objectOfNote[i].transform.localPosition = new Vector3(-moving, 0, 0);
        }

        //noteCheck.transform.localPosition = new Vector3(speedOfNote * floatOfNote[0]-750, 0, 0);

        // üũ���� ǥ���ϱ�
        float index = (noteCheckfloat-0.5f) * routeOfNote;
        lineOfNote.transform.localPosition = new Vector3(-index,0,0);

    }

    public void NoteActive()
    {
        Debug.Log(FightOfListList.Count);
        for (int i = 0; i < FightOfListList.Count; i++)
        {
            objectOfNote[i].SetActive(true);
        }
    }

    /// <summary>
    /// ��ư�� �������� �����Ͽ� ��Ʈ�� ��ġ�� üũ�մϴ�
    /// </summary>
    public void Check()
    {
        // ��Ÿ�� �ƴ����� �˻��մϴ�
        bool missCheck = false;
        // üũ������ ���ϱ����ؼ� ������� �����ݴϴ� ���ӽð��� 75�ۼ�Ʈ ������ ��Ÿ���ϴ�
        float index = (noteCheckfloat) * remainOfNote;

        // ��Ʈ���� ��ȸ�ϸ鼭 üũ�մϴ�
        for (int i = 0; i < objectOfNote.Count; i++)
        {
            // ��Ȱ��ȭ�Ǿ ������ ���� ��Ʈ�� �˻������ʽ��ϴ�
            if(objectOfNote[i].activeSelf == false)
            {
                continue;
            }

            // �յڷ� ���ݾ� ������ ������ ������ ��Ÿȿ���� ����
            // ��Ȯ�� ������ noteCheckfloat * remainOfNote

            // ��Ʈ�� ������ �´��� üũ�� �մϴ�
            if (index + (justOfNote * 0.55f) > floatOfNote[i] &&
                index - (justOfNote * 0.55f) < floatOfNote[i] &&
                objectOfNote[i].activeSelf == true)
            {
                // �������� �Ҹ�Ŭ���� ����մϴ�
                damageManager.SwordClip(Random.Range(3, 5));
                // ���������� ��Ʈ�� ��Ȱ��ȭ �մϴ�
                objectOfNote[i].SetActive(false);
                // �������� ����Ͽ� �����ϴ�
                damageManager.CalculationOfDamage(FightOfListList[i]);
                missCheck = true;
            }
            // ��Ȯ�� �������� ���ߴٸ� ����ϰԶ� �����ߴ��� �����ϴ�
            else if(index + (normalOfNote * 0.55f) > floatOfNote[i] &&
                    index - (normalOfNote * 0.55f) < floatOfNote[i] &&
                    objectOfNote[i].activeSelf == true)
            {
                damageManager.SwordClip(Random.Range(3, 5));
                objectOfNote[i].SetActive(false);
                missCheck = true;

                // �������� ����Ͽ� �����ϴ�
                damageManager.CalculationOfDamage(FightOfListList[i]);

            }

        }

        if(missCheck == false)
        {
            damageManager.MissClip();
        }


    }
    #endregion
}

/// <summary>
/// �����ڿ� ����ڰ� ���Ե� ���������� ���� ����ü�Դϴ�
/// </summary>
public struct StructOfFight
{
    public ParentsOfParty attacker;
    public ParentsOfParty defender;

    public StructOfDamage structOfDamage;

    public StructOfFight(ParentsOfParty attacker, ParentsOfParty defender, StructOfDamage structOfDamage)
    {
        this.attacker = attacker;
        this.defender = defender;
        this.structOfDamage = structOfDamage;
    }
}

/// <summary>
/// ������ ������ ������ ���� ����ü �Դϴ�
/// </summary>
public struct StructOfDamage
{
    public int damage;
    public int damageOfStamina;
    public bool splash;
    public int numberOfNote;

    public StructOfDamage(int a = 1, int b = 1, bool c = false, int d = 1)
    {
        damage = a;
        damageOfStamina = b;
        splash = c;
        numberOfNote = d;
    }
}
