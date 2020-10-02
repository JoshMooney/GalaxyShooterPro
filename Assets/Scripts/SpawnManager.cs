using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _spawnEntity;
    [SerializeField] private float _secondsBetweenSpawning = 5.0f;

    void Start()
    {
        Spawn();
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(_secondsBetweenSpawning);
            Spawn();
        }
    }

    void Spawn()
    {
        Debug.Log("Spawned");
        Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
        Instantiate(_spawnEntity, spawnPosition, Quaternion.identity);
    }
}
