using System.Collections;
using UnityEngine;

public class EndLayerController : MonoBehaviour
{
    private static Transform _transform;
    private static bool isActive;

    private void Start()
    {
        _transform = GetComponent<Transform>();
        
        isActive = true;
    }

    private void Update()
    {
        if (isActive)
        {
            StartCoroutine(ChangeY());
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
        }
    }

    private static IEnumerator ChangeY()
    {
        isActive = false;
        
        var T = _transform.position;
        T.y += .1f;
        _transform.position = T;
        
        yield return new WaitForSeconds(1f);
        
        isActive = true;
    }
}
