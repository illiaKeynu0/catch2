using TMPro;
using UnityEngine;

public class TXT_Gem : MonoBehaviour
{
    private static TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public static void TextUpdate(int gems)
    {
        text.text = gems.ToString();
    }
}
