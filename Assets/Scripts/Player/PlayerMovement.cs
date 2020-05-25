using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool isMoving = false;

    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;

    public bool isJumping = false;
    public bool isGrounded = false;

    public bool canRun = true;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public DistanceText distanceText;
    private float? startingPosition = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
        {
            isMoving = true;

            if (startingPosition == null)
            {
                startingPosition = transform.position.x;
            }

            if (isGrounded) // Begin jump
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }

        if (rb.velocity.y < 0) // Falling, apply extra gravity
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space)) // Jumping,  and not holding the space key
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if (isMoving && canRun)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            if (startingPosition != null)
            {
                distanceText.updateText(transform.position.x - startingPosition ?? 0f);
            }
        }
     }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
