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

    void Awake()
    {
            itemSlotContainer= transform.Find("ItemSlotContainer");
            Debug.Log(itemSlotContainer);
            itemSlotTemplate= itemSlotContainer.Find("itemSlotTemplate");
        
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
                //Use item
                inventory.UseItem(item);


            };

              itemSlotRecTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //Drop item
                Item duplicateItem = new Item {itemType = item.itemType, amount= item.amount};
                inventory.RemoveItem(duplicateItem);
                isDestroyed= true;
                Debug.Log("Player here: "+player.transform.position);
                Debug.Log("Duplicate Item: "+duplicateItem);
                ItemWorld.DropItem(player.transform.position, duplicateItem);


            };


            itemSlotRecTransform.anchoredPosition= new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRecTransform.Find("ItemImage").GetComponent<Image>();
            image.sprite= item.GetSprite();


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
