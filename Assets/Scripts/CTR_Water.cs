using System.Collections;
using UnityEngine;

public class CTR_Water : MonoBehaviour
{
    public static CTR_Water Instance;
    
    public bool isReset;

    private Coroutine _currentCoroutine;
    private Vector2 _originalPosition;
    private float _waterSlow;
    private bool _isMoving;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _originalPosition = transform.position;
        
        Activation();
    }

    public void Activation()
    {
        if (!_isMoving) _currentCoroutine = StartCoroutine(ChangeY());
    }

    public IEnumerator ResetPosition()
    {
        while (Vector2.Distance(transform.position, _originalPosition) > 0.001)
        {
            transform.position = Vector2.MoveTowards(transform.position, _originalPosition, 2f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        isReset = true;
    }

    private IEnumerator ChangeY()
    {
        isReset = false;
        _isMoving = true;
        
        if (_SYS_GameManager.Instance.currentGameState == _SYS_GameManager.GameState.End)
        {
            _isMoving = false;
            yield break;
        }
        
        var T = transform.position;
        T.y += 0.1f;
        transform.position = T;
        
        yield return new WaitForSeconds(.2f);
        
        _isMoving = false;
        Activation();
    }

    public IEnumerator WaterLevelDelay()
    {
        if (_currentCoroutine == null) yield break;
        
        StopCoroutine(_currentCoroutine);

        yield return new WaitForSeconds(5f);
        
        _isMoving = false;
        Activation();
    }
}
