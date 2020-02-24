using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // handle to text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _healthText;
    [SerializeField]
    private Sprite[] _livesSpites;
    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Text _gameOverText;
    private player _player;
    [SerializeField]
    private Text _restartGame;
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _player = GameObject.Find("Player").GetComponent<player>();
        _healthText.text = "Health: " + player._health;
        _scoreText.text = "Score: " + player.score;
        _GameOverTextOff();
       
       
    }

    void Update()
    {
        
        _scoreText.text = "Score: " + player.score;
        _healthText.text = "Health: " + player._health;
        
    }
     public void GameOver()
    {
        _GameOverTextOn();
       
        _gameManager.GameOverRestart();
    }
    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _livesSpites[currentLives];
    }
    public void _GameOverTextOn()
    {
        _restartGame.gameObject.SetActive(true);
        _gameOverText.text = "GAME OVER";
       
    }
    public void _GameOverTextOff()
    {
        _gameOverText.text = "";
        _restartGame.gameObject.SetActive(false);
       
    }
 
    
}
