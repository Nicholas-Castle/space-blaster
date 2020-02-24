using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField]
    private float _speed = 10.0f;
  
    void Update()
    {
       
        transform.Translate(Time.deltaTime * _speed * Vector3.up);
        if (transform.position.y > 10)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);    
        }
    }
    
}
