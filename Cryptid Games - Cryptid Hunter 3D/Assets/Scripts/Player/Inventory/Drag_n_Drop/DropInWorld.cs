using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Mirror;
public class DropInWorld : NetworkBehaviour, IDropHandler
{

    // I want to get the players positions and create the item in world that was droped
    private RectTransform rectTransform;
    [SerializeField]private Canvas canvas; // not sure what to do here cuz we dont draw on a 2d canvas
    private CanvasGroup canvasGroup;
    [SerializeField] private GameObject player;
    private Vector2 playerPos;
    private Item currentItem;
    
    private void Awake()
    {
        rectTransform  = GetComponent<RectTransform>();  
        canvasGroup = GetComponent<CanvasGroup>();
        playerPos= player.transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha= .6f;
        canvasGroup.blocksRaycasts= false;
        //Item.GetSprite();
    }

    public void OnDrag(PointerEventData eventData)
    {
         Debug.Log("OnDrag");
         rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // moves teh object teh amount the mouse moved in frame, when the canvas shit is figured our we ( /canvas.scalefactor)
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha= 1f;
        canvasGroup.blocksRaycasts= true;
        SpanwItem();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped in World");
        Debug.Log("Will Spawn @: "+ playerPos);
        SpanwItem();
        



    }
    [Server]
    private void SpanwItem()
    {
        ItemWorld.SpawnItemWorld(playerPos, new Item { itemType = Item.ItemType.Sword, amount = 2});
    }
}
