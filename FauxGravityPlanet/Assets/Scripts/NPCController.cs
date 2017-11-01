using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://answers.unity3d.com/questions/429623/enemy-movement-from-waypoint-to-waypoint.html
// http://answers.unity3d.com/questions/423208/need-ai-script-that-makes-object-follow-the-player.html

[RequireComponent(typeof(Rigidbody))]
public class NPCController : MonoBehaviour {


    public GameObject planet;
    
    List<Vector3> waypoints= new List<Vector3>(); // The amount of Waypoint you want
    public float patrolSpeed = 3f;       // The walking speed between Waypoints
    public bool  loop = true;       // Do you want to keep repeating the Waypoints
    public float dampingLook = 6.0f;          // How slowly to turn
    public float pauseDuration = 0.0f;   // How long to pause at a Waypoint

    private float curTime = 0.0f;
    private int currentWaypoint = 0;

    void Start()
    {

        this.transform.position = getRandomPos();
        AddGravityBody(this.gameObject);
        
        for (int i = 0; i < 50; i++){
            waypoints.Add(getRandomPos());
            print(waypoints[i]);
        }
     
	}
    
    Vector3 getRandomPos(){
        // to spawn around a planet, you need the planet (sphere) radius, spawn character ( known as fallingObject) height and planet position
        float planetRadius = planet.transform.localScale.x * 0.5f;
        float fallerheight = this.transform.localScale.y * 0.5f;
        Vector3 planetPosition = planet.transform.position;
        
        return Random.onUnitSphere * (planetRadius + fallerheight) + planetPosition;
    }
    
    void AddGravityBody(GameObject npc){
        npc.name = "NPC Player";
        //npc.transform.parent = this.transform;
        npc.AddComponent<FauxGravityBody>();
        npc.GetComponent<FauxGravityBody>().body = FauxGravityBody.BodyType.StaticBody;
        npc.GetComponent<FauxGravityBody>().gravity = Random.Range(-50.0f, -9.82f);
        npc.GetComponent<FauxGravityBody>().attractor = planet.GetComponent<FauxGravityAttractor>();
    }
    
    // Update is called once per frame
    void Update () {
        
        
        if(currentWaypoint < waypoints.Count){
            patrol();
        }else{    
        
            if(loop){
                currentWaypoint = 0;
            } 
        }
     
	}
    
    void  patrol (){
        
        Vector3 target = waypoints[currentWaypoint];
        target.y = transform.position.y; // Keep waypoint at character's height
        Vector3 moveDirection = target - transform.position;
        
        if(moveDirection.magnitude < 0.5f){

            if (curTime <= 0.0f)
            {
                curTime = Time.time; // Pause over the Waypoint
            }
        
            if ((Time.time - curTime) >= pauseDuration){
                currentWaypoint++;
                curTime = 0;
            }
        }else{        
            var rotation = Quaternion.LookRotation(target - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * dampingLook);
            this.transform.Translate(moveDirection.normalized * patrolSpeed * Time.deltaTime);
        }  
        
        
    }
    

    
}
