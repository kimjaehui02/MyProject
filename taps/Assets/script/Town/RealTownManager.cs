using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTownManager : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Vector2 mapSize;

    public string[] GetVs;

    public GameObject GameObject1;
    public GameObject GameObject2;

    public float moveSpeed1;
    public float moveSpeed2;

    private void Update()
    {

        BackGroundManage();

    }

    public void BackGroundManage()
    {
        float test = playerTransform.position.x / (mapSize.x * 2);
        GameObject1.transform.position = new Vector3(-moveSpeed1 * test, GameObject1.transform.position.y, GameObject1.transform.position.z);

        GameObject2.transform.position = new Vector3(-moveSpeed2 * test, GameObject2.transform.position.y, GameObject2.transform.position.z);



        if (playerTransform.position.x > mapSize.x)
        {
            //Debug.Log(123);
            GameManager.instance.MoveScene(GetVs[1], +1);

        }

        if (playerTransform.position.x < -mapSize.x)
        {
            GameManager.instance.MoveScene(GetVs[0], -1);
        }
    }

    public void SceneStart(Transform playerTransform,
                           Vector2 mapSize,     
                           GameObject GameObject1, 
                           GameObject GameObject2)
    {
        this.playerTransform = playerTransform;
        this.mapSize = mapSize;
        this.GameObject1 = GameObject1;
        this.GameObject2 = GameObject2;

    }
}
