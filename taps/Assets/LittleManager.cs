using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleManager : MonoBehaviour
{
    public GameObject game;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log(123);
            if(game.activeSelf == true)
            {
                game.SetActive(false);
            }
            else
            {
                game.SetActive(true);
            }

        }
    }

}
