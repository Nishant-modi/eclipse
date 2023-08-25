using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public GameObject border;
    public int i;
    public GameObject[] dropImages;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if(transform.childCount == 0)
        {
            inventory.isFull[i] = false;
            dropImages[i].SetActive(false);
        }

        if(inventory.isFull[i])
        {
            dropImages[i].SetActive(true);
        }
        else
        {
            dropImages[i].SetActive(false);
        }
    }

    public void ActivateBorder()
    {
        border.SetActive(true);
    }

    public void DropItem()
    {
        border.SetActive(false);
        dropImages[i].SetActive(false);
        foreach (Transform child in transform)
        {
            child.GetComponent<SpawnItem>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
        GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
        foreach(GameObject g in weapons)
        {
            g.GetComponent<Weapon>().pointerOnUI = false;
        }
            
    }
}
