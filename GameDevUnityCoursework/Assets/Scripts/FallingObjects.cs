using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity3d.com/questions/708141/ie-numerator-to-spawn-falling-objects-along-x-axis.html
// http://answers.unity3d.com/questions/1087572/instantiate-objects-around-a-sphere-inside-a-spher.html

public class FallingObjects : MonoBehaviour {

    private float minTime = 5f; 
    private float maxTime = 10f;
    private float horizontalBounds = 30f;
    
    public GameObject[] fallers;
    
    public bool doSpawn = true;
    
	public void SetTimers(float min, float max){
		minTime = min;
		maxTime = max;
		
		StartFallingAsteroids();
	}
	
    void StartFallingAsteroids() {
        StartCoroutine(Spawner());
    }
    
    
    IEnumerator Spawner() {
       
        do
        {
            FallOnGround();
              
			yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            
        }while(doSpawn && GameManager.endOfAsteroidAttack == false);
     }
    
    void FallOnGround(){
        GameObject faller = fallers[Random.Range(0, fallers.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-horizontalBounds, horizontalBounds), Random.Range(100.0f, 200.0f), Random.Range(100.0f * GenerateBlock.blocksDestroyed, 100.0f * GenerateBlock.blocksCreated));
        Quaternion spawnRotation = Random.rotation; // Quaternion.identity
        
        GameObject newFaller = Instantiate(faller, spawnPosition, spawnRotation) as GameObject;
        newFaller.name = faller.name;
        newFaller.transform.parent = this.transform;
        
    }


}
