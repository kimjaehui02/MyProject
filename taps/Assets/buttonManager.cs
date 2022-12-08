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

    private void OnMouseEnter()
    {
        game.SetActive(true);
    }

    private void OnMouseOver()
    {
        //game.GetComponent<RectTransform>().position = Input.mousePosition;
        Vector3 vector3 = new Vector3(Input.mousePosition.x + intOfX, Input.mousePosition.y + intOfY);
        game.transform.position = vector3;
        game.transform.GetChild(0).GetComponent<Text>().text = testText;
        game.transform.GetChild(1).GetComponent<Text>().text = testText2;
        //Debug.Log(132);
    }

    private void OnMouseExit()
    {
        game.SetActive(false);
    }
}
