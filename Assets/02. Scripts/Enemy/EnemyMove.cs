
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public LayerMask wallLayer;
    public LayerMask groundLayer;

    
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool movingRight = true;

    public Transform wallCheck;
    public Transform groundCheck;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
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
}
