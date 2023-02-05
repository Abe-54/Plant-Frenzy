using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objToSpawn;
    public float minXSpawnPos;
    public float maxXSpawnPos;
    public Transform spawnPos;

    private float startDelay = 2.0f;
    public float repeatRate = 2.0f;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        
    }

    public void InvokeSpawning()
    {
        InvokeRepeating("SpawnObject", startDelay, repeatRate);
    }

    void SpawnObject()
    {
        if (!playerController.isDead)
            Instantiate(objToSpawn, new Vector3(Random.Range(minXSpawnPos, maxXSpawnPos), spawnPos.position.y, 0f), objToSpawn.transform.rotation);
    }
}