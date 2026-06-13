using System.Collections;
using TMPro;
using UnityEngine;

public class CTR_DashUpgrade : MonoBehaviour
{
    public static bool IsDashActive;
    
    [SerializeField] private TextMeshPro price;
    private Transform _player;

    private void OnEnable()
    {
        _SYS_GameManager.OnLevelEnd += TextControl;
    }

    private void OnDisable()
    {
        _SYS_GameManager.OnLevelEnd -= TextControl;
    }

    private void Start()
    {
        IsDashActive = false;
        price.enabled = false;

        _player = GameObject.FindWithTag("Player").transform;
    }
    
    private void ActivateUpgrade()
    {
        if (!IsDashActive && _SYS_GameManager.Instance.gems >= _SYS_GameManager.Instance.price_Dash && CTR_Water.Instance.isReset)
        {
            IsDashActive = true;
            TextControl();
            _SYS_GameManager.Instance.DecreaseGems(_SYS_GameManager.Instance.price_Dash);
        }
    }

    private void TextControl()
    {
        price.text = _SYS_GameManager.Instance.price_Dash.ToString();

        if (IsDashActive || _SYS_GameManager.Instance.currentGameState != _SYS_GameManager.GameState.End)
        {
            price.enabled = false;
        }
        else
        {
            price.enabled = true;
        }
        
        StartCoroutine(UpdateClock());
    }

    private IEnumerator UpdateClock()
    {
        transform.position = _player.position;

        yield return new WaitForSeconds(0.01f);

        TextControl();
    }
    
    private void OnMouseDown()
    {
        ActivateUpgrade();
    }
}
