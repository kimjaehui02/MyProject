using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TownManager : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Vector3 cameraPosition;

    public GameObject mainCamera;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;

    public float moveSpeed1;
    public float moveSpeed2;

    public GameObject GameObject1;
    public GameObject GameObject2;

    public string[] GetVs;
    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        LimitCameraArea();
        BackGroundManage();
    }

    void LimitCameraArea()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        float lx = mapSize.x - width;
        float clampX = Mathf.Clamp(mainCamera.transform.position.x, -lx + center.x, lx + center.x);

        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(mainCamera.transform.position.y, -ly + center.y, ly + center.y);

        mainCamera.transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }



    

    public void BackGroundManage()
    {
        float test = playerTransform.position.x / (mapSize.x * 2);
        GameObject1.transform.position = new Vector3( -moveSpeed1 * test, GameObject1.transform.position.y, GameObject1.transform.position.z);
        
        GameObject2.transform.position = new Vector3( -moveSpeed2 * test, GameObject2.transform.position.y, GameObject2.transform.position.z);

        if(playerTransform.position.x > mapSize.x)
        {
            GameManager.instance.MoveScene(GetVs[1]);

        }

        if (playerTransform.position.x < -mapSize.x)
        {
            GameManager.instance.MoveScene(GetVs[0]);
        }
    }

    public GameObject Game;

    private void Update()
    {
        Game.GetComponent<RectTransform>().position = Input.mousePosition;
    }
}
