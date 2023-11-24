using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _runSpeed = 5f;

    Vector2 _moveInput;
    Rigidbody2D _rigidbody;
    Animator _playerAnimator;

    bool _playerHasHorizontalMoveSpeed = false;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
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
