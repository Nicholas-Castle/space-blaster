using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 5.0f;

    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;


    void Start() 
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();    
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
    }

    // check for laser collsion (trigger)
    // instaintiate explosion at the pos og the astroid ()
    // destroy explosion after 3 sec
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(other.gameObject);
        _spawnManager.BeginSpawn();
        Destroy(this.gameObject, 0.1f);
        }
    }
}
