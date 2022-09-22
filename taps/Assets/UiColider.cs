using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiColider : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(name);
        Debug.Log(name);
    }
}
