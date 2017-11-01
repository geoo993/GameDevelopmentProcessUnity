using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://wiki.unity3d.com/index.php/Scripts/Controllers
// http://answers.unity3d.com/questions/26177/how-to-create-a-basic-follow-ai.html
// http://answers.unity3d.com/questions/274809/how-to-make-enemy-chase-player-basic-ai.html
// http://www.theappguruz.com/blog/unity-3d-enemy-obstacle-awarness-ai-code-sample

[RequireComponent(typeof(Rigidbody))]
public class NPCWander : MonoBehaviour {
    
    public GameObject planet;
    public GameObject player;
    
    // moving forward parameters
    public float moveSpeed = 5.0f;
    public float directionChangeInterval = 1.0f;
    public float maxHeadingChange = 30;
	private float heading;
	private Vector3 targetRotation;
    
    // follow player parameters
    public bool followPlayer = false;
    public float rotationSpeed = 3; //speed of turning
    
    [SerializeField, Range(20.0f, 100.0f)]
    private float maxDist; 
    
    [SerializeField, Range(5.0f, 20.0f) ]
    private float minDist;

    // Collision obstacle parameters
    public bool obstacleAvoidance;
    public bool debugAvoidance;
    [SerializeField, Range(2.0f, 100.0f)]
    private float forwardRayDistance;
    
    [SerializeField, Range(2.0f, 100.0f)]
    private float leftRightRayDistance;
    
    [SerializeField, Range(1.0f, 10.0f)]
    private float forwardRayDirectionOffset, leftRightRayDirectionOffset;
    
    private RaycastHit hit;
    private bool isThereAnyThing = false;
    
    [SerializeField, Range(10.0f, 100.0f)]
    private float avoidanceSpeed;


    private Vector3 forwardVec;
 
    void Awake ()
    {
        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);
        
        this.transform.position = getRandomPos();
        AddGravityBody(this.gameObject);
        
        StartCoroutine(NewHeading(directionChangeInterval));
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

    void Update()
    {
        //Look At Somthly Towards the Target OR wander if there is nothing in front.
        if (isThereAnyThing == false )
        {
            if (followPlayer)
            {
                Follow(player);
            }
            else
            {
                Wander();
            }

        }
        else
        {
            forwardVec = transform.forward;
            Move();
        }

        if (obstacleAvoidance)
        {
            //Checking for any Obstacle in front.
            // Two rays left and right to the object to detect the obstacle.
            Transform leftRay = transform;
            Transform rightRay = transform;

            //Use Physics.RayCast to detect the obstacle
            if (Physics.Raycast(leftRay.position + (transform.right * forwardRayDirectionOffset), transform.forward, out hit, forwardRayDistance)
            || Physics.Raycast(rightRay.position - (transform.right * forwardRayDirectionOffset), transform.forward, out hit, forwardRayDistance))
            {
                if (hit.collider.gameObject.CompareTag("CubeObstacles"))
                {
                    isThereAnyThing = true;
                    transform.Rotate(Vector3.up * Time.deltaTime * avoidanceSpeed);
                }
            }

            // Now Two More RayCast At The End of Object to detect that object has already pass the obstacle.
            // Just making this boolean variable false it means there is nothing in front of object.
            if (Physics.Raycast(transform.position - (transform.forward * leftRightRayDirectionOffset), transform.right, out hit, leftRightRayDistance) ||
                Physics.Raycast(transform.position - (transform.forward * leftRightRayDirectionOffset), -transform.right, out hit, leftRightRayDistance))
            {
                if (hit.collider.gameObject.CompareTag("CubeObstacles"))
                {
                    isThereAnyThing = false;
                }
            }

            if (debugAvoidance)
            {
                // Use to debug the Physics.RayCast.
                Debug.DrawRay(transform.position + (transform.right * forwardRayDirectionOffset), transform.forward * forwardRayDistance, Color.red);
                Debug.DrawRay(transform.position - (transform.right * forwardRayDirectionOffset), transform.forward * forwardRayDistance, Color.red);
                Debug.DrawRay(transform.position - (transform.forward * leftRightRayDirectionOffset), -transform.right * leftRightRayDistance, Color.yellow);
                Debug.DrawRay(transform.position - (transform.forward * leftRightRayDirectionOffset), transform.right * leftRightRayDistance, Color.yellow);
            }
            
        }else{
            isThereAnyThing = false;
        }
    }
    
    void Follow(GameObject target){

        float range = maxDist;
        float stop = minDist;
        
        //rotate to look at the player
        var distance = Vector3.Distance(this.transform.position, target.transform.position);

        if (distance <= range)
        {
            //rotate to look at the player
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion look = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(this.transform.rotation, look, rotationSpeed * Time.deltaTime);
            forwardVec = transform.forward;
            //move
            if (distance > stop)
            {
                Move();
            }
        }
         
         
    }
    
    void Wander(){
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        forwardVec = forward;
        Move();
    }
    
    void Move(){
        // move in forward direction.
        //transform.Translate(forward * moveSpeed * Time.deltaTime);
		transform.position += forwardVec * moveSpeed * Time.deltaTime;
    }
    
 
    IEnumerator NewHeading (float waitTime)
    {
        while (true) {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
 
    /// Calculates a new direction to move towards.
    void NewHeadingRoutine ()
    {
        var floor = Mathf.Clamp(heading - maxHeadingChange, 0, 360);
        var ceil  = Mathf.Clamp(heading + maxHeadingChange, 0, 360);
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }
}
