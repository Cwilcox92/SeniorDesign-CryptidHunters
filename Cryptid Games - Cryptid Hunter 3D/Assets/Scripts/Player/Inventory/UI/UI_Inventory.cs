using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    
    private Transform itemSlotTemplate; 
    private Transform itemSlotContainer;
    private GameObject player;
    private bool isDestroyed= false;
    private Crafting crafting;
    private Transform outputSlot; 
    private Transform craftingSlotTemplate; 
    private Transform gridContainer, craftingContainer;
    private int playerCounter= 0;
    private GameObject slot01, slot11, slot02, slot22, craftedItem;
    private Transform Slot0, Slot1, Slot2, Slot3;
    private bool pressedWood, pressedOre;
    private PlayerController playerController;
    private bool removed = false;

    void Awake()
    {
            playerController= new PlayerController();
            itemSlotContainer= transform.Find("ItemSlotContainer");
            //Debug.Log(itemSlotContainer);
            itemSlotTemplate= itemSlotContainer.Find("itemSlotTemplate");
            craftingContainer= transform.Find("Crafting");
            gridContainer= craftingContainer.Find("Grid_Container");
            craftingSlotTemplate = craftingContainer.Find("Crafted_Item");
            outputSlot= transform.Find("createdItemTemplate");
            Slot0= gridContainer.Find("Slot_0_0");
            Slot1= gridContainer.Find("Slot_0_1");
            Slot2= gridContainer.Find("Slot_1_0");
            Slot3= gridContainer.Find("Slot_1_1"); 
            craftedItem=  craftingSlotTemplate.GetChild(0).gameObject;
            slot01= Slot0.GetChild(0).gameObject;   
            Debug.Log(slot01);   
            slot11= Slot1.GetChild(0).gameObject;  
            slot02= Slot2.GetChild(0).gameObject;  
            slot22= Slot3.GetChild(0).gameObject;  
          
    }
    public void SetPlayer()
    {
        this.player= GameObject.Find("Test_PLayer(Clone)");
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();

    }

    private void Inventory_OnItemListChanged(object sender, EventArgs e)
    {
       RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        if(isDestroyed)
        {
            return;

        }
        else
        {
        foreach(Transform child in itemSlotContainer)
        {
            if(child == itemSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);

        }
        int x = 0;
        int y= 0;
        float itemSlotCellSize = 60f;
        foreach(Item item in inventory.GetItemList())
        {         
            RectTransform itemSlotRecTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRecTransform.gameObject.SetActive(true);

            itemSlotRecTransform.GetComponent<Button_UI>().ClickFunc= () =>
            {
                playerCounter++;
                inventory.UseItem(item);
                Debug.Log("Player counter: "+playerCounter);
                if(playerCounter == 1)
                {
                    slot01.SetActive(true);
                }
                if(playerCounter == 2)
                {
                    slot11.SetActive(true);
                }
                if(playerCounter == 3)
                {
                    slot02.SetActive(true);
                }
                if(playerCounter == 4 )
                {
                    slot22.SetActive(true);
                    craftedItem.SetActive(true);
                    playerCounter= 0;

                }
                if(playerCounter > 4)
                {
                   inventory.RemoveItem(item);
                   RefreshInventoryItems();
                }
                if(item.amount == 0)
                {
                    inventory.RemoveItem(item);
                    removed= true;
                }           
            };
                    
            itemSlotRecTransform.anchoredPosition= new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRecTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite= item.GetSprite();
            if(removed == true)
            {
                Destroy(itemSlotTemplate);
            }  
            TextMeshProUGUI uiText = itemSlotRecTransform.Find("Amount").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            

            x++;
            if(x > 5)
            {
                x =0;
                y--;
            }
        }
        }

    }
}
