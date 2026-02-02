using UnityEngine;

public class Seeker : MonoBehaviour
{
    [SerializeField] private float sprintSpeed = 12f;
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintDuration = 5f;
    [SerializeField] private float sprintCooldown = 3f;
    
    private Rigidbody2D rb;
    private float sprintCooldownTimer = 0f;
    private bool isSprinting = false;
    private float sprintTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        sprintCooldownTimer -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Space) && sprintCooldownTimer <= 0f)
        {
            StartSprint();
        }
        
        if (isSprinting)
        {
            sprintTimer -= Time.deltaTime;
            if (sprintTimer <= 0f)
            {
                isSprinting = false;
            }
        }
    }
    
    private void StartSprint()
    {
        isSprinting = true;
        sprintTimer = sprintDuration;
        sprintCooldownTimer = sprintCooldown;
    }
    
    private void FixedUpdate()
    {
        Vector2 moveDirection = GetMoveDirection();
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
        rb.linearVelocity = moveDirection.normalized * currentSpeed;
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
}
