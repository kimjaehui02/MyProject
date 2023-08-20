using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testest : MonoBehaviour
{
    public BoxCollider2D boxCollider2;
    public GameObject gameObject2222;

    public List<GameObject> gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2 = gameObject.AddComponent<BoxCollider2D>();
        gameObjects.Add(Instantiate(gameObject2222));
        gameObjects.Add(Instantiate(gameObject2222));
        gameObjects.Add(Instantiate(gameObject2222));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
