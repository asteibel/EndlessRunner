using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool isMoving = false;

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;

    public bool isJumping = false;
    public bool isGrounded = false;

    public bool canRun = true;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true;
            Debug.Log("key down, is grounded: " + isGrounded);
            if (isGrounded)
            {
                rb.AddForce(new Vector2(0f, jumpForce));
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if (isMoving && canRun)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
     }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
