using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    public static float SpeedMultiplier = 1f;
    
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

    public static IEnumerator SpeedBoost()
    {
        SpeedMultiplier = 1.5f;
        yield return new WaitForSeconds(5f);
        SpeedMultiplier = 1f;
    }

    public void Sink()
    {
        _rb.gravityScale = 0.5f;
        gameObject.layer = _layer;
    }
}
