using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
 
    void Update()
    {
        if (this.gameObject.transform.position.y > 10)
        {
            OnBecameInvisible();
        }
    }

    private void OnBecameInvisible() 
    {
        Destroy(this.gameObject);    
    }
}