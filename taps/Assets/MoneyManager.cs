using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text text;

    private void Update()
    {
        text.text = "Gold : " + (GameManager.instance.Gold);
    }

}
