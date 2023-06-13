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
    private int attackNumber = 0;
    private float attackTime = 0.0f;
    private float idleTime = 0.0f;

    public List<SpriteRenderer> spriteRenderers;

    private bool isBlocking = false;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();

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

        if (!IsAttacking()) // 공격 중이 아닐 때만 방향 전환 처리
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
        }
    }

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
                //LittleEnemy enemy = collider.GetComponent<LittleEnemy>();
                //if (enemy != null && enemy.CanBeParried())
                //{
                //    enemy.Parry();
                //}
            }
        }
    }

    private void HandleBlock()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isBlocking)
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
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);

            foreach (var animator in animators)
            {
                animator.SetTrigger("Jump");
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


    private bool IsAttacking()
    {
        foreach (var animator in animators)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsTag("Attack") && currentState.normalizedTime < 1f)
            {
                return true;
            }
        }
        return false;
    }
}
