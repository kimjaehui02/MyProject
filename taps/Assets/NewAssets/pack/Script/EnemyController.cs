using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public bool isGrounded = true;

    public IEnumerator SetGroundedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isGrounded = true;
    }

    private void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        if (isGrounded)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }
}