using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class Created_Item_Spawning : NetworkBehaviour
{

    private GameObject player;
    private Vector3 playerPos;
    [SerializeField] private GameObject trapPrefab = null;
    private mapGenerator mapGenerator;


private void Start() {
     player= GameObject.Find("TestPLayer(Clone)");
     playerPos= player.transform.position;        
}
    public void SpawnCraftedItem()
    {
       // Debug.Log("PLayer: "+ player);
       // Debug.Log("Trap: " + trapPrefab);
        GameObject craftedItem= Instantiate(trapPrefab, player.transform);
        craftedItem.transform.parent = null;
        Debug.Log("Player pos at: " + player.transform.position);
       Debug.Log("Crafted Item Prefab will spawn here");
        NetworkServer.Spawn(craftedItem);
    }
}
