using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryUI;
    //[SerializeField] private InventoryPage equippedUI;
    public int inventorySize = 3;
    public int equippedSize = 1;

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        //equippedUI.InitializeInventoryUI(equippedSize);
        if (inventoryUI.isActiveAndEnabled == false)
        {
            inventoryUI.Show();
            //equippedUI.Show();
        }
    }
}
