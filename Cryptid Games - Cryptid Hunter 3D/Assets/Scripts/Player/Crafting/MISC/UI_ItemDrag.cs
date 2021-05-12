using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemDrag : MonoBehaviour
{
    public static UI_ItemDrag Instance {get; private set;}
    private RectTransform parentRectTransform;
    private Item item;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        parentRectTransform= transform.parent.GetComponent<RectTransform>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, Input.mousePosition, null, out Vector2 localPoint);
        transform.localPosition= localPoint;
    }

    public Item GetItem()
    {
        return item;
    }

      public void SetItem(Item item)
    {
        this.item= item;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
