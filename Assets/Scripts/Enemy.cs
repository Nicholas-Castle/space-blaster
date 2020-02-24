using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField]
    private GameObject[] powerups;

   [SerializeField]
    private float _speed = 4f;
    private float _randomXPos = 9; 
    private int _enemyHealth = 100;
    
    private Animator _enemy_Animator;
    private player _player;
    private AudioSource _audioSource;

    void Start() 
    {
         
        _player = GameObject.Find("Player").GetComponent<player>();
        _audioSource = GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("The player is null!");
        }
        _enemy_Animator = gameObject.GetComponent<Animator>();

        if(_enemy_Animator == null)
        {
            Debug.LogError("Animator is null!");
        }
    }
    
  
    void Update()
    {
        transform.Translate(Time.deltaTime * _speed * Vector3.down);
        if (gameObject.transform.position.y < -6)
        {
            float randomX = Random.Range(-_randomXPos, _randomXPos);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            player playerComponet = other.transform.GetComponent<player>();
            if (playerComponet != null)
            {
                playerComponet.Damage();

            }
            // trigger animation
            _enemy_Animator.SetTrigger("OnEnemyDeath");
            _speed = 2f;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 0.5f);
            
        }
      
        if (other.tag == "Bullet")
        {   
            var _randomNum = Random.Range(1, 100);
            float _posX = transform.position.x;
            Vector3 spwanPos = new Vector3(Random.Range(-8, 8), 10, 0);
            player.score += 10;
            
            GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");

            Destroy(bullet);

            // trigger animation
            _enemy_Animator.SetTrigger("OnEnemyDeath");
            _speed = 2f;
            _audioSource.Play();
            Destroy(this.gameObject, 2.5f);

            if (_randomNum > 95)
            {
                GameObject newPowerUp = Instantiate(powerups[0], spwanPos, Quaternion.identity);
            }
            else if (_randomNum > 70 && _randomNum < 80)
            {
                GameObject speedPowerup = Instantiate(powerups[1], spwanPos, Quaternion.identity);
            }
            else if (_randomNum < 5)
            {
                GameObject shieldPowerup = Instantiate(powerups[2], spwanPos, Quaternion.identity);
            }
            
        }
    }
}
