using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump Settings")]
    public int maxJumps = 2;

    [Header("Parry Settings")]
    public float parryJumpForce = 8f;
    private bool hasParried = false;
    public bool isParrying = false;

    [Header("Slow Motion")]
    public float slowTimeScale = 0.2f;
    public float slowDuration = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private int jumpsLeft;
    public AudioClip jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpsLeft = maxJumps;
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (move > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0) transform.localScale = new Vector3(-1, 1, 1);


        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetFloat("YVelocity", rb.linearVelocity.y);
        animator.SetBool("isGrounded", isGrounded);

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsLeft--;

            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        if (isGrounded)
        {
            jumpsLeft = maxJumps;
            hasParried = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !hasParried)
        {
            if (!isGrounded)
            {
                isParrying = true;

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

                rb.linearVelocity = new Vector2(rb.linearVelocity.x, parryJumpForce);

                animator.SetTrigger("Attack");

                StartCoroutine(SlowMotion());

                StartCoroutine(ParryWindow());

                hasParried = true;
            }
        }
    }

    IEnumerator ParryWindow()
    {
        yield return new WaitForSeconds(0.2f);
        isParrying = false;
    }

    IEnumerator SlowMotion()
    {
        Time.timeScale = slowTimeScale;
        yield return new WaitForSecondsRealtime(slowDuration);
        Time.timeScale = 1f;
    }
}