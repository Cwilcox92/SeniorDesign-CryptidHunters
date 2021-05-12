using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This is an implementation adapted from Code Monkey https://www.youtube.com/watch?v=2WnAOV7nHW0&t=1347s
// And have been modified by Carlton Wilcox

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) // this spawns items into the wolrd, I am going to have o meet with randon to integrate this in to the map generation
    {
        Transform itemTransform = Instantiate(ItemAssets.Instance.itemWorldPrefab, position, Quaternion.identity);

        ItemWorld itemWorld = itemTransform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;

    }

    private Item item;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }
    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();

    }

    public Item GetItem()
    {
    return item;        
    }

     public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir= new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized; 
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 5f, item);
        itemWorld.GetComponent<Rigidbody>().AddForce(randomDir * 5f, ForceMode.Impulse);
        return itemWorld;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);// going to have to fix this to destroy over the network
    }
}