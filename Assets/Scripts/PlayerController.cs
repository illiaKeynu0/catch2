using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    public bool ReplayPressed;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private Vector2 MoveInput;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
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
        if (button.isPressed && GameManager.Instance.currentGameState == GameManager.GameState.End && WaterLayer.OnStart)
        {
            ReplayPressed = true;
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
            _rb.AddForce(MoveInput * (70f * Booster.SpeedMultiplier), ForceMode2D.Force);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Gem":
                Destroy(other.gameObject);
                GameManager.Instance.AddScore();
                break;
            case "Boulder":
                _animator.SetTrigger(Hit);
                GameManager.Instance.Hit();
                break;
            case "Booster":
                StartCoroutine(Booster.SpeedBoost());
                Destroy(other.gameObject);
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boulder"))
            _animator.ResetTrigger(Hit);
    }
}
