using TMPro;
using UnityEngine;

public class TXT_Score : MonoBehaviour
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
