using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 5f;
    Vector2 moveInput;

    public float CurrentMoveSpeed { get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;
                } else
                {
                    return walkSpeed;
                }
            }
            else
            {
                //idle speed is 0
                return 0;

            }

        }
    }
    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            _isMoving = value;
            if (animator != null)
            {
                animator.SetBool("isMoving", value);
            }
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            if (animator != null)
            {
                animator.SetBool("isRunning", value);
            }
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed * CurrentMoveSpeed, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
       

    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Run started");
            IsRunning = true;
        }
        else if(context.canceled)
        {
            Debug.Log("Run canceled");
            IsRunning = false;
        }
    }
}
