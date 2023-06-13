using System.Collections.Generic;
using UnityEngine;

public class HeroKnightNew : MonoBehaviour
{
    public float speed = 4.0f; // 플레이어 이동 속도
    public float jumpPower = 7.5f; // 점프 힘
    public List<Animator> animators; // 애니메이터 리스트
    public Rigidbody2D rb; // Rigidbody2D 컴포넌트
    public Sensor_HeroKnightTab2 groundSensor; // 바닥 감지 센서 컴포넌트
    private bool isGrounded = false; // 바닥에 닿은 상태인지 여부
    private bool isAttacking = false; // 공격 중인지 여부
    private float attackTime = 0.0f; // 공격 시간
    private float idleTime = 0.0f; // 대기 시간
    private bool isBlocking = false; // 방어 중인지 여부
    public float attackRange = 2.0f; // 공격 범위
    public LayerMask enemyLayer; // 적 레이어
    public List<SpriteRenderer> spriteRenderers; // 스프라이트 렌더러 리스트

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 가져오기
        // groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>(); // GroundSensor 컴포넌트 가져오기
    }

    private void Update()
    {
        UpdateMovement(); // 이동 업데이트
        HandleAttacks(); // 공격 처리

        HandleBlock(); // 방어 처리
        UpdateAnimation(); // 애니메이션 업데이트
    }

    private void UpdateMovement()
    {
        float inputX = Input.GetAxis("Horizontal"); // 수평 입력 값 가져오기

        if (!isAttacking) // 공격 중이 아닐 때만 방향 전환 처리
        {
            if (inputX > 0)
            {
                FlipSprites(false); // 오른쪽 방향을 보도록 스프라이트 뒤집기
            }
            else if (inputX < 0)
            {
                FlipSprites(true); // 왼쪽 방향을 보도록 스프라이트 뒤집기
            }
        }

        rb.velocity = new Vector2(inputX * speed, rb.velocity.y); // 수평 이동 속도 설정

        isGrounded = groundSensor.State(); // 바닥에 닿은 상태인지 여부 갱신

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // 바닥에 닿았고 스페이스바를 눌렀을 때 점프
        {
            rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse); // 점프 힘을 가하기

            foreach (var animator in animators) // 모든 애니메이터에 대해 점프 트리거 설정
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    private int attackNumber = 0; // 현재 공격 번호

    private void HandleAttacks()
    {
        attackTime += Time.deltaTime; // 공격 시간 업데이트

        if (Input.GetKeyDown(KeyCode.Z) && attackTime > 0.25f && !isBlocking) // Z 키를 누르고 일정 시간이 경과했으며 방어 중이 아닐 때
        {
            attackNumber++; // 공격 번호 증가
            if (attackNumber > 3)
                attackNumber = 1;

            if (attackTime > 1.0f)
                attackNumber = 1;

            foreach (var animator in animators) // 모든 애니메이터에 대해 해당 공격 트리거 설정
            {
                animator.SetTrigger("Attack" + attackNumber);
            }

            attackTime = 0.0f; // 공격 시간 초기화

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange); // 일정 범위 내의 충돌체들 검출
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy")) // 충돌한 객체가 적인 경우
                {
                    AttackEnemy(collider.gameObject); // 적을 공격 처리
                }
            }
        }
    }


    private void HandleBlock()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isBlocking && !isAttacking) // C 키를 누르고 방어 중이 아니며 공격 중이 아닐 때
        {
            isBlocking = true; // 방어 상태로 설정

            foreach (var animator in animators) // 모든 애니메이터에 대해 방어 관련 트리거 설정
            {
                animator.SetBool("IdleBlock", true);
                animator.SetTrigger("Block");
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange * 1.5f); // 일정 범위 내의 적 검출
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy")) // 충돌한 객체가 적인 경우
                {
                    PushEnemy(collider.gameObject); // 적을 밀어내는 처리
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.C)) // C 키를 눌렀다 뗐을 때
        {
            isBlocking = false; // 방어 상태 해제

            foreach (var animator in animators) // 모든 애니메이터에 대해 방어 관련 트리거 해제
            {
                animator.SetBool("IdleBlock", false);
            }
        }





    }

    private void UpdateAnimation()
    {
        foreach (var animator in animators)
        {
            animator.SetFloat("AirSpeedY", rb.velocity.y); // 애니메이터에 수직 속도 전달
            animator.SetBool("Grounded", isGrounded); // 애니메이터에 바닥에 닿은 상태 전달
            animator.SetBool("IdleBlock", isBlocking); // 애니메이터에 방어 중인지 여부 전달

            if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon) // 수평 속도가 0보다 큰 경우
            {
                animator.SetInteger("AnimState", 1); // 애니메이션 상태를 이동 상태로 설정
                idleTime = 0.05f; // 대기 시간 초기화
            }
            else
            {
                idleTime -= Time.deltaTime; // 대기 시간 감소
                if (idleTime < 0)
                {
                    animator.SetInteger("AnimState", 0); // 애니메이션 상태를 대기 상태로 설정
                }
            }
        }
    }

    private void FlipSprites(bool flipX)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.flipX = flipX; // 스프라이트 렌더러의 X 축 뒤집기
        }
    }

    private void AttackEnemy(GameObject enemy)
    {
        SpriteRenderer[] childRenderers = enemy.GetComponentsInChildren<SpriteRenderer>(); // 적의 모든 자식 스프라이트 렌더러 가져오기
        foreach (SpriteRenderer renderer in childRenderers)
        {
            StartCoroutine(FlashAndShake(renderer)); // 플래시와 흔들림 효과 적용
        }

        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>(); // 적의 Rigidbody2D 컴포넌트 가져오기
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>(); // 적의 Collider2D 컴포넌트 가져오기

        float bounceForce = 3f; // 튕기는 힘
        Vector2 bounceDirection = enemy.transform.position - transform.position; // 튕기는 방향 계산
        bounceDirection.Normalize(); // 튕기는 방향을 정규화

        enemyRb.velocity = Vector2.zero; // 적의 속도 초기화
        enemyRb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse); // 튕기는 힘 가하기
        enemyCollider.enabled = false; // 적의 충돌 처리 비활성화

        float destroyDelay = 1f; // 제거 지연 시간
        enemy.GetComponent<EnemyController>().isGrounded = false;
        Destroy(enemy, destroyDelay); // 적 제거
    }

    private void PushEnemy(GameObject enemy)
    {
        SpriteRenderer[] childRenderers = enemy.GetComponentsInChildren<SpriteRenderer>(); // 적의 모든 자식 스프라이트 렌더러 가져오기
        foreach (SpriteRenderer renderer in childRenderers)
        {
            StartCoroutine(FlashAndShake2(renderer)); // 플래시와 흔들림 효과 적용
        }

        Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>(); // 적의 Rigidbody2D 컴포넌트 가져오기
        Collider2D enemyCollider = enemy.GetComponent<Collider2D>(); // 적의 Collider2D 컴포넌트 가져오기

        float pushForce = 5f; // 밀어내는 힘
        Vector2 pushDirection = enemy.transform.position - transform.position; // 밀어내는 방향 계산
        pushDirection.Normalize(); // 밀어내는 방향을 정규화

        enemyRb.velocity = Vector2.zero; // 적의 속도 초기화
        enemyRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse); // 밀어내는 힘 가하기

        enemy.GetComponent<EnemyController>().isGrounded = false;
        enemy.GetComponent<EnemyController>().StartCoroutine(enemy.GetComponent<EnemyController>().SetGroundedAfterDelay(1.0f));
    }

    private IEnumerator<WaitForSeconds> FlashAndShake(SpriteRenderer renderer)
    {
        float flashDuration = 0.1f; // 플래시 지속 시간
        float shakeDuration = 0.1f; // 흔들림 지속 시간
        float shakeMagnitude = 0.1f; // 흔들림 강도

        Color originalColor = renderer.color; // 원래 색상 저장
        renderer.color = Color.red; // 적의 색상을 빨강색으로 변경

        Vector3 originalPosition = renderer.transform.localPosition; // 원래 위치 저장
        float elapsedTime = 0f; // 경과 시간 초기화
        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude); // X 축 흔들림 강도 계산
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude); // Y 축 흔들림 강도 계산
            renderer.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f); // 위치 흔들기
            elapsedTime += Time.deltaTime; // 경과 시간 증가
            yield return null;
        }
        renderer.transform.localPosition = originalPosition; // 원래 위치로 복원

        yield return new WaitForSeconds(flashDuration); // 플래시 지속 시간만큼 대기

        renderer.color = originalColor; // 적의 색상 원래대로 복원
    }

    private IEnumerator<WaitForSeconds> FlashAndShake2(SpriteRenderer renderer)
    {
        float flashDuration = 0.1f; // 플래시 지속 시간
        float shakeDuration = 0.1f; // 흔들림 지속 시간
        float shakeMagnitude = 0.1f; // 흔들림 강도

        Color originalColor = renderer.color; // 원래 색상 저장
        renderer.color = Color.yellow; // 적의 색상을 노랑색으로 변경

        Vector3 originalPosition = renderer.transform.localPosition; // 원래 위치 저장
        float elapsedTime = 0f; // 경과 시간 초기화
        while (elapsedTime < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude); // X 축 흔들림 강도 계산
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude); // Y 축 흔들림 강도 계산
            renderer.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f); // 위치 흔들기
            elapsedTime += Time.deltaTime; // 경과 시간 증가
            yield return null;
        }
        renderer.transform.localPosition = originalPosition; // 원래 위치로 복원

        yield return new WaitForSeconds(flashDuration); // 플래시 지속 시간만큼 대기

        renderer.color = originalColor; // 적의 색상 원래대로 복원
    }
}
