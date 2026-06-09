using UnityEngine;

public class ObjectClear : MonoBehaviour
{
    private void OnCollisionEnter2D (Collision2D other)
    {
        Destroy(other.gameObject);
    }
}
