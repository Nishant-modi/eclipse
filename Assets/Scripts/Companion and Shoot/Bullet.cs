using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float distance;
    //public float laserDelayTime;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            ZombieHealth zh = collision.GetComponent<ZombieHealth>();
            zh.TakeDamage(damage);
        }

        if(collision.transform.tag != "Bullet")
        {
            Destroy(gameObject);
        }
        
    }
}
