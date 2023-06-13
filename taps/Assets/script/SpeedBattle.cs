using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBattle : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    /// <summary>
    /// ������ ��Ʈ 4����
    /// </summary>
    public List<int> listIntOfBattleNote;

    /// <summary>
    /// �绡�� �ľ��ϴ� �ð�
    /// </summary>
    public float floatOfTimeMax;

    /// <summary>
    /// �绡�� �ľ��ϴ½ð� ����
    /// </summary>
    public float floatOfTime;

    public List<GameObject> listGameObjectOfPlayer;
    public List<GameObject> listGameObjectOfEnemy;

    public List<Vector3> listVector3OfPlayer;

    public List<Vector3> listVector3OfEnemy;

    public GameObject gameObjectOfBattingUi;


    public int number;



    private void Update()
    {
        vector();
    }

    public void vector()
    {
        for (int i = 0; i < listGameObjectOfPlayer.Count; i++)
        {
            if(listGameObjectOfPlayer[i] == null)
            {
                continue;
            }
            listGameObjectOfPlayer[i].transform.localPosition = listVector3OfPlayer[i];

        }

        for (int i = 0; i < listGameObjectOfEnemy.Count; i++)
        {
            if (listGameObjectOfEnemy[i] == null)
            {
                continue;
            }
            listGameObjectOfEnemy[i].transform.localPosition = listVector3OfEnemy[i];

        }
    }

    public void Battleing()
    {


    }

    public void OpenBattle(List<int> intint)
    {
        //sceneStartManager.
    }
       
    public void Damaged(bool attack)
    {
        if(attack == true)
        {
            for (int i = 0; i < GameManager.instance.listRealPlayer.Count; i++)
            {
                if(GameManager.instance.listRealPlayer[i].floatOfHp == 0)
                {
                    continue;
                }

                GameManager.instance.listRealPlayer[i].floatOfHp--;
                break;
            }


        }
        else
        {
            listGameObjectOfEnemy[0].GetComponent<PlayerObjectManager>().intOfHp--;
            if (listGameObjectOfEnemy[0].GetComponent<PlayerObjectManager>().intOfHp == 0)
            {
                GameObject @object = listGameObjectOfEnemy[0];
                listGameObjectOfEnemy.RemoveAt(0);
                Destroy(@object);

            }
        }
    }

    public GameObject gameObjectOfBattleObject;
    public GameObject gameObjectOfBattleUi;

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
            var party = Instantiate(sceneStartManager.listGameObjectOfParty[gm[i].intOfType]);
            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(0);

            party.GetComponent<PlayerObjectManager>().Setting(gm[i].floatOfHp, gm[i].floatOfStamina);

            listGameObjectOfPlayer.Add(party);
        }


        // �������� �����մϴ�
        for (int i = 0; i < 4; i++)
        {
            var party = Instantiate(sceneStartManager.listGameObjectOfPartyEnemy[i]);



            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(1);
            listGameObjectOfEnemy.Add(party);
        }


        //boolOfStartEnd = true;
    }

    /// <summary>
    /// �� �Լ��� ȣ���ϸ� ������ �����ϴ�
    /// </summary>
    public void BattleEnder()
    {

        gameObjectOfBattleObject.SetActive(false);
        gameObjectOfBattleUi.SetActive(false);

        foreach (var i in listGameObjectOfPlayer)
        {
            Destroy(i);
        }

        foreach (var i in listGameObjectOfEnemy)
        {
            Destroy(i);
        }

        listGameObjectOfPlayer.Clear();
        listGameObjectOfEnemy.Clear();

    }




}
