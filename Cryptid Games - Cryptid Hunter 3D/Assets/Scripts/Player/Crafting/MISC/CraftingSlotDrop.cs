using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSlotDrop : MonoBehaviour
{
    private Transform[,] slotTransformArray;
    private Transform itemContainer;
    private Transform outputSlot;
    private CraftingSystem crafting;

    private void Awake()
    {
        Transform gridContainer= transform.Find("Grid_Container");
        itemContainer = transform.Find("itemContainer");

        slotTransformArray= new Transform[2,2];

        for(int x= 0; x < 2; x++)
        {
            for(int y= 0; y< 2; y++)
            {
                slotTransformArray[x,y]= gridContainer.Find("Slot_"+ x +"_" + y);
                UI_CraftingItemSlot craftingItemSlot = slotTransformArray[x,y].GetComponent<UI_CraftingItemSlot>();
                craftingItemSlot.SetXY(x,y);
               craftingItemSlot.OnItemDropped += CraftingSlotDrop_OnItemDropped;
            }
        }

        outputSlot= transform.Find("Crafted_Item");        
    }

    private void CraftingSlotDrop_OnItemDropped(object sender, UI_CraftingItemSlot.OnItemDroppedEventArgs e)
    {
        crafting.TryAddItem(e.item, e.x, e.y);
    }

}

