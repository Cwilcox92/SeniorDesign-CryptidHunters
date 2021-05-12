using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Item : MonoBehaviour
{
   public enum ItemType
   {
       Sword,
       HealthPotion,
       ManaPotion,
       Coin,
       MedKit,
       Ore,
       Wood,
   }

   public ItemType itemType;
   public int amount;

   public static Sprite GetSprite(ItemType itemType)
   {
       switch(itemType)
       {
           default:
           case ItemType.Wood: return ItemAssets.Instance.WoodSprite;
           case ItemType.Ore: return ItemAssets.Instance.OreSprite;
           case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
           case ItemType.ManaPotion: return ItemAssets.Instance.manaPotionSprite;
           case ItemType.Coin: return ItemAssets.Instance.coinSprite;
           case ItemType.MedKit: return ItemAssets.Instance.medkitSprite;
       }
   }
   public Sprite GetSprite() {
        return GetSprite(itemType);
    }

   public bool IsStackable()
   {
       switch(itemType)
       {
           default:
           case ItemType.HealthPotion: 
           case ItemType.ManaPotion: 
           case ItemType.Coin:
                return true; 
           case ItemType.Sword: 
           case ItemType.MedKit: 
                return false;
       }

   }
}
