using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private int _lives = 3;

    private float _canFire = -1;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);   
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        PollInput();
    }

    void PollInput()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Vector3 spawnPoint = new Vector3(0, 0.8f, 0) + transform.position;
        Instantiate(_laserPrefab, spawnPoint, Quaternion.identity);
    }

    void CalculateMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        /*        transform.Translate(Vector3.right * hor * _speed * Time.deltaTime);
                transform.Translate(Vector3.up * vert * _speed * Time.deltaTime);*/

        Vector3 direction = new Vector3(hor, vert, 0);
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    public void Damage()
    {
        _lives -= 1;
        if(_lives <= 0)
        {
            Destroy(gameObject);
        }
    }
}
