using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _defaultSpeed = 5.0f;
    [SerializeField] private float _speedBoostMultipler = 2.0f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldVisualisation;
    [SerializeField] private int _lives = 3;

    private float _speed;
    private float _canFire = -1;
    private SpawnManager _spawnManager;
    
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive = false;
    private bool _isShieldActive = false;

    void Start()
    {
        _shieldVisualisation.SetActive(false);
        _speed = _defaultSpeed;
        transform.position = new Vector3(0, 0, 0);

        _spawnManager = GameObject.Find("SpawnManager")?.GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.Log("Spawn manager is null", this);
        }
        
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
        
        Vector3 spawnPoint;
        GameObject bulletPrefab;
        if (_isTripleShotActive)
        {
            bulletPrefab = _tripleShotPrefab;
            spawnPoint = transform.position;
        }
        else
        {
            bulletPrefab = _laserPrefab;
            spawnPoint = new Vector3(0, 1.05f, 0) + transform.position;
        }
        Instantiate(bulletPrefab, spawnPoint, Quaternion.identity);
    }

    void CalculateMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        /*        transform.Translate(Vector3.right * hor * _speed * Time.deltaTime);
                transform.Translate(Vector3.up * vert * _speed * Time.deltaTime);*/

        Vector3 direction = new Vector3(hor, vert, 0);
        transform.Translate(direction * (_speed * Time.deltaTime));
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            DeactivateShields();
            return;
        }
        
        _lives -= 1;
        if(_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    
    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed = _defaultSpeed * _speedBoostMultipler;
        
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }
    
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(7.0f);
        _isSpeedBoostActive = false;
        _speed = _defaultSpeed;
    }
    
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualisation.SetActive(true);
    }

    private void DeactivateShields()
    {
        _isShieldActive = false;
        _shieldVisualisation.SetActive(false);
    }
}
