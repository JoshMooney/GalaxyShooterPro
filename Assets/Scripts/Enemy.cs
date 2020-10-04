using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;

    private bool _isDead;
    private Player _player;
    private int _scoreValue = 10;
    private Animator _animator;

    private AudioSource _explosionSfxSource;

    void Start()
    {
        _player = GameObject.Find("Player")?.GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Enemy was not able to get a reference of player", this);
        }

        _animator = GetComponent<Animator>();
        if (_animator == null)
        {
            Debug.Log("Enemy was not able to get a reference of its Animator", this);
        }

        _explosionSfxSource = GetComponent<AudioSource>();
        if (_explosionSfxSource == null)
        {
            Debug.Log("Enemy was not able to get a reference of its AudioSource", this);
        }
    }

    void Update()
    {
        CalculateMovement();
        DetectOffScreen();
    }

    void DetectOffScreen()
    {
        float offScreenY = -4;
        if(transform.position.y < offScreenY && !_isDead)
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
            //Player player = other.transform.GetComponent<Player>();
            _player?.Damage();
            Die();
        } 
        else if (other.tag == "Laser")
        {
            _player?.AddScore(_scoreValue);
            Destroy(other.gameObject);
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        _speed = 0;
        _animator.SetTrigger("OnEnemyDeath");
        _explosionSfxSource.Play();
        
        Destroy(gameObject, 2.8f);
    }
}
