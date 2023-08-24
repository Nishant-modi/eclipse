using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public GameObject border;
    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if(transform.childCount == 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void ActivateBorder()
    {
        border.SetActive(true);
    }

    public void DropItem()
    {
        border.SetActive(false);
        foreach (Transform child in transform)
        {
            child.GetComponent<SpawnItem>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }
}
