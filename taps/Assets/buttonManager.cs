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
}
