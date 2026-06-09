using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private List<GameObject> objects, spawns;
    private List<Transform> spawnsT;
    private bool isActive;
    
    private void Start()
    {
        objects = new List<GameObject>();
        objects.AddRange(Resources.LoadAll<GameObject>("gameobjects"));
        
        spawns = new List<GameObject>();
        spawns.AddRange(GameObject.FindGameObjectsWithTag("Spawn Point"));

        spawnsT = new List<Transform>();
        foreach (var spawn in spawns)
        {
            spawnsT.Add(spawn.transform);
        }

        isActive = true;
    }

    private void Update()
    {
        if (isActive && GameManager.Instance.currentGameState != GameManager.GameState.End)
        {
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        isActive = false;
        
        foreach (var spawn in spawnsT)
        {
            var index = GameManager.Instance.RandomIndex(objects.Count);
            
            if (index <= -1 || (index == 0 && Random.Range(0, 10) < 9)) continue;
            
            Instantiate(objects[index], spawn);

            yield return new WaitForSeconds(.75f);
        }
        
        isActive = true;
    }
    
}
