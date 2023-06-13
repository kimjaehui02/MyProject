using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniChar : MonoBehaviour
{
    public int number;

    public List<GameObject> gameObjects;
    public List<GameObject> gameObjects2;
    public Text text;

    public List<Color> GetColor;

    public Button Buttons;

    private void Update()
    {
        text.text = "Lv." + (GameManager.instance.listRealPlayer[number].LEVEL);

        if (GameManager.instance.listRealPlayer[number].floatOfHp == 0)
        {
            gameObjects2[0].GetComponent<Image>().color = GetColor[3];
            gameObjects2[1].GetComponent<Image>().color = GetColor[3];


        }
        else
        {
            gameObjects2[0].GetComponent<Image>().color = GetColor[2];
            gameObjects2[1].GetComponent<Image>().color = GetColor[2];
        }

        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].SetActive(false);
        }

        for(int i=0; i < GameManager.instance.listRealPlayer[number].LEVEL+3; i++)
        {
            gameObjects[i].SetActive(true);
            gameObjects[i].GetComponent<Image>().color = GetColor[2];
        }

        for (int i = 0; i < GameManager.instance.listRealPlayer[number].floatOfHp; i++)
        {
            gameObjects[i].GetComponent<Image>().color = GetColor[0];
        }


        if(GameManager.instance.HpItem == 0)
        {
            Buttons.interactable = false;
        }
        else
        {
            Buttons.interactable = true;
        }

    }

    public void ButtonCLick()
    {
        GameManager.instance.HpItem--;
        GameManager.instance.listRealPlayer[number].floatOfHp++;
    }

}
