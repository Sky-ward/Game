using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;

    private Rigidbody2D rb;
    private Vector2 input;
    private bool isDashing;
    private float dashTime;
    private float lastDash = -Mathf.Infinity;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastDash + dashCooldown)
        {
            isDashing = true;
            dashTime = dashDuration;
            lastDash = Time.time;
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = input * dashSpeed;
            dashTime -= Time.fixedDeltaTime;
            if (dashTime <= 0f)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.velocity = input * moveSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
    }

    public Vector2 GetVelocityForInput(Vector2 direction, bool dashing)
    {
        if (dashing) return direction.normalized * dashSpeed;
        return direction.normalized * moveSpeed;
    }
}
