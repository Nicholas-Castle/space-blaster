using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int _speed = 4;
    private float lowerBounds = -6;

    [SerializeField]
    private int powerupID;
    [SerializeField]
    private AudioClip _clip;


    void Update()
    {       
        var getPosY = this.gameObject.transform.position.y;

        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (getPosY < lowerBounds)
        {
           OnBecameInvisible();
        }
       
    }
     private void OnTriggerEnter2D(Collider2D other) 
        {   
            
            player playerComponet = other.transform.GetComponent<player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            
            if (other.tag == "Player")
            {
                if (playerComponet != null)
                {
                    switch(powerupID)
                    {
                        case 0:
                            playerComponet.TipleShotActive();
                            OnBecameInvisible(); 
                            break;
                        case 1:
                            playerComponet.SpeedActive();
                            OnBecameInvisible();
                            break;
                        case 2:
                            playerComponet.ShieldActive();
                            OnBecameInvisible();
                            break;
                        /*case 3:
                            playerComponet.DeathBeamActive();
                            OnBecameInvisible();
                            break;*/
                    }
                }   
            }
        }
    void OnBecameInvisible() 
    {
        
        Destroy(this.gameObject);    
    }
    
}
