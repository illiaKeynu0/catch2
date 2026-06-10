using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    private static TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public static void TextUpdate(int health)
    {
        text.text = Mathf.Max(0, health).ToString();
    }
}
