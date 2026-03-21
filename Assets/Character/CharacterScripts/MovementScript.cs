using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private Rigidbody2D _rb;
    private Vector2 moveInput;
    private Animator _animator;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        _rb.linearVelocity = moveInput * speed;
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        _animator.SetFloat("InputX", moveInput.x);
        _animator.SetFloat("InputY", moveInput.y);

        if (context.performed)
            _animator.SetBool("isWalking", true);
        else if (context.canceled)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetFloat("LastInputX", moveInput.x);
            _animator.SetFloat("LastInputY", moveInput.y);
        }
    }
}