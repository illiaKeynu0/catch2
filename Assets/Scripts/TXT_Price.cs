using TMPro;
using UnityEngine;

public class TXT_Price : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    
    public void TextUpdate(int gems)
    {
        text.text = gems.ToString();
    }
}
