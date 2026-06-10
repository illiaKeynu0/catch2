using System.Collections;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public static WaterLayer Instance;
    
    public bool isReset;
    
    private Vector2 _originalPosition;
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
        if (!_isMoving) StartCoroutine(ChangeY());
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
        
        if (GameManager.Instance.currentGameState == GameManager.GameState.End)
        {
            _isMoving = false;
            yield break;
        }
        
        var T = transform.position;
        T.y += .01f;
        transform.position = T;
        
        yield return new WaitForSeconds(.2f);
        
        _isMoving = false;
        Activation();
    }
}
