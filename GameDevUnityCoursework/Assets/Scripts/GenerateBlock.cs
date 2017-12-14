using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlock : MonoBehaviour {
    
    public GameObject[] blocks;
    public GameObject[] collectableItems;
    
    public static int blocksCreated = 2;
    public static int blocksDestroyed = 0;
    private bool ShouldCollider = true;
    
    void GenerateNewBlock(){
        
        Vector3 blockPosition = new Vector3(0.0f, 0.0f, 100.0f * blocksCreated);
        GameObject trophyBlock = blocks[blocks.Length - 1];
        GameObject blockToGenerate = GameManager.endOfAsteroidAttack ? trophyBlock : blocks[Random.Range(0, blocks.Length - 1)];
        GameObject newBlock = Instantiate(blockToGenerate, blockPosition, Quaternion.identity) as GameObject;
        Transform collectables = newBlock.transform.Find("Collectables");
        SpawnCollectableItemIn(collectables);
        
        blocksCreated += 1;
        blocksDestroyed += 1;
    }
    
    void SpawnCollectableItemIn( Transform collectables){
        GameObject itemToSpawn = null;
        
        foreach (Transform child in collectables) {

            if (child.tag == "CollectableA") {
                itemToSpawn = getCollectableItem("GreenCrystal");
            } else if (child.tag == "CollectableB") {
				string[] itemNames = { "CandyBar", "Shield", "Speed" };
				itemToSpawn = getCollectableItem(itemNames[Random.Range(0, itemNames.Length)]);
            } else if (child.tag == "CollectableC"){
                itemToSpawn = getCollectableItem("Gold");
            }else if (child.tag == "CollectableD") {
                itemToSpawn = getCollectableItem("Trophy");
            }
            
            // 
            // child CollectableA -> Special  (crystal)
            // child CollectableB -> GamePlay Balancing (shield, speed, candy bar)
            // child CollectableC -> Points (gold)
            // child CollectableD -> Points (trophy)

            if (itemToSpawn)
            {
                GameObject collectable = Instantiate(itemToSpawn, child.position, Quaternion.identity) as GameObject;
                collectable.name = itemToSpawn.name;
                collectable.transform.parent = child;
            }
        }
    }
    
    GameObject getCollectableItem(string itemName ) {
        GameObject item = null;
        foreach (GameObject child in collectableItems)
        {
            if (child.name == itemName)
            {
                item = child;
                break;
            }
        }

        return item;
    }

    void ResetCollider()
    {
        ShouldCollider = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BlockCollider"){
            if (ShouldCollider)
            {
				Destroy(other.gameObject);
                GenerateNewBlock();
                ShouldCollider = false;
                Invoke("ResetCollider", 1.0f);
            }
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "BlockCollider"){
        
        }
    }
}
