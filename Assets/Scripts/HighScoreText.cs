using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    private static TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.enabled = false;
    }

    public static void TextUpdate(int highScore)
    {
        text.text = highScore.ToString();
        text.enabled = true;
    }

    public static void HideScore()
    {
        text.enabled = false;
    }
}
