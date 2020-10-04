using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject _spawnEnemy;
    [SerializeField] private GameObject _spawnEnemyContainer;
    [SerializeField] private float _secondsBetweenSpawning = 5.0f;
    
    [Header("Powerups")]
    [SerializeField] private GameObject[] _spawnPowerups;
    [SerializeField] private GameObject _spawnPowerupContainer;
    

    private bool _stopSpawning = false;
    void Start()
    {
        // Spawn Enemy at the start of game
        Spawn(_spawnEnemy, _spawnEnemyContainer);
        
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning==false)
        {
            yield return new WaitForSeconds(_secondsBetweenSpawning);
            Spawn(_spawnEnemy, _spawnEnemyContainer);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_stopSpawning==false)
        {
            float randomSpawnTime = Random.Range(3.0f, 7.0f);
            yield return new WaitForSeconds(randomSpawnTime);
            int spawnIndex = Random.Range(0, _spawnPowerups.Length);
            GameObject randomPowerup = _spawnPowerups[spawnIndex];
            Spawn(randomPowerup, _spawnPowerupContainer);
        }
    }

    void Spawn(GameObject spawnEntity, GameObject spawnContainer)
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
        GameObject entity = Instantiate(spawnEntity, spawnPosition, Quaternion.identity);
        entity.transform.parent = spawnContainer.transform;
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
