using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.enabled = false;
    }

    private void Update()
    {
        _text.text = GameManager.Instance.highScore.ToString();
        
        if (GameManager.Instance.currentGameState == GameManager.GameState.End)
        {
            _text.enabled = true;
        }
        else
        {
            _text.enabled = false;
        }
    }
}
