using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UniversalDragDrop : MonoBehaviour, IDropHandler
{
    [SerializeField]private Vector2 inventoryAnchor;
    private void Awake() {
        inventoryAnchor= GameObject.Find("itemSlotTemplate").GetComponent<Transform>().position;
        GetComponent<RectTransform>().anchoredPosition = inventoryAnchor;
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    
}
