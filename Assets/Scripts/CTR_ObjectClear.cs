using UnityEngine;

public class CTR_ObjectClear : MonoBehaviour
{
    private void OnCollisionEnter2D (Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
