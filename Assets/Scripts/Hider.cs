using UnityEngine;

public class Hider : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float dashCooldown = 1.5f;

    private Rigidbody2D rb;
    private float dashCooldownTimer = 0f;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private Vector2 dashDirection = Vector2.zero;
    private int Wall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Wall = LayerMask.NameToLayer("Wall");
    }

    void Update()
    {
        dashCooldownTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldownTimer <= 0f && !isDashing)
        {
            Dash();
        }
        
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
    }
    
    private void Dash()
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;
        dashDirection = GetMoveDirection();
        if (dashDirection == Vector2.zero)
        {
            dashDirection = transform.right;
        }
        

        Physics2D.IgnoreLayerCollision(gameObject.layer, Wall, true);
        Invoke(nameof(ReenableWallCollision), dashDuration);
    }
    
    private void ReenableWallCollision()
    {
        Physics2D.IgnoreLayerCollision(gameObject.layer, Wall, false);
    }
    
    private void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDirection.normalized * dashSpeed;
        }
        else
        {
            rb.linearVelocity *= rb.linearDamping;
        }
    }
    
    private Vector2 GetMoveDirection()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) direction.y += 1;
        if (Input.GetKey(KeyCode.S)) direction.y -= 1;
        if (Input.GetKey(KeyCode.A)) direction.x -= 1;
        if (Input.GetKey(KeyCode.D)) direction.x += 1;
        return direction.normalized;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Game")
        {
            if (collision.CompareTag("Seeker"))
            {
                GameManager.Instance.HiderCaught();
                Destroy(gameObject);
            }
        }
    }
}
