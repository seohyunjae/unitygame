using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public int dmg;
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }    
    }

}
