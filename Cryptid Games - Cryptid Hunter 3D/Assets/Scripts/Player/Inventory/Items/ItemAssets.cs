using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public Transform itemWorldPrefab;

   // private GameObject itemWorldNetworkObject;
    public static ItemAssets Instance{get; private set;}

    private void Awake() {
        Instance= this;
       // itemWorldNetworkObject= GameObject.Find("ItemWorld(Clone)");
        //itemWorldPrefab= itemWorldNetworkObject.transform;
    }

    public Sprite swordSprite;
    public Sprite healthPotionSprite;
    public Sprite manaPotionSprite;
    public Sprite coinSprite;
    public Sprite medkitSprite;
    public Sprite WoodSprite;
    public Sprite OreSprite;
    



}
