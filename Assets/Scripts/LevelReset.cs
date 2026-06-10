using UnityEngine;

public class LevelReset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) GameManager.Instance.PlayerHit(2);
    }
}
