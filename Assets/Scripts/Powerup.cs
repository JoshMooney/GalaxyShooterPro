using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    
    [SerializeField] private float _speed = 2.5f;

    [SerializeField]
    private Type _type = Type.NIL;
    public enum Type
    {
        TripleShot,
        SpeedBoost,
        Shields,
        NIL
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        DetectOffScreen();
    }

    void CalculateMovement()
    {
        Vector3 direction = Vector3.down;
        transform.Translate(direction * (_speed * Time.deltaTime));
    }

    void DetectOffScreen()
    {
        float offScreenY = -4;
        if(transform.position.y < offScreenY)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.transform.GetComponent<Player>();
            switch (_type)
            {
                case Type.TripleShot:
                    player.TripleShotActive();
                    break;
                case Type.SpeedBoost:
                    player.SpeedBoostActive();
                    break;
                case Type.Shields:
                    player.ShieldActive();
                    break;
                case Type.NIL:
                    Debug.Log("NIL powerup type was collided with, somethings not right", this);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
