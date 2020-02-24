using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyContainer, EnemyPrefab;
    private bool _stopSpawn;
    private float _spawnPosx = 11f;
    
    IEnumerator SpawnRoutine()
    {
        
        float _posX = transform.position.x;
        Vector3 spwanPos = new Vector3(_posX, -_spawnPosx, 0);
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawn == false)
        {
            GameObject newEnemy = Instantiate(EnemyPrefab, spwanPos, Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(1);   
        }
    }
    
    public void OnDeath()
    {
        _stopSpawn = true;
    }
    
    public void BeginSpawn()
    {
        StartCoroutine(SpawnRoutine());
    }
}

