using UnityEngine;
using Random = UnityEngine.Random;

public class CTR_Boulder : MonoBehaviour
{
    private Rigidbody2D _rb;
    private LayerMask _layer;
    
    private void Awake()
    {
        _layer = LayerMask.NameToLayer("Erase");
    }
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        _rb.AddTorque(Random.Range(-0.5f, 0.5f), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.layer = _layer;
        }
        if (other.gameObject.CompareTag("Water"))
        {
            _SYS_AudioManager.Instance.PlaySound(_SYS_AudioManager.SoundType.BoulderSplash);
            gameObject.layer = _layer;
            _rb.gravityScale = 0.5f;
        }
    }
}
