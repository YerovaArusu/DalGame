using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject player;
    public List<Slot_UI> slots = new List<Slot_UI>();
    
    void Start()
    {
        inventoryPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
           ToggleInv(); 
           SetupInventory();
        }
    }

    public void ToggleInv()
    {
        if (!inventoryPanel.activeSelf)
        {
           inventoryPanel.SetActive(true); 
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void SetupInventory()
    {
        
    }
}