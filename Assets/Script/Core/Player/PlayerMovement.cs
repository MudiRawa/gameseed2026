using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Gravity")]
    public float gravity = -9.81f;

    private CharacterController controller;

    private Vector2 moveInput;
    private Vector3 velocity;

    private bool isGrounded;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
        controller = GetComponent<CharacterController>();

        // SIMPAN POSISI AWAL
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        GroundCheck();
        Move();
        ApplyGravity();

        if (Caught.instance.isCaught)
        {
            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {
        controller.enabled = false;

        transform.position = startPosition;
        transform.rotation = startRotation;

        controller.enabled = true;

        velocity = Vector3.zero;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    void GroundCheck()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}