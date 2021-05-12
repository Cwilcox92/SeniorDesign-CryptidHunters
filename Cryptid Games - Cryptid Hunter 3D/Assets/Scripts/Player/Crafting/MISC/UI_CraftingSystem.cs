using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CraftingSystem : MonoBehaviour
{
    /*
    [SerializeField] private Transform pfUI_Item;
    private Transform [,] slotTransformArray;
    private Transform outputSlotTransform;
    private Transform itemContainer;


    private void Awake()
    {
        Transform gridContainer = transform.Find("Grid_Container");
        itemContainer= transform.Find("itemContainer");

        slotTransformArray = new Transform[CraftingSystem.GRID_SIZE, CraftingSystem.GRID_SIZE];
        
        for(int x= 0; x < CraftingSystem.GRID_SIZE; x++ )
        {
            for(int y= 0; y < CraftingSystem.GRID_SIZE; y++)
            {
                slotTransformArray[x,y]= gridContainer.Find("Slot_"+ x + "_"+ y);
                UI_CraftingItemSlot craftingItemSlot = slotTransformArray[x,y].GetComponent<UI_CraftingItemSlot>();
                craftingItemSlot.SetXY(x,y);
                craftingItemSlot.OnItemDropped += UI_CraftingSystem_OnItemDropped; 
            }
        }
         outputSlotTransform= transform.Find("Crafted_Item");
         CreateItem(0,0, new Item {itemType = Item.ItemType.Wood});
         CreateItem(1,1, new Item {itemType = Item.ItemType.Ore});
         CreateItemOutput(new Item {itemType= Item.ItemType.Sword});
    }

    private void UI_CraftingSystem_OnItemDropped(object sender, UI_CraftingItemSlot.OnItemDroppedEventArgs e)
    {
        Debug.Log(e.item + " "+ e.x + " " + e.y);
    }

    private void CreateItem(int x, int y, Item item)// creates the item on the grid
    {
        Transform itemTransform= Instantiate(pfUI_Item, itemContainer); // instantiate prefab
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = slotTransformArray[x,y].GetComponent<RectTransform>().anchoredPosition;// put it on the correct slot
        itemTransform.GetComponent<DragDrop>().SetItem(item);// call the ItemWorld to set the item

    }
      private void CreateItemOutput(Item item)// creates the item on the grid output
    {
        Transform itemTransform= Instantiate(pfUI_Item, itemContainer); // instantiate prefab
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = outputSlotTransform.GetComponent<RectTransform>().anchoredPosition;// put it on the correct slot
        itemTransform.GetComponent<DragDrop>().SetItem(item);// call the ItemWorld to set the item

    }*/
}
