using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CTR_Spawner : MonoBehaviour
{
    public static CTR_Spawner Instance;
    
    private List<GameObject> _objects, _spawns;
    private List<Transform> _spawnsT;
    private bool _isActive;

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
        _objects = new List<GameObject>();
        _objects.AddRange(Resources.LoadAll<GameObject>("gameobjects"));
        
        _spawns = new List<GameObject>();
        _spawns.AddRange(GameObject.FindGameObjectsWithTag("Spawn Point"));

        _spawnsT = new List<Transform>();
        foreach (var spawn in _spawns)
        {
            _spawnsT.Add(spawn.transform);
        }
        
        Activation();
    }

    public void Activation()
    {
        if (!_isActive) StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        _isActive = true;
        
        foreach (var spawn in _spawnsT)
        {
            if (_SYS_GameManager.Instance.currentGameState == _SYS_GameManager.GameState.End)
            {
                _isActive = false;
                yield break;
            }

            var index = _SYS_GameManager.Instance.RandomIndex(_objects.Count);
            
            if (index <= -1 || (index == 0 && Random.Range(0, 10) < 9)) continue;
            
            Instantiate(_objects[index], spawn);

            yield return new WaitForSeconds(1.75f - CTR_Booster.SpeedMultiplier);
        }

        CTR_Booster.SpeedMultiplier = 1f;
        
        _isActive = false;
        Activation();
    }
    
}
