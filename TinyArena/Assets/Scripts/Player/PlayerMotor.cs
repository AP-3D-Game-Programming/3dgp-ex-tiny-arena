using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Movement")]
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;
    public Vector3 moveDirection;

    [Header("Footsteps")]
    public AudioClip step1;
    public AudioClip step2;
    public float stepDelay = 0.5f;

    private bool isStepping = false;
    private bool playFirstStep = true;
    private bool isSprinting = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        HandleFootsteps();
    }

    public void ProcessMove(Vector2 input)
    {
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        controller.Move(speed * Time.deltaTime * transform.TransformDirection(moveDirection));

        playerVelocity.y += gravity * Time.deltaTime;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Sprint()
    {
        speed = 10f;
        isSprinting = true;
    }

    public void Walk()
    {
        speed = 5f;
        isSprinting = false;
    }

    void HandleFootsteps()
    {
        bool isMoving = moveDirection.magnitude > 0.5f;

        if (isMoving && !isStepping && isGrounded)
        {
            StartCoroutine(PlayStep());
        }
    }

    IEnumerator PlayStep()
    {
        isStepping = true;

        AudioClip clipToPlay = playFirstStep ? step1 : step2;
        AudioManager.Instance.PlayFootstep(clipToPlay);

        playFirstStep = !playFirstStep;

        float currentStepDelay = isSprinting ? stepDelay * 0.7f : stepDelay;
        yield return new WaitForSeconds(currentStepDelay);

        isStepping = false;
    }
}
