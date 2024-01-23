using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{

    public GameObject inventoryPanel;
    public GameObject player;
    public List<Slot_UI> slots = new List<Slot_UI>();
    private StatsSystem statsSystem;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

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
        statsSystem = player.GetComponent<StatsSystem>();

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
        
        
        if (player != null && statsSystem != null)
        {
            if (slots != null)
            {
                
                if (slots.Count == statsSystem.inventory.slots.Count)
                {
                    for (int i = 0; i < slots.Count; i++)
                    {
                     
                        if (slots[i] != null && statsSystem.inventory.slots[i] != null)
                        {
                            if (statsSystem.inventory.slots[i].type != Collectible_Type.NONE)
                            {
                                foreach (Inventory.Slot slot in statsSystem.inventory.slots)
                                {
                                    Collectible_Type type = slot.type;
                                    int count = slot.count;
                                    int maxAllowed = slot.maxAllowed;
                                    Sprite icon = slot.icon;
                                    
                                }

                                slots[i].SetItem(statsSystem.inventory.slots[i]);
                            }
                            else
                            {
                                slots[i].SetEmpty();
                            }
                        }
                        else
                        {
                            Debug.LogError("Slot or inventory slot is null at index " + i);
                        }
                    }
                }
                else
                {
                    Debug.LogError("Mismatched slot counts between Inventory_UI and StatsSystem.");
                }
            }
            else
            {
                Debug.LogError("Slots list is null in Inventory_UI.");
            }
        }
        else
        {
            Debug.LogError("Player or StatsSystem is null in Inventory_UI.");
        }
    }

}
