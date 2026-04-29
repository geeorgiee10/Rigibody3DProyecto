using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Player Movement")]
        public float speed = 5f;
        public float jumpForce = 6f;

    private Rigidbody rb;
    private Vector2 moveInput;
    private bool isGrounded = false;
    [SerializeField] private float runMultiplier = 2f;
    [SerializeField] private bool isRunning;
    

    //[Header("Player Animations")]
    //[SerializeField] private PlayerAnimations PlayerAnimations;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody>(); 
    }

    void Update()
    {
        isRunning = Keyboard.current != null &&
                    (Keyboard.current.leftCtrlKey.isPressed || 
                    Keyboard.current.rightCtrlKey.isPressed);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private bool IsGrounded() => Physics.Raycast(transform.position, -transform.up, 1.1f);

    public void OnJump()
    {
        if (IsGrounded())
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            //animator.SetTrigger("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.TransformDirection(
            new Vector3(moveInput.x, 0, moveInput.y));


        float currentSpeed = isRunning ? speed * runMultiplier : speed;
        Vector3 velocity = direction * currentSpeed;
        Vector3 newVelocity = new Vector3(velocity.x, rb.linearVelocity.y, velocity.z);
        rb.linearVelocity = newVelocity;
    }
}
