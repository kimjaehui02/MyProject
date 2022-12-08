using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RealCameraManager : MonoBehaviour
{
    public SceneStartManager sceneStartManager;

    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    Vector3 cameraPosition;

    public GameObject mainCamera;

    [SerializeField]
    Vector2 center;


    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;








    void Start()
    {
        //playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }


    void FixedUpdate()
    {
        LimitCameraArea();

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


    public void SceneStart(Transform playerTransform,
                           Vector2 mapSize,
                           GameObject mainCamera)
    {
        this.playerTransform = playerTransform;
        this.mapSize = mapSize;
        this.mainCamera = mainCamera;

    }



}
