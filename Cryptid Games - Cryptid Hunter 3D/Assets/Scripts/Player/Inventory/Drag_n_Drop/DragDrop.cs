using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    private RectTransform rectTransform;
    [SerializeField]private Canvas canvas; // not sure what to do here cuz we dont draw on a 2d canvas
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        rectTransform  = GetComponent<RectTransform>();  
        canvasGroup = GetComponent<CanvasGroup>();   
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha= .6f;
        canvasGroup.blocksRaycasts= false;
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
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
