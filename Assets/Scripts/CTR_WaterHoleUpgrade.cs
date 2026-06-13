using TMPro;
using UnityEngine;

public class CTR_WaterHoleUpgrade : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    [SerializeField] private TextMeshProUGUI text;

    private int _hp = 2;
    private bool _wasUsed;

    private void OnEnable()
    {
        _SYS_GameManager.OnPriceChanged += TextControl;
        _SYS_GameManager.OnLevelReset += ReActivation;
        _SYS_GameManager.OnLevelEnd += TextControl;
    }

    private void OnDisable()
    {
        _SYS_GameManager.OnPriceChanged -= TextControl;
        _SYS_GameManager.OnLevelReset -= ReActivation;
        _SYS_GameManager.OnLevelEnd -= TextControl;
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
        
        TextControl();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_wasUsed && _SYS_GameManager.Instance.currentGameState == _SYS_GameManager.GameState.Start)
        {
            _wasUsed = true;
            StartCoroutine(CTR_Water.Instance.WaterLevelDelay());
        }
    }

    public void Clicked()
    {
        if(_hp <= 0 ) return;
        
        if (_SYS_GameManager.Instance.gems >= _SYS_GameManager.Instance.price_WaterHole && CTR_Water.Instance.isReset)
        {
            _SYS_GameManager.Instance.DecreaseGems(_SYS_GameManager.Instance.price_WaterHole);
            _SYS_GameManager.Instance.ChangePrice(0);
            _hp--;
            
            TextControl();
            
            if (_hp <= 0 ) ActivateSelf();
        }
    }

    private void TextControl()
    {
        text.text = _SYS_GameManager.Instance.price_WaterHole.ToString();

        if (_hp > 0 && _SYS_GameManager.Instance.currentGameState == _SYS_GameManager.GameState.End)
        {
            text.enabled = true;
        }
        else
        {
            text.enabled = false;
        }
    }

    private void ActivateSelf()
    {
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
    }

    private void ReActivation()
    {
        _wasUsed = false;
    }
}
