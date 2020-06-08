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

    private GameManager gameManager;
    private float? startingPosition = null;

    public float speedMultiplier = 1f;

    private AudioManager audioManager;

    private Animator animator;

    private float playerJumpTimeToRemember = 0.2f;
    private float lastJumpPress = -1f;
    private float lastGrounded = -1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        animator = GetComponent<Animator>();

    }

    private bool isControlledByMobile = false;
    private bool isControlledByKeys = false;

    private void Update()
    {

        lastJumpPress -= Time.deltaTime;
        lastGrounded -= Time.deltaTime;

        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isMoving = true;
                isControlledByKeys = true;
            } else if (Input.touchCount > 0)
            {
                isMoving = true;
                isControlledByMobile = true;
            }

            if (isMoving && startingPosition == null)
            {
                startingPosition = transform.position.x;
            }
        }

        if (isControlledByKeys && Input.GetKeyDown(KeyCode.Space))
        {
            lastJumpPress = playerJumpTimeToRemember;
        } else if (isControlledByMobile && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            lastJumpPress = playerJumpTimeToRemember;
        }

        if (isGrounded)
        {
            lastGrounded = playerJumpTimeToRemember;
        }

        if ((lastGrounded > 0) && isMoving)
        {
            if (lastJumpPress > 0)
            {
                if (Random.value < 0.5f)
                {
                    audioManager.Play("Jump");
                }
                else
                {
                    audioManager.Play("Jump2");
                }
                rb.velocity = Vector2.up * jumpForce;
                lastJumpPress = 0;
                lastGrounded = 0;

            }


            /**if (isControlledByKeys && Input.GetKeyDown(KeyCode.Space)) // Begin jump
            {

                if (Random.value < 0.5f)
                {
                    audioManager.Play("Jump");
                }
                else
                {
                    audioManager.Play("Jump2");
                } 
                rb.velocity = Vector2.up * jumpForce;
            }
            else if (isControlledByMobile)
            {
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {

                    if (Random.value < 0.5f)
                    {
                        audioManager.Play("Jump");
                    }
                    else
                    {
                        audioManager.Play("Jump2");
                    }

                    rb.velocity = Vector2.up * jumpForce;
                }
            } */
        }

        if (rb.velocity.y < 0) // Falling, apply extra gravity
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        } else if (rb.velocity.y > 0)
        {
            if (isControlledByKeys && !Input.GetKey(KeyCode.Space))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
            } else if (isControlledByMobile && ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.touchCount == 0))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
            }
        }

    }

    public int currentDistance = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if (isMoving && canRun)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime  *  speedMultiplier, rb.velocity.y);
            if (startingPosition != null)
            {
                currentDistance = Mathf.RoundToInt(transform.position.x - startingPosition ?? 0f);
                gameManager.UpdateScore(currentDistance);
            }
        }

        animator.SetFloat("PlayerYSpeed", rb.velocity.y);
        animator.SetFloat("PlayerXSpeed", rb.velocity.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
