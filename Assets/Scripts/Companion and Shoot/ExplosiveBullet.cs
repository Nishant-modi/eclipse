using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    public float speed;
    public int damage;
    public float distance;
    public float blastRadius = 5f;
    public float timeToExplode = 1f;
    //public float time;
    public Rigidbody2D rb;
    bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        //time = timeToExplode;
        StartCoroutine(TimerStart());
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    IEnumerator TimerStart()
    {
        yield return new WaitForSeconds(timeToExplode);
        if(!hasExploded)
        {
            Explode();
        }
    }
    

    void Explode()
    {
        hasExploded = true;
        var colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (var collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.gameObject.tag == "Enemy")
            {
                ZombieHealth zh = collider.gameObject.GetComponent<ZombieHealth>();
                //Debug.Log(collider.gameObject.name);
                zh.TakeDamage(damage);
            }
            if (collider.gameObject.tag == "Player")
            {
                PlayerHealth playerHealth = collider.gameObject.GetComponent<PlayerHealth>();
                

                playerHealth.TakeDamage(damage/2);
            }


        }
        Destroy(gameObject);
    }
}
