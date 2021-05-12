using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;


public class UI_CraftingItemSlot : MonoBehaviour, IDropHandler
{
    private int x;
    private int y;
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs: EventArgs
    {
        public Item item;
        public int x; 
        public int y;
    }
    public void OnDrop(PointerEventData eventData)
    {
        UI_ItemDrag.Instance.Hide();
        Item item= UI_ItemDrag.Instance.GetItem();
            
        OnItemDropped?.Invoke(this, new OnItemDroppedEventArgs {item = item});
    }

    public void SetXY(int x, int y)
    {
        this.x= x;
        this.y= y;
    }
}
