using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBattleManager : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    #region  ������Ʈ

    /// <summary>
    /// ���� �Ŵ���
    /// </summary>
    //public GameObject gameObjectOfSoundManager;

    /// <summary>
    /// ���� ī�޶�
    /// </summary>
    //public GameObject gameObjectOfMainCamera;

    /// <summary>
    /// ��� ������Ʈ��
    /// </summary>
    //public GameObject gameObjectOfBackGround;






    #endregion

    #region �뵵��

    #region ����

    /// <summary>
    /// ��������� ������ �˷��ִ� ���Դϴ�
    /// </summary>
    public bool boolOfStartEnd;

    #endregion

    #region ���� ȭ�� ������Ʈ��

    /// <summary>
    /// ������ ī�޶� �ڽĿ�����Ʈ�� ��Ʋ ������Ʈ���Դϴ�
    /// </summary>
    public GameObject gameObjectOfBattleObject;

    /// <summary>
    /// ������ �ڽ��� �ڽ� ������Ʈ�� �ִ� ĵ�����Դϴ�
    /// </summary>
    public GameObject gameObjectOfBattleUi;

    #endregion

    #region ��� ������ ui

    /// <summary>
    /// ����� ����� �����ִ� ui
    /// </summary>
    public GameObject gameObjectOfPhaseUi;

    /// <summary>
    /// ����� ��Ÿ���� 0= ���� 1= 
    /// </summary>
    public int intOfPhase;



    #endregion

    #region �ߴ� ��Ʈ ui

    /// <summary>
    /// �ߴ��� ��Ʈ���� �����ִ� Ui
    /// </summary>
    public GameObject gameObjectOfNoteUi;

    /// <summary>
    /// ��Ʈ���Դϴ�
    /// </summary>
    public List<GameObject> listGameObjectOfNote;

    /// <summary>
    /// üũ���� ǥ���ϴ� ������Ʈ�Դϴ�
    /// </summary>
    public GameObject gameObjectOfCheck;

    /// <summary>
    /// ��Ʈ�� �����ٴϴ� ���� ����
    /// </summary>
    public float floatOfRouteOfNote;

    /// <summary>
    /// ��Ʈ�� �����ִ� �ð�
    /// </summary>
    public float floatOfRemainOfNote;

    /// <summary>
    /// ��Ʈ�� ��Ȯ�� ��ġ�Ǵ� �ð�
    /// </summary>
    public float floatOfJustTimeOfNote;

    /// <summary>
    /// ��Ʈ�� ���� ��ġ�Ǵ� �ð�
    /// </summary>
    public float floatOfNomalTimeOfNote;

    /// <summary>
    /// �ð����� �����Ͽ� ���ݴ��ϴ� �ð��� üũ�Ҷ� ���ϴ�
    /// </summary>
    public float floatOfCheckTime;

    /// <summary>
    /// ��Ʈ�� ��ġ�� ������ȭ �� ���� ����Ʈ�� ��������ϴ�
    /// </summary>
    public List<float> listFloatOfNotePosition;

    #endregion

    #region �÷��̾�� ����
    /// <summary>
    /// �÷��̾� ��Ƽ
    /// </summary>
    public List<GameObject> listGameObjectOfParty;

    /// <summary>
    /// �� ��Ƽ
    /// </summary>
    public List<GameObject> listGameObjectOfEnemyParty;

    /// <summary>
    /// ��Ŀ���ϴ� �÷��̾��Դϴ�
    /// </summary>
    public int intOfPlayerFocus;

    /// <summary>
    /// ��Ŀ���ϴ� ��ĳ�����Դϴ�
    /// </summary>
    public int intOfEnemyFocus;

    /// <summary>
    /// �Ʊ��� ��ġ
    /// </summary>
    public List<Vector3> listVector3OfPlayerPosition;



    /// <summary>
    /// �÷��̾� ĳ���� ������
    /// </summary>
    public List<GameObject> listGameObjectOfPlayerPrefab;

    /// <summary>
    /// �� ĳ���͵� ������
    /// </summary>
    public List<GameObject> listGameObjectOfEnemyPrefab;

    #endregion

    #endregion

    private void Update()
    {
        if(boolOfStartEnd == true)
        {
            NomalUpdate();
            BattleUpdate();
            Notetransform();
            NoteOutCheck();
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            listGameObjectOfParty[intOfPlayerFocus].GetComponent<PlayerObjectManager>().Animinput();

            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
            Check();
        }
    }


    #region �������� ó��

    /// <summary>
    /// �⺻���� �÷��̾� ĳ���͵��� ��ġ�մϴ�
    /// </summary>
    private void NomalUpdate()
    {
        // �÷��̾���� ��ġ�� ���� Ȯ�� ũ�⸦ ���մϴ�
        for (int i = 0; i < listGameObjectOfParty.Count; i++)
        {
            // ĳ���͵��� ��ġ�մϴ�
            listGameObjectOfParty[(i + intOfPlayerFocus) % listGameObjectOfParty.Count].transform.localPosition = listVector3OfPlayerPosition[i];

            // ��ġ�� 0�̸� Ȯ���ϰ� �ƴϸ� ����մϴ�
            if (i == 0)
            {
                listGameObjectOfParty[(i + intOfPlayerFocus) % listGameObjectOfParty.Count].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                listGameObjectOfParty[(i + intOfPlayerFocus) % listGameObjectOfParty.Count].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            }

        }

        // �� ĳ���͵� ���� ���ݴϴ�
        for (int i = 0; i < listGameObjectOfEnemyParty.Count; i++)
        {
            // ĳ���͵��� ��ġ�մϴ�
            listGameObjectOfEnemyParty[(i + intOfPlayerFocus) % listGameObjectOfEnemyParty.Count].transform.localPosition = listVector3OfPlayerPosition[i];


            if (i == 0)
            {
                listGameObjectOfEnemyParty[(i + intOfPlayerFocus) % listGameObjectOfEnemyParty.Count].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                listGameObjectOfEnemyParty[(i + intOfPlayerFocus) % listGameObjectOfEnemyParty.Count].transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

            }

        }


    }

    /// <summary>
    /// Ű�� ������ ĳ���͸� ��ü�մϴ�
    /// </summary>
    private void BattleUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            intOfPlayerFocus++;
            intOfPlayerFocus %= listGameObjectOfParty.Count;
        }
    }

    #region ��Ʈ���� ó��

    #region ��Ʈ�߿����� �������� �ֵ�

    /// <summary>
    /// ���� ������ ���� ��Ʈ�� óġ�մϴ�
    /// </summary>
    private void NoteOutCheck()
    {
        for (int i = 0; i < listFloatOfNotePosition.Count; i++)
        {
            if(listFloatOfNotePosition[i] > floatOfRemainOfNote)
            {
                listGameObjectOfNote[i].SetActive(false);
                listFloatOfNotePosition[i] = 0f;
            }
        }
    }



    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// ��Ʈ�� ��ǥ�� ���
    /// </summary>
    private void Notetransform()
    {

        // ��Ʈ���� �Ⱦ�� �̵��� �����ݴϴ�
        for (var i = 0; i < listGameObjectOfNote.Count; i++)//floatOfNote.Count; i++)
        {
            if (listGameObjectOfNote[i].activeSelf == false)
            {
                continue;
            }


            // ��Ʈ���� �ð����� ���� �̵��ϰ� ����ϴ�
            listFloatOfNotePosition[i] += Time.fixedDeltaTime;


        }



        // ���̸� �ð����� ����� ��Ʈ�� �̵��ϴ� �ð��� ����
        var speedOfNote = floatOfRouteOfNote / floatOfRemainOfNote;

        // ��Ʈ�� ���̸� �����ϱ�(��Ʈ�� �ӵ� * ���� �ð�)
        var size = new Vector3(speedOfNote* floatOfJustTimeOfNote, 100, 0);
        var size2 = new Vector3(speedOfNote* floatOfNomalTimeOfNote, 100, 0);

        for (var i = 0; i < listFloatOfNotePosition.Count; i++)
        {
            if (listGameObjectOfNote[i].activeSelf == false)
            {
                continue;
            }
            // ������ ���� ��Ȯ�� ���������� ǥ���մϴ�
            listGameObjectOfNote[i].transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = size;
            // ������ ���� ���� ���������� ǥ���մϴ�
            listGameObjectOfNote[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = size2;
        }


        // ��Ʈ�� ��ǥ�� �����ϱ�(��Ʈ�� �ӵ� * ��Ʈ�� ���� �ð�)
        for (int i = 0; i < listFloatOfNotePosition.Count; i++)
        {
            if (listGameObjectOfNote[i] == null)
            {
                continue;
            }
            var moving = speedOfNote * listFloatOfNotePosition[i] - (floatOfRouteOfNote * 0.5f);
            listGameObjectOfNote[i].transform.localPosition = new Vector3(-moving, 0, 0);
        }

        //noteCheck.transform.localPosition = new Vector3(speedOfNote * floatOfNote[0]-750, 0, 0);

        // üũ���� ǥ���ϱ�
        float index = (floatOfCheckTime - 0.5f) * floatOfRouteOfNote;
        gameObjectOfCheck.transform.localPosition = new Vector3(-index, 0, 0);

    }


    #endregion
    /// <summary>
    /// ��Ʈ���� �����մϴ�
    /// </summary>
    public void NoteOn()
    {
        foreach (GameObject i in listGameObjectOfNote)
        {
            if (i.activeSelf == false)
            {
                i.SetActive(true);

                i.GetComponent<RectTransform>().localPosition = new Vector3(500, 0, 0);

                break;
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// ��ư�� �������� �����Ͽ� ��Ʈ�� ��ġ�� üũ�մϴ�
    /// </summary>
    public int Check()
    {
        // ������ �����մϴ�
        int able = 0;

        // ��Ÿ�� �ƴ����� �˻��մϴ�
        bool missCheck = false;
        // üũ������ ���ϱ����ؼ� ������� �����ݴϴ� ���ӽð��� 75�ۼ�Ʈ ������ ��Ÿ���ϴ�
        float index = (floatOfCheckTime) * floatOfRemainOfNote;

        // ��Ʈ���� ��ȸ�ϸ鼭 üũ�մϴ�
        for (int i = 0; i < listGameObjectOfNote.Count; i++)
        {
            // ��Ȱ��ȭ�Ǿ ������ ���� ��Ʈ�� �˻������ʽ��ϴ�
            if (listGameObjectOfNote[i].activeSelf == false)
            {
                continue;
            }

            // �յڷ� ���ݾ� ������ ������ ������ ��Ÿȿ���� ����
            // ��Ȯ�� ������ noteCheckfloat * remainOfNote

            // ��Ʈ�� ������ �´��� üũ�� �մϴ�
            if (index + (floatOfJustTimeOfNote * 0.55f) > listFloatOfNotePosition[i] &&
                index - (floatOfJustTimeOfNote * 0.55f) < listFloatOfNotePosition[i] &&
                listGameObjectOfNote[i].activeSelf == true)
            {
                // �������� �Ҹ�Ŭ���� ����մϴ�
                //damageManager.SwordClip(Random.Range(3, 5));

                NoteAttack();
                // ��ġ ���󺹱�

                listGameObjectOfNote[i].GetComponent<RectTransform>().localPosition = new Vector3(500, 0, 0);
                listFloatOfNotePosition[i] = 0;
                // ���������� ��Ʈ�� ��Ȱ��ȭ �մϴ�
                listGameObjectOfNote[i].SetActive(false);
                // �������� ����Ͽ� �����ϴ�
                //damageManager.CalculationOfDamage(List2StructOfFightOfCastEnemy[i]);
                able = 2;
                missCheck = true;

                break;
            }
            // ��Ȯ�� �������� ���ߴٸ� ����ϰԶ� �����ߴ��� �����ϴ�
            else if (index + (floatOfNomalTimeOfNote * 0.55f) > listFloatOfNotePosition[i] &&
                    index - (floatOfNomalTimeOfNote * 0.55f) < listFloatOfNotePosition[i] &&
                    listGameObjectOfNote[i].activeSelf == true)
            {
                //damageManager.SwordClip(Random.Range(3, 5));
                NoteAttack();
                // ��ġ ���󺹱�
                listGameObjectOfNote[i].GetComponent<RectTransform>().localPosition = new Vector3(500, 0, 0);
                listFloatOfNotePosition[i] = 0;

                listGameObjectOfNote[i].SetActive(false);
                missCheck = true;

                // �������� ����Ͽ� �����ϴ�
                //damageManager.CalculationOfDamage(List2StructOfFightOfCastEnemy[i]);
                able = 1;

                break;
            }

        }

        if (missCheck == false)
        {
            //damageManager.MissClip();
        }

        return able;

    }


    /// <summary>
    /// ��Ʈ�� ���������� �� �۾����Դϴ�
    /// </summary>
    /// <param name="checks"></param>
    public void NoteAttack(int checks = 0)
    {
        listGameObjectOfParty[intOfPlayerFocus].GetComponent<PlayerObjectManager>().Animinput();

        switch (checks)
        {
            case 2:

                break;

            case 1:
                break;

            default:

                break;

        }
        listGameObjectOfParty[intOfPlayerFocus].GetComponent<PlayerObjectManager>().StaDamage();
    }

    #endregion

    #endregion

    #region �ܹ����� ó��

    /// <summary>
    /// �� �Լ��� ȣ���ϸ� ���� ȭ���� ���ɴϴ�
    /// </summary>
    public void BattleStarter()
    {

        var gm = GameManager.instance.listRealPlayer;


        // ��Ʋ�� ������Ʈ�� ui�� Ȱ��ȭ �մϴ�
        gameObjectOfBattleObject.SetActive(true);
        gameObjectOfBattleUi.SetActive(true);




        // �÷��̾����� �����մϴ�
        for (int i = 0; i < gm.Count; i++)
        {
            var party = Instantiate(listGameObjectOfPlayerPrefab[gm[i].intOfType]);
            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(0);

            party.GetComponent<PlayerObjectManager>().Setting(gm[i].floatOfHp, gm[i].floatOfStamina);

            listGameObjectOfParty.Add(party);
        }


        // �������� �����մϴ�
        for (int i = 0; i < 4; i++)
        {
            var party = Instantiate(listGameObjectOfEnemyPrefab[i]);



            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(1);
            listGameObjectOfEnemyParty.Add(party);
        }

        NomalUpdate();

        boolOfStartEnd = true;
    }

    /// <summary>
    /// �� �Լ��� ȣ���ϸ� ������ �����ϴ�
    /// </summary>
    public void BattleEnder()
    {

        gameObjectOfBattleObject.SetActive(false);
        gameObjectOfBattleUi.SetActive(false);

        foreach (var i in listGameObjectOfParty)
        {
            Destroy(i);
        }

        foreach (var i in listGameObjectOfEnemyParty)
        {
            Destroy(i);
        }

        listGameObjectOfParty.Clear();
        listGameObjectOfEnemyParty.Clear();

    }

    #endregion











    #region ����

    // ���ο� ������ ȭ���� ��ȯ�Ǵ°� �ӵ����� �پ�峮
    // �׸��� �������� ���� ��ũ��Ʈ�� �̹� �������ؼ� ����� ����ϴ�
    // �׷��� ���Ӱ� ���� ��ũ��Ʈ�̴�
    // �ʿ��� �͵��� ������ ����

    // ������Ʈ��

    // -    ���ΰ� ��Ƽ��
    // -    �� ��Ƽ��

    // �⺻���� ĳ���͵��� ������ ���ؼ� ��װ� �ʿ��ϴ�

    // -    (���) ���� �����Ȳ ui
    // -    (�ߴ�) ���� ��Ʈ Ui

    // ����� ui�� ���� ������ �������ؼ� �ʿ��ϰ�
    // �ߴ��� ui�� �����Ҷ� ��Ʈ�� ���� �����ϱ����ؼ� �ʿ��ϴ�


    // �⺻���� ����

    // ���� ������ ��Ʈ�� ������ ������ �ִ�
    // ���� ������ ������� �ڽ��� ��Ʈ��ŭ ��Ʈ�� �����Ѵ�
    // �߾ӿ� ������ ��Ʈ���� ���̴µ�
    // ������ ��Ʈ�� ��ġ�� �����ȴ�
    // �÷��̾ ��ġ�� �°� ��Ʈ�� ������ �ش� ��Ʈ�� �ı��ǰ�
    // ��� ��Ʈ�� �ı��Ǹ� �������� ��ư�� ���� ���� ó���� �����ϴ�

    // ��Ʈ�� ������ �ൿ�� �Ҷ����� �÷��̾�� �Ƿΰ� ���δ�
    // �Ƿδ� �������� ������ �ش� ��ġ��ŭ �߰����ظ� �԰� �����
    // �Ƿΰ� ���ϰ� �����Ǹ� ���ظ� ���� �ʾƵ� �����ϸ� ����� ���°� �ȴ�
    // �̸� �������� �Ƿΰ� ���̸� ��ü�� �ؾ� �Ѵ�

    #endregion
}
