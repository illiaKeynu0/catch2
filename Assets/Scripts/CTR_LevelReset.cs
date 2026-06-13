using UnityEngine;

public class CTR_LevelReset : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) _SYS_GameManager.Instance.PlayerHit(2);
    }
}
