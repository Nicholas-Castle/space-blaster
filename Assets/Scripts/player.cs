using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.5f;

    [SerializeField]
    private GameObject BulletPrefab, TripleShotPrefab, DeathBeamPrefab;

    [SerializeField]
    private float fireRate = 0.2f;
    private float delayedShot = 0.0f;
    [SerializeField]
    public static int _health = 100;
    [SerializeField]
    private int _lives = 3;
    public static int score;
    private SpawnManager _spawnManager;
    private bool isSpeedAvtive, isShieldActive, isTripleShotAvtive, isDeathBeamActive = false;
    Vector3 startingPos = new Vector3(0, 0, 0);
    public Renderer sheildRend;
    private UIManager _uiManager;
    private GameManager _gameManager;
    private Animator _leftAnim;
    private Animator _rightAnim;
    private player _player;
    [SerializeField]
    private GameObject _rightDmg, _leftDmg;
    [SerializeField]
    private AudioClip _bulletSoundClip;
    
    [SerializeField]
    private AudioSource _audioSource;

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _player =  GameObject.Find("Player").GetComponent<player>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _leftAnim = gameObject.GetComponent<Animator>();
        _rightAnim = gameObject.GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();

        transform.position = startingPos;

        if(_audioSource == null) {
            Debug.Log("Audio source on player is null");
        }
        else 
        {
            _audioSource.clip = _bulletSoundClip;
        }


        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is Null");
        }
            var sheildRend = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
            sheildRend.enabled = false;
            _player = GameObject.Find("Player").GetComponent<player>();
        
        if (_player == null)
        {
            Debug.LogError("The player is null!");
        }
        if(_leftAnim == null)
        {
            Debug.LogError("Animator is null!");
        }
         if(_rightAnim == null)
        {
            Debug.LogError("Animator is null!");
        }
    }
    void Update()
    {
        Movement();
        
         if (Input.GetKeyDown(KeyCode.Space) && Time.time > delayedShot)
        {
            fire();
        }
        Death();
    }
    void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _speed * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _rightAnim.ResetTrigger("rightPlayerAnim");

            _leftAnim.SetTrigger("leftPlayerAnim");
        }
        else
        {
            _leftAnim.ResetTrigger("leftPlayerAnim");
        }
      
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            _leftAnim.ResetTrigger("leftPlayerAnim");
            _rightAnim.SetTrigger("rightPlayerAnim");
        }
         else
        {
            _rightAnim.ResetTrigger("rightPlayerAnim");
        }
      
        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);
        if (isSpeedAvtive == true)
        {
            _speed = 13f;            
        }
        else
        {
            _speed = 5.5f;
        }

        if (transform.position.x >= 11)
        {
        
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
           
            transform.position = new Vector3(11f, transform.position.y, 0);
        }

       
    }
    void fire()
    {
        GameObject findPlayer = GameObject.FindGameObjectWithTag("Player");
        if (isTripleShotAvtive == true)
        {
            delayedShot = Time.time + fireRate;
            Instantiate(TripleShotPrefab, transform.position + (Vector3.up * .5f), Quaternion.identity);
        }
        else if (isDeathBeamActive == true && isTripleShotAvtive == false)
        {
            delayedShot = Time.time + fireRate * 2;
            Instantiate(DeathBeamPrefab, transform.position, Quaternion.identity, findPlayer.transform.parent);
        }
        else
        {
            delayedShot = Time.time + fireRate;
            Instantiate(BulletPrefab, transform.position + (Vector3.up * .8f), Quaternion.identity);
        }

        _audioSource.Play();
    }
    public void Damage()
    {
        if (isShieldActive == true)
        {
            isShieldActive = false;
            var sheildRend = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
            sheildRend.enabled = false;
            return;
        }

        _health -= 20;
        if (_health == 0)
        {
            _lives--;
           _uiManager.UpdateLives(_lives);
            _health = 100;
        }
        if (_lives == 2)
        {
            _leftDmg.SetActive(true);
        }
        else if(_lives == 1)
        {
            _rightDmg.SetActive(true);
        }
    }
    public void Death()
    {
        var enemyClones = GameObject.FindGameObjectsWithTag("Enemy");
        if (_lives == 0)
        {
            
            foreach (var clone in enemyClones)
            {
                Destroy(clone);
            }
            _health = 100;
            score = 0;
            _spawnManager.OnDeath();
            Destroy(this.gameObject);
            _uiManager.GameOver();
          
        
        }
        
    }
   
   
    public int Health() 
    {  
        return  _health;
    }
    public void TipleShotActive()
    {
        isTripleShotAvtive = true;
        StartCoroutine(TriShotInactive());

    }
    public void SpeedActive()
    {
        isSpeedAvtive = true;
        StartCoroutine(SpeedInactive());
    }
    public void DeathBeamActive()
    {
        isDeathBeamActive = true;
        StartCoroutine(DeathBeamInactive());
    }
    public void ShieldActive()
    {
        isShieldActive = true;
        var sheildRend = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
        sheildRend.enabled = true;
        StartCoroutine(ShieldInactive());
    }
    IEnumerator TriShotInactive()
    {
        yield return new WaitForSeconds(5);
        isTripleShotAvtive = false;
    }
    IEnumerator SpeedInactive()
    {
        yield return new WaitForSeconds(10);
        isSpeedAvtive = false;
    }
    IEnumerator ShieldInactive()
    {
        yield return new WaitForSeconds(8);
        var sheildRend = this.gameObject.transform.GetChild(0).GetComponent<Renderer>();
        sheildRend.enabled = false;
        isSpeedAvtive = false;
    }
    IEnumerator DeathBeamInactive()
    {
        yield return new WaitForSeconds(5);
        isSpeedAvtive = false;
    }
    
    
}

