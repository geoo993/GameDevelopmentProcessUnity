using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity3d.com/questions/708141/ie-numerator-to-spawn-falling-objects-along-x-axis.html
// http://answers.unity3d.com/questions/1087572/instantiate-objects-around-a-sphere-inside-a-spher.html

public class FallingObjects : MonoBehaviour {

    [SerializeField, Range(0.0f, 10.0f)] float minTime = 5f; 
    [SerializeField, Range(1.0f, 10.0f)] float maxTime = 10f;
    [SerializeField, Range(1.0f, 500.0f)] float horizontalBounds = 50f;
    [SerializeField, Range(100, 500)] int count = 200;
    [SerializeField, Range(1.0f, 50.0f)] float awayOffset = 5.0f;
    
    public GameObject[] fallers;
    //public GameObject planet;
    
    public bool doSpawn = true;
    
    void Start() {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner() {
        while (doSpawn && count > 0) {

            FallOnGround();
            count--;
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
         }
     }
    
    void FallOnGround(){
        GameObject faller = fallers[Random.Range(0, fallers.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(-horizontalBounds, horizontalBounds), Random.Range(100.0f, 200.0f), Random.Range(100.0f * GenerateBlock.blocksDestroyed, 100.0f * GenerateBlock.blocksCreated));
        Quaternion spawnRotation = Random.rotation; // Quaternion.identity
        
        GameObject newFaller = Instantiate(faller, spawnPosition, spawnRotation) as GameObject;
        newFaller.name = faller.name;
        newFaller.transform.parent = this.transform;
        
    }

    /*
    void FallOnPlanet(){
        GameObject faller = fallers[Random.Range(0, fallers.Length)];
        // to spawn around a planet, you need the planet (sphere) radius, spawn character ( known as fallingObject) height and planet position
        float planetRadius = planet.transform.localScale.x * 0.5f;
        float fallerheight = faller.transform.localScale.y * 0.5f;
        Vector3 planetPosition = planet.transform.position;
        Quaternion rotation = Random.rotation; // Quaternion.identity
        
        Vector3 onPlanetPosition = Random.onUnitSphere * (planetRadius + fallerheight) + planetPosition;
        float distance = Vector3.Distance(planetPosition, onPlanetPosition);
        Vector3 spawnPosition = Random.onUnitSphere * (distance + awayOffset);
        
        GameObject newFaller = Instantiate(faller, spawnPosition, rotation) as GameObject;
        newFaller.name = "Faller";
        newFaller.transform.parent = this.transform;
        newFaller.AddComponent<FauxGravityBody>();
        newFaller.GetComponent<FauxGravityBody>().body = FauxGravityBody.BodyType.StaticBody;
        newFaller.GetComponent<FauxGravityBody>().gravity = Random.Range(-50.0f, -9.82f);
        newFaller.GetComponent<FauxGravityBody>().attractor = planet.GetComponent<FauxGravityAttractor>();
            
    }
    */

}
