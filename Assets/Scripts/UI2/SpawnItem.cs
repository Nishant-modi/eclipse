using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    public int index;
    public WeaponManager weaponManager;
    private GameObject[] borders;
    public int ammo = 0;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weaponManager = GameObject.FindGameObjectWithTag("WeaponManager").GetComponent<WeaponManager>();
        borders = GameObject.FindGameObjectsWithTag("borders");

    }

    public void SpawnDroppedItem()
    {
        weaponManager.DeactivateCompanion();
        Vector2 pos = new Vector2 (player.position.x + 1f, player.position.y);
        Instantiate(item, pos, Quaternion.identity);
    }

    public void UseCompanion()
    {
        foreach (GameObject item in borders)
        {
            item.SetActive(false);
            print(item);
        }
        weaponManager.ActivateCompanion(index);
        Slot currentBorder = transform.parent.gameObject.GetComponentInChildren<Slot>();
        currentBorder.ActivateBorder();

    }
}
