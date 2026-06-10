using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    [HideInInspector] public float speedMultiplier = 1f;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    
    private Vector2 MoveInput;

    private bool _isBoosted;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();
    }
    
    public void OnReplay(InputValue button)
    {
        if (button.isPressed && GameManager.Instance.currentGameState == GameManager.GameState.End && WaterLayer.Instance.isReset)
        {
            GameManager.Instance.ResetGame();
        }
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
        if (GameManager.Instance.currentGameState != GameManager.GameState.End)
        {
            _rb.AddForce(MoveInput * (70f * speedMultiplier), ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Boulder":
                _animator.SetTrigger(Hit);
                GameManager.Instance.PlayerHit(1);
                break;
            case "Booster":
                StartCoroutine(SpeedBoost());
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boulder"))
            _animator.ResetTrigger(Hit);
    }

    private IEnumerator SpeedBoost()
    {
        if (_isBoosted) yield break;

        _isBoosted = true;
        
        speedMultiplier = 1.5f;
        yield return new WaitForSeconds(5f);

        _isBoosted = false;
        
        speedMultiplier = 1f;
    }
}
