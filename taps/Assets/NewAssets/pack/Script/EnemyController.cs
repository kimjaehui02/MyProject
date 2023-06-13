using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool canBeParried = true;
    public float moveSpeed = 2.0f;

    private void Update()
    {
        MoveLeft();
    }

    private void MoveLeft()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }

    public void Parry()
    {
        // 乔拜 贸府 肺流 备泅
    }

    public bool CanBeParried()
    {
        return canBeParried;
    }
}
