using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _runSpeed = 5f;
    [SerializeField] float _jumpForce = 8f;
    [SerializeField] float _climbSpeed = 3f;

    Vector2 _moveInput;
    Rigidbody2D _rigidbody;
    Animator _playerAnimator;
    CapsuleCollider2D _bodyCollider;
    BoxCollider2D _feetCollider;
    float _startingGravity;

    bool _playerHasHorizontalMoveSpeed = false;
    bool _playerIsClimbing = false;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
        _bodyCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _startingGravity = _rigidbody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) return;

        if (value.isPressed)
        {
            _rigidbody.velocity += new Vector2(0f, _jumpForce);
        }
    }

    void ClimbLadder()
    {
        if (!_feetCollider.IsTouchingLayers(LayerMask.GetMask("Climable")))
        {
            _rigidbody.gravityScale = _startingGravity;
            return;
        }

        _rigidbody.gravityScale = 0;
        Vector2 climbVelocity = new Vector2(_rigidbody.velocity.x, _moveInput.y * _climbSpeed);
        _playerIsClimbing = Mathf.Abs(climbVelocity.y) > Mathf.Epsilon;
        _rigidbody.velocity = climbVelocity;

        _playerAnimator.SetBool("isClimbing", _playerIsClimbing);
    }
    
    void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * _runSpeed, _rigidbody.velocity.y);
        _playerHasHorizontalMoveSpeed = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        _rigidbody.velocity = playerVelocity;

        _playerAnimator.SetBool("isRunning", _playerHasHorizontalMoveSpeed);
    }

    void FlipSprite()
    {
        _playerHasHorizontalMoveSpeed = Mathf.Abs(_rigidbody.velocity.x) > Mathf.Epsilon;
        if (_playerHasHorizontalMoveSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(_rigidbody.velocity.x), 1f);
        }
        
    }

}
