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

        _spriteRenderer.sprite = GameEvents.RandomSprite();
        
        _rb.AddTorque(Random.Range(-0.5f, 0.5f), ForceMode2D.Impulse);
    }

    public void Sink()
    {
        _rb.gravityScale = 0.5f;
        gameObject.layer = _layer;
    }
    
}
