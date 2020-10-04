using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _isGameover;
    
    private UIManager _uiManager;
    
    void Start()
    {
        _uiManager = GameObject.Find("UIManager")?.GetComponent<UIManager>();
        if (_uiManager == null)
        {
            Debug.Log("UI Manager is null", this);
        }  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameover)
        {
            SceneManager.LoadScene("Scenes/Game");
        }
    }

    public void TriggerGameover()
    {
        _isGameover = true;
        _uiManager.TriggerGameOver();
    }

    public void RestartGame()
    {
        _isGameover = false;
        _uiManager.RestartGame();
        
    }
}
