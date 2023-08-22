using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public bool multipleFire = false;
    public bool isShield = false;
    public float bulletSpeed = 20f;
    public int bulletDamage = 10;
    public float bulletGravityScale = 0.01f;
    public float shotgunAngle = 12f;
    Quaternion shotgunAngle1;
    Quaternion shotgunAngle2;
    //Vector3 bulletRot = new Vector3(0, -90, 0);

    private void Start()
    {
        bulletPrefab.GetComponent<Bullet>().speed = bulletSpeed;
        bulletPrefab.GetComponent<Bullet>().damage = bulletDamage;

        shotgunAngle1 = Quaternion.Euler(0, 0, shotgunAngle);
        shotgunAngle2 = Quaternion.Euler(0, 0, -shotgunAngle);
    }

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
        if (multipleFire)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * shotgunAngle1);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * shotgunAngle2);
        }
        else
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        
    }
}
