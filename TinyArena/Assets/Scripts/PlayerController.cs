//using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontalInput;
    float verticalInput;

    Rigidbody rb;
    public Camera playerCamera;
    public float moveSpeed = 5f;

    // ADDED
    public float sprintMultiplier = 1.5f; // Sprint speed multiplier
    public KeyCode sprintKey = KeyCode.LeftShift; // Sprint key
    private bool isSprinting = false; // Track sprint state

    public int playerUI = 0;

    private Vector3 moveDirection;
    private Vector3 normalizedMoveDirection;

    //playerDrag
    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;
    public float groundDrag;

    //jumping
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool isReadyToJump = true;
    public KeyCode jumpKey = KeyCode.Space;

    //shooting
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float fireRate = 0.5f; // 0.5 sec = 1 schot per halve seconde
    private float nextFireTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        SpeedControl();
        Shoot();
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);

        // Input ophalen
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Sprint input // ADDED
        isSprinting = Input.GetKey(sprintKey);

        // Camera forward en right vector
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Y-component nul maken
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Beweging berekenen
        moveDirection = forward * verticalInput + right * horizontalInput;
        normalizedMoveDirection = moveDirection.normalized;

        //when to jump
        if (Input.GetKeyDown(jumpKey) && isReadyToJump && isGrounded)
        {
            isReadyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        //handle drag
        if (isGrounded)
        {
            rb.linearDamping = groundDrag;
        }
        else
        {
            rb.linearDamping = 0;
        }

        if (Input.GetKeyDown(KeyCode.W) && playerUI == 0)
        {
            playerUI++;
        }
    }

    void FixedUpdate()
    {
        // ADDED: adjust movement speed based on sprint state
        float currentSpeed;

        if (isSprinting)
        {
            currentSpeed = moveSpeed * sprintMultiplier;
            if (playerUI == 1)
            {
                playerUI++;
            }
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        rb.MovePosition(rb.position + normalizedMoveDirection * currentSpeed * Time.fixedDeltaTime);

        
    }

    
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        if (isGrounded && rb.linearVelocity.y > 0f)
        {
            flatVel = rb.linearVelocity;
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.linearVelocity += Vector3.up * jumpForce;
        if (playerUI == 2)
            playerUI++;
    }

    void ResetJump()
    {
        isReadyToJump = true;
    }

    void Shoot()
    {
        if (Input.GetMouseButton(0))
        {

            if (Time.time < nextFireTime)
                return;

            // Spawn bullet
            Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            // Reset cooldown
            nextFireTime = Time.time + fireRate;
        }
    }

}
