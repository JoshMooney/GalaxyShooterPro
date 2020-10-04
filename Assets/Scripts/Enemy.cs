using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;


    void Start()
    {
        
    }

    void Update()
    {
        CalculateMovement();
        DetectOffScreen();
    }

    void DetectOffScreen()
    {
        float offScreenY = -4;
        if(transform.position.y < offScreenY)
        {
            float newXPosition = Random.Range(-8f, 8f);
            transform.position = new Vector3(newXPosition, 8, transform.position.z);
        }
    }

    void CalculateMovement()
    {
        Vector3 direction = Vector3.down;
        transform.Translate(direction * (_speed * Time.deltaTime));
    }

    // private void OnTriggerEnter(Collider other) Originally for 3D collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player?.Damage();
            Destroy(gameObject);
        } 
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
