using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBattle : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    /// <summary>
    /// 전투용 노트 4박자
    /// </summary>
    public List<int> listIntOfBattleNote;

    /// <summary>
    /// 재빨리 쳐야하는 시간
    /// </summary>
    public float floatOfTimeMax;

    /// <summary>
    /// 재빨리 쳐야하는시간 현재
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
    /// 이 함수를 호출하면 전투 화면이 나옵니다
    /// </summary>
    public void BattleStarter()
    {

        var gm = GameManager.instance.listRealPlayer;


        // 배틀용 오브젝트와 ui를 활성화 합니다
        gameObjectOfBattleObject.SetActive(true);
        gameObjectOfBattleUi.SetActive(true);




        // 플레이어측을 생성합니다
        for (int i = 0; i < gm.Count; i++)
        {
            var party = Instantiate(sceneStartManager.listGameObjectOfParty[gm[i].intOfType]);
            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(0);

            party.GetComponent<PlayerObjectManager>().Setting(gm[i].floatOfHp, gm[i].floatOfStamina);

            listGameObjectOfPlayer.Add(party);
        }


        // 적군측을 생성합니다
        for (int i = 0; i < 4; i++)
        {
            var party = Instantiate(sceneStartManager.listGameObjectOfPartyEnemy[i]);



            party.transform.parent = gameObjectOfBattleObject.transform.GetChild(1);
            listGameObjectOfEnemy.Add(party);
        }


        //boolOfStartEnd = true;
    }

    /// <summary>
    /// 이 함수를 호출하면 전투가 끝납니다
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
