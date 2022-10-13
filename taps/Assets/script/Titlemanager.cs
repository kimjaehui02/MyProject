using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlemanager : MonoBehaviour
{

    public void MoveScene()
    {
        GameManager.instance.MoveScene("TownScene");
    }
}
