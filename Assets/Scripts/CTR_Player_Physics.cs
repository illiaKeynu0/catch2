using UnityEngine;

public class CTR_Player_Physics : MonoBehaviour
{
    public static CTR_Player_Physics Instance;
    
    private static readonly int Hit = Animator.StringToHash("Hit");
    private Rigidbody2D _rb;

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

        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void FixedUpdate()
    {
        if (_SYS_GameManager.Instance.currentGameState != _SYS_GameManager.GameState.End)
        {
            _rb.AddForce(_SYS_Player_Input.Instance.MoveInput * 55f, ForceMode2D.Force);
        }
    }

    public void Dash()
    {
        _rb.AddForce(_SYS_Player_Input.Instance.MoveInput * 7f, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boulder"))
        {
            CTR_Player_Avatar.Instance.animator.SetTrigger(Hit);
            _SYS_GameManager.Instance.PlayerHit(1);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boulder"))
            CTR_Player_Avatar.Instance.animator.ResetTrigger(Hit);
    }
}
