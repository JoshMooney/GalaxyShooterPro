using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3.0f;
    [SerializeField] private GameObject _explosionEffect;

    private SpawnManager _spawnManager;

    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager")?.GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("Asteroid was not able to get an instance of SpawnManager", this);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);
            Instantiate(_explosionEffect, gameObject.transform.position, Quaternion.identity);
            _spawnManager.StartSpawning();
            
            Destroy(gameObject, 0.25f);
        }
    }
}
