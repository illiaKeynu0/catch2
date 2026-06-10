using UnityEngine;
using Random = UnityEngine.Random;

public class GemController : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private LayerMask _layer;

    private void Awake()
    {
        _layer = LayerMask.NameToLayer("Erase");
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();

        _spriteRenderer.sprite = GameManager.Instance.RandomSprite();
        
        _rb.AddTorque(Random.Range(-0.5f, 0.5f), ForceMode2D.Impulse);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(AudioManager.SoundType.GemCollect);
            GameManager.Instance.AddScore(1);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Water"))
        {
            AudioManager.Instance.PlaySound(AudioManager.SoundType.GemSplash);
            gameObject.layer = _layer;
            _rb.gravityScale = 0.5f;
        }
    }
}
