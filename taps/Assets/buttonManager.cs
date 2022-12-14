using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    public GameObject game;
    // Update is called once per frame

    public string testText;
    public string testText2;

    public int intOfX;
    public int intOfY;

    private void Start()
    {
        if(game == null)
        {
            Debug.Log("start");
            game = GameObject.Find("UITEXT");
        }
    }

    private void OnMouseEnter()
    {
        if (game == null)
        {
            Debug.Log("enter");

            game = GameObject.Find("UITEXT");
        }
        game.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (game == null)
        {
            Debug.Log("over");

            game = GameObject.Find("UITEXT");
        }
        //game.GetComponent<RectTransform>().position = Input.mousePosition;
        Vector3 vector3 = new Vector3(Input.mousePosition.x + intOfX, Input.mousePosition.y + intOfY);
        game.transform.position = vector3;
        game.transform.GetChild(0).GetComponent<Text>().text = testText;
        game.transform.GetChild(1).GetComponent<Text>().text = testText2;
        //Debug.Log(132);
    }

    private void OnMouseExit()
    {
        if (game == null)
        {
            Debug.Log("exit");

            game = GameObject.Find("UITEXT");
        }
        game.SetActive(false);
    }

    private void OnMouseUpAsButton()
    {
        Clicking();
    }

    public int input;

    public void Clicking()
    {
        switch (input)
        {
            case 0:
                if(GameManager.instance.Gold >= 500)
                {
                    GameManager.instance.Gold -= 500;
                    for (int i = 0; i < GameManager.instance.listRealPlayer.Count; i++)
                    {
                        GameManager.instance.listRealPlayer[i].LEVEL++;
                    }
                }
                break;
            case 1:
                if (GameManager.instance.Gold >= 300)
                {
                    GameManager.instance.Gold -= 300;
                    for (int i = 0; i < GameManager.instance.listRealPlayer.Count; i++)
                    {
                        GameManager.instance.listRealPlayer[i].floatOfHp++;
                    }
                }

                break;
            case 2:
                if (GameManager.instance.Gold >= 100)
                {
                    GameManager.instance.Gold -= 100;

                    GameManager.instance.HpItem++;
                }

                break;

        }

    }
}
