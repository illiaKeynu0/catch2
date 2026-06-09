using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int Hit = Animator.StringToHash("Hit");
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Vector2 MoveInput;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        if (MoveInput.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        _rb.AddForce(MoveInput * 70f, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Gem":
                Destroy(other.gameObject);
                GameEvents.AddScore();
                break;
            case "Boulder":
                _animator.SetTrigger(Hit);
                GameEvents.Hit();
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _animator.ResetTrigger("Hit");
    }
}
