using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{

    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    [SerializeField] private InventoryDescription itemDescription;
    //[SerializeField] private WeaponManager weaponManager;

    List<InventoryItem> listOfItems = new List<InventoryItem>();

    [Header("temporary variables")]
    public Sprite image;
    public int qty;
    public string title;



    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for(int i=0; i < inventorySize; i++)
        {
            InventoryItem item = Instantiate(itemPrefab, Vector3.zero,Quaternion.identity);
            item.transform.SetParent(contentPanel);
            listOfItems.Add(item);
            item.OnItemClicked += HandleItemSelection;
            item.OnItemBeginDrag += HandleBeginDrag;
            item.OnItemDroppedOn += HandleSwap;
            item.OnItemEndDrag += HandleEndDrag;

        }
    }

    private void HandleEndDrag(InventoryItem obj)
    {
        Debug.Log(obj.name);
    }

    private void HandleSwap(InventoryItem obj)
    {
        Debug.Log(obj.name);
    }

    private void HandleBeginDrag(InventoryItem obj)
    {
        Debug.Log(obj.name);
    }

    private void HandleItemSelection(InventoryItem obj)
    {
        itemDescription.SetDescription(image, title, qty.ToString());
        listOfItems[0].Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(image, qty);
    }

    public void Hide()
    {
        gameObject.SetActive(false) ;
    }
}
