using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    Rigidbody2D _rigidbody;
    CircleCollider2D _bodyCollider;
    BoxCollider2D _reversePeriscopeCollider;
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bodyCollider = GetComponent<CircleCollider2D>();
        _reversePeriscopeCollider = GetComponent<BoxCollider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed, 0);

    }

    void OnTriggerExit2D(Collider2D collider)
    {
        _moveSpeed = -_moveSpeed;
        FlipEnemyDirection();
    }

    private void FlipEnemyDirection()
    {
        var direction = new Vector2(-(Mathf.Sign(_rigidbody.velocity.x)), 1f);
        _spriteRenderer.flipX = (direction.x < 0) ? true : false;
    }
}
