using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{

    private float speed = 1; 
    private Rigidbody2D _rb;
    private Vector2 moveInput;
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D> ();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        _rb.linearVelocity = moveInput * speed;

        
    }

    public void Move(InputAction.CallbackContext context)
    {
        _animator.SetBool("isWalking", true);
        if (context.canceled)
        {
            _animator.SetBool("isWalking", false);
            _animator.SetFloat("LastInputX", moveInput.x);
            _animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        _animator.SetFloat("InputX", moveInput.x);
        _animator.SetFloat("InputY", moveInput.y);
      
        
    }
    
}
