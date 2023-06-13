using System.Collections.Generic;
using UnityEngine;

public class HeroKnightNew : MonoBehaviour
{
    public float speed = 4.0f;
    public float jumpPower = 7.5f;
    public List<Animator> animators;
    public Rigidbody2D rb;
    public Sensor_HeroKnightTab2 groundSensor;
    private bool isGrounded = false;
    private bool isAttacking = false; // 추가: 공격 중 여부를 저장하는 변수
    private float attackTime = 0.0f;
    private float idleTime = 0.0f;

    public List<SpriteRenderer> spriteRenderers;

    private bool isBlocking = false;

    public float attackRange = 2.0f;
    public LayerMask enemyLayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
    }

    private void Update()
    {
        UpdateMovement();
        HandleAttacks();
        HandleParry();
        HandleBlock();
        UpdateAnimation();
    }

    private void UpdateMovement()
    {
        float inputX = Input.GetAxis("Horizontal");

        if (!isAttacking) // 수정: 공격 중이 아닐 때만 방향 전환 처리
        {
            if (inputX > 0)
            {
                FlipSprites(false);
            }
            else if (inputX < 0)
            {
                FlipSprites(true);
            }
        }

        rb.velocity = new Vector2(inputX * speed, rb.velocity.y);

        isGrounded = groundSensor.State();

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Space 키로 점프
        {
            rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);

            foreach (var animator in animators)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    private int attackNumber = 0;

    private void HandleAttacks()
    {
        attackTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Z) && attackTime > 0.25f && !isBlocking)
        {
            attackNumber++;
            if (attackNumber > 3)
                attackNumber = 1;

            if (attackTime > 1.0f)
                attackNumber = 1;

            foreach (var animator in animators)
            {
                animator.SetTrigger("Attack" + attackNumber);
            }

            attackTime = 0.0f;



            if (IsAttacking())
            {

                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.CompareTag("Enemy"))
                    {
                        AttackEnemy(collider.gameObject);
                    }
                }
            }
        }
    }


    private void HandleParry()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            PerformParry();
        }
    }

    private void PerformParry()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Vector2.one, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                // LittleEnemy enemy = collider.GetComponent<LittleEnemy>();
                // if (enemy != null && enemy.CanBeParried())
                // {
                //     enemy.Parry();
                // }
            }
        }
    }

    private void HandleBlock()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isBlocking && !isAttacking) // 수정: 공격 중이 아닐 때만 블록 가능
        {
            isBlocking = true;

            foreach (var animator in animators)
            {
                animator.SetBool("IdleBlock", true);
                animator.SetTrigger("Block");
            }
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            isBlocking = false;

            foreach (var animator in animators)
            {
                animator.SetBool("IdleBlock", false);
            }
        }
    }

    private void UpdateAnimation()
    {
        foreach (var animator in animators)
        {
            animator.SetFloat("AirSpeedY", rb.velocity.y);
            animator.SetBool("Grounded", isGrounded);
            animator.SetBool("IdleBlock", isBlocking);

            if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
            {
                animator.SetInteger("AnimState", 1);
                idleTime = 0.05f;
            }
            else
            {
                idleTime -= Time.deltaTime;
                if (idleTime < 0)
                {
                    animator.SetInteger("AnimState", 0);
                }
            }
        }
    }

    private void FlipSprites(bool flipX)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.flipX = flipX;
        }
    }

    private void AttackEnemy(GameObject enemy)
    {
        // 적을 공격하는 로직 작성
        // 예시: enemy.GetComponent<Enemy>().TakeDamage(damageAmount)

        SpriteRenderer[] childRenderers = enemy.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in childRenderers)
        {
            StartCoroutine(FlashAndShake(renderer));
        }

        // 공격 로직 실행 후 공격 상태 종료
        isAttacking = false;
    }

    private IEnumerator<WaitForSeconds> FlashAndShake(SpriteRenderer renderer)
    {
        float flashDuration = 0.1f;
        float shakeDuration = 0.1f;
        float shakeMagnitude = 0.1f;

        // 적 오브젝트를 빨강색으로 변경
        Color originalColor = renderer.color;
        renderer.color = Color.red;

        // 순간적인 피격 효과를 위한 위치 진동
        Vector3 originalPosition = renderer.transform.localPosition;
        float elapsedTime = 0f;
        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            renderer.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        renderer.transform.localPosition = originalPosition;

        // 일정 시간 대기
        yield return new WaitForSeconds(flashDuration);

        // 적 오브젝트의 색상 원상 복구
        renderer.color = originalColor;
    }

    private bool IsAttacking()
    {
        foreach (var animator in animators)
        {

            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("Attack1") && currentState.normalizedTime < 1f)
            {
                Debug.Log(11111111111111);
                return true;
            }
            if (currentState.IsName("Attack2") && currentState.normalizedTime < 1f)
            {
                Debug.Log(222222222222222);
                return true;
            }
            if (currentState.IsName("Attack3") && currentState.normalizedTime < 1f)
            {
                Debug.Log(33333333333);
                return true;
            }
        }
        return false;
    }
}
