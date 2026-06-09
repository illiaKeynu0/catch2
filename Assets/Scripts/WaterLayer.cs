using System.Collections;
using UnityEngine;

public class WaterLayer : MonoBehaviour
{
    public static bool OnStart;
    
    private bool _isActive;
    private Vector2 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
        
        _isActive = true;
    }

    private void Update()
    {
        if (_isActive && GameManager.Instance.currentGameState == GameManager.GameState.Start)
        {
            StartCoroutine(ChangeY());
            OnStart = false;
        }
        else if (GameManager.Instance.currentGameState == GameManager.GameState.End)
        {
            transform.position = Vector2.MoveTowards(transform.position, _originalPosition, 2f * Time.deltaTime);
            if (transform.position.y <= -8.5f)
            {
                OnStart = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Gem":
                other.gameObject.GetComponent<GemController>().Sink();
                break;
            case "Boulder":
                other.gameObject.GetComponent<BoulderController>().Sink();
                break;
            case "Booster":
                other.gameObject.GetComponent<Booster>().Sink();
                break;
        }
    }

    private IEnumerator ChangeY()
    {
        _isActive = false;
        
        var T = transform.position;
        T.y += .1f;
        transform.position = T;
        
        yield return new WaitForSeconds(1f);
        
        _isActive = true;
    }
}
