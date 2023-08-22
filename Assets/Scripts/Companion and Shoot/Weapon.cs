using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool multipleFire = false;
    public float bulletSpeed = 20f;
    public int bulletDamage = 10;
    public float bulletGravityScale = 0.01f;
    //Vector3 bulletRot = new Vector3(0, -90, 0);


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
