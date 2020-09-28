using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private bool _isDebugMode = false;
    [SerializeField] private float _speed = 8.0f;
    [SerializeField] private float _despawnDistance = 10.0f;

    private Vector3 _direction;
    private Vector3 _startingPosition;


    void Start()
    {
        _direction = Vector3.up;
        _startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckForDespawn();
    }

    private void CheckForDespawn()
    {
        if (transform.position.x > _startingPosition.x + _despawnDistance || 
            transform.position.x < _startingPosition.x - _despawnDistance || 
            transform.position.y > _startingPosition.y + _despawnDistance ||
            transform.position.y < _startingPosition.y - _despawnDistance)
        {
            LogMessage("Despawned");
            Destroy(gameObject);
        }
    }

    private void LogMessage(string msg)
    {
        if(_isDebugMode)
        {
            Debug.Log(msg, gameObject);
        }
    }

    private void Move()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
