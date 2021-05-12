using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Crafting_UI : MonoBehaviour
{
     public event EventHandler OnItemListChanged;
     private Crafting crafting;

    private Transform gridContainer;

    private Transform[,] slotTransformArray;
    private Transform craftingSlotTemplate;
    private Transform outputSlot;  
     private GameObject player;   
    private bool isDestroyed= false;  
    int counter = 0;

    private void Awake() {
        gridContainer= transform.Find("Crafting");
        craftingSlotTemplate = gridContainer.Find("craftingSlotTemplate");
        outputSlot= gridContainer.Find("createdItemTemplate");
    }

     public void SetPlayer()
    {
        this.player= GameObject.Find("Test_PLayer(Clone)");
    }

    public void SetCrafting(Crafting crafting)
    {
        this.crafting = crafting;
        crafting.OnItemListChanged += Crafting_OnItemListChanged;
        RefreshCraftingItems();
    }

    private void Crafting_OnItemListChanged(object sender, EventArgs e)
    {
       RefreshCraftingItems();
    }

    public void RefreshCraftingItems()
    {
        foreach(Transform child in gridContainer)
        {
            if(child == craftingSlotTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);

        }
        int x = 0;
        int y= 0;
        float itemSlotCellSize = 60f;
        foreach(Item item in crafting.GetItemList())
        {         
            RectTransform itemSlotRecTransform = Instantiate(craftingSlotTemplate, gridContainer).GetComponent<RectTransform>();
            itemSlotRecTransform.gameObject.SetActive(true);
          
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
            if(x > 2)
            {
                x =0;
                y--;
            }
        }
    }









    
}
