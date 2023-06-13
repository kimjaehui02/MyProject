using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public Text text;

    public string inputs;

    public bool type;

    private void Update()
    {
        if(type)
        text.text = inputs + (GameManager.instance.Gold);
        else
        text.text = inputs + (GameManager.instance.HpItem);

    }

}
