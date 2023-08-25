using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [Header("General")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public PlayerMovement playerMovement;
    public WeaponManager manager;
    

    [Header("Bullet details")]
    public float bulletSpeed = 20f;
    public int bulletDamage = 10;
    public int ammo = 1;
    //public float bulletGravityScale = 0.01f;

    [Header("Weapon Type")]
    public bool isPistol;
    public bool isShotgun;
    public bool isBurstgun;
    public bool isExplosive;
    public bool isShield;
    public bool isLaser;
    public bool isMedkit;

    [Header("Weapon Specific Details")]
    public float shotgunAngle = 12f;
    public float burstDelayTime = 0.1f;
    //public float laserDelayTime = 1f;
    public Vector3 shieldOffset = new Vector3(0.5f, 0f, 0f);
    public float bulletDistance = 10f;

    Coroutine shoot;
    public bool pointerOnUI;
    Quaternion shotgunAngle1;
    Quaternion shotgunAngle2;
    GameObject shield;
    

    private void Start()
    {
        if(isLaser)
        {
            bulletPrefab.GetComponent<Laser>().speed = bulletSpeed;
            bulletPrefab.GetComponent<Laser>().damage = bulletDamage;
            bulletPrefab.GetComponent<Laser>().distance = bulletDistance;
        }
        else if (isExplosive)
        {
            bulletPrefab.GetComponent<ExplosiveBullet>().speed = bulletSpeed;
            bulletPrefab.GetComponent<ExplosiveBullet>().damage = bulletDamage;
            bulletPrefab.GetComponent<ExplosiveBullet>().distance = bulletDistance;
        }
        else if(isMedkit)
        {
            
        }
        else
        {
            bulletPrefab.GetComponent<Bullet>().speed = bulletSpeed;
            bulletPrefab.GetComponent<Bullet>().damage = bulletDamage;
            bulletPrefab.GetComponent<Bullet>().distance = bulletDistance;
        }
        

        shotgunAngle1 = Quaternion.Euler(0, 0, shotgunAngle);
        shotgunAngle2 = Quaternion.Euler(0, 0, -shotgunAngle);
    }

    // Update is called once per frame
    void Update()
    {
        pointerOnUI = EventSystem.current.IsPointerOverGameObject();
        if (Input.GetMouseButtonDown(0))
        {
            if(pointerOnUI)
            {
                return;
            }
            shoot = StartCoroutine(Shoot());
        }

        if(isShield || isLaser)
        {
            if(Input.GetButtonUp("Fire1"))
            {
                //StopShoot();
            }
        }
    }
    
    IEnumerator Shoot()
    {
        //if(ammo>0)
        {
            if (isShotgun)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * shotgunAngle1);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * shotgunAngle2);
                //ammo--;
                yield return null;
            }
            if (isPistol || isExplosive)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                //ammo--;
                yield return null;
            }
            if (isBurstgun)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(burstDelayTime);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(burstDelayTime);
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                //ammo--;
                yield return null;

            }
            if (isShield)
            {
                //playerMovement.canMove = false;
                shield = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(3f);
                Destroy(shield);
                yield return null;
                //ammo--;
            }
            if (isLaser)
            {
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                yield return null;
                //ammo--;
            }
            if (isMedkit)
            {
                PlayerHealth ph = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
                ph.Heal(bulletDamage);
                yield return null;
                //ammo--;
            }
        }
        StopCoroutine(shoot);
        
    }

    void StopShoot()
    {
        if (isShield)
        {
            playerMovement.canMove = true;
            Destroy(shield);
        }
        if(isLaser)
        {
            //Draw2DRay(Vector3.zero,Vector3.zero);
        }
    }
}
