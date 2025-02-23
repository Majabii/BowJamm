using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnController : MonoBehaviour
{

    public GameObject[] zombies;
    public int zombieIndex;
    public Transform[] beginSpawnLocations = new Transform[4];
    public Transform[] endSpawnLocations = new Transform[4];
    public int startDelay = 3;
    public int spawnInterval = 5;
    
    private struct zombieSpawnLocation
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie", startDelay, spawnInterval);
    }

    private void SpawnZombie()
    {
        Instantiate(zombies[zombieIndex - 1], GetSpawnLocation(), zombies[zombieIndex - 1].transform.rotation);
    }
    private Vector3 GetSpawnLocation()
    {
        int index = Random.Range(0, 3);
        Vector3 result = new Vector3();
        result.x = Random.Range(beginSpawnLocations[index].position.x, endSpawnLocations[index].position.x);
        result.y = 0;
        result.z = Random.Range(beginSpawnLocations[index].position.z, endSpawnLocations[index].position.z);
        return result;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
