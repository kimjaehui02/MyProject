using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool canBeParried = true;
    public float moveSpeed = 2.0f;
    public bool isGrounded = true;

    public float pushForce = 10f;
    public float pushDistance = 0.75f;
    public float flashDuration = 0.1f;
    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.1f;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        MoveLeft();
        CheckPlayerDistance();
    }

    private void MoveLeft()
    {
        if (isGrounded)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    private void CheckPlayerDistance()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= pushDistance)
            {
                PushCharacter(player);
            }
        }
    }

    private void PushCharacter(GameObject character)
    {
        StartCoroutine(FlashAndShake());
        Rigidbody2D characterRb = character.GetComponent<Rigidbody2D>();
        Vector2 pushDirection = character.transform.position - transform.position;
        pushDirection.Normalize();
        characterRb.velocity = Vector2.zero;
        characterRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
    }

    private IEnumerator FlashAndShake()
    {
        spriteRenderer.color = Color.red;

        Vector3 originalPosition = transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
    }

    public void Parry()
    {
        // 乔拜 贸府 肺流 备泅
    }

    public bool CanBeParried()
    {
        return canBeParried;
    }

    public IEnumerator SetGroundedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isGrounded = true;
    }
}
