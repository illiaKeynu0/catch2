using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerController : MonoBehaviour
{
    private List<GameObject> objects, spawns;
    private List<Transform> spawnsT;
    private bool isActive;
    
    private void Start()
    {
        objects = new List<GameObject>();
        objects.Add(Resources.Load<GameObject>("Gem"));
        objects.Add(Resources.Load<GameObject>("Boulder"));
        
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
        if (isActive)
        {
            StartCoroutine(Spawner());
        }
    }

    private IEnumerator Spawner()
    {
        var randomObject = Random.Range(0, objects.Count);
        var randomSpawn = Random.Range(0, spawnsT.Count);
        
        Instantiate(objects[randomObject], spawnsT[randomSpawn]);
        isActive = false;
        yield return new WaitForSeconds(1f);
        isActive = true;
    }
    
}
