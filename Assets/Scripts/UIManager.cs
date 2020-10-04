using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameoverText;
    [SerializeField] private Text _gameoverInstructionsText;

    [SerializeField] private Image _livesImage;
    [SerializeField] private Sprite[] _livesSprites;

    private Coroutine _gameoverFlickerRoutine;
    
    void Start()
    {
        _gameoverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
    }

    public void UpdateScoreText(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        _livesImage.sprite = _livesSprites[lives];
    }

    public void TriggerGameOver()
    {
        _gameoverText.gameObject.SetActive(true);
        _gameoverInstructionsText.gameObject.SetActive(true);
        
        _gameoverFlickerRoutine = StartCoroutine(GameOverFlickerRoutine());
    }

    public void RestartGame()
    {
        StopCoroutine(_gameoverFlickerRoutine);
        _gameoverText.gameObject.SetActive(false);
        _gameoverInstructionsText.gameObject.SetActive(false);
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
