

using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;          // 이동 속도
    public LayerMask wallLayer;           // 벽 레이어
    public LayerMask groundLayer;         // 바닥 레이어
    public LayerMask playerLayer;         // 플레이어 레이어
    public float detectRange = 3f;        // 플레이어 감지 범위
    

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool movingRight = true;

    public Transform wallCheck;
    public Transform groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // 플레이어 감지
        Collider2D player = Physics2D.OverlapCircle(transform.position, detectRange, playerLayer);
        if (player != null && player.CompareTag("Player"))
        {
            // 공격 애니메이션 실행
            rb.velocity = Vector2.zero; // 이동 멈춤
            animator.SetTrigger("Attack");
            return; // 공격 중에는 이동 로직 건너뜀
        }

        // 좌우 이동
        rb.velocity = new Vector2((movingRight ? 1 : -1) * moveSpeed, rb.velocity.y);

        // 벽 감지
        RaycastHit2D wallHit = Physics2D.Raycast(wallCheck.position, movingRight ? Vector2.right : Vector2.left, 0.3f, wallLayer);
        if (wallHit.collider != null)
        {
            Flip();
        }

        // 바닥 감지
        RaycastHit2D groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.3f, groundLayer);
        if (groundHit.collider == null)
        {
            // 바닥이 없으면 떨어져라
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    // 좌우 반전
    private void Flip()
    {
        movingRight = !movingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // 센서를 그려라.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}

