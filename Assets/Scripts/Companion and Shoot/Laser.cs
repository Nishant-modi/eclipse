using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed;
    public int damage;
    public float distance;
    public float laserDelayTime;
    public LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootLaser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootLaser()
    {
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, transform.right);
            Draw2DRay(transform.position, ray.point);
            if (ray.collider.gameObject.tag == "Enemy")
            {
                ZombieHealth zh = ray.collider.GetComponent<ZombieHealth>();
                zh.TakeDamage(damage);
            }

        }
        else
        {
            Draw2DRay(transform.position, transform.transform.right * distance);
        }
        yield return new WaitForSeconds(laserDelayTime);
        Destroy(gameObject);
    }

    void Draw2DRay(Vector3 startPos, Vector3 endPos)
    {
        laserLine.SetPosition(0, startPos);
        laserLine.SetPosition(1, endPos);

    }
}
