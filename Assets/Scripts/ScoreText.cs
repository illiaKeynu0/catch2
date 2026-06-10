using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private static TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public static void TextUpdate(int score)
    {
        text.text = score.ToString();
    }
}
