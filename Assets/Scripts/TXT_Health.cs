using TMPro;
using UnityEngine;

public class TXT_Health : MonoBehaviour
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
