using UnityEngine;
using System.Collections;

// https://www.youtube.com/watch?v=5B6ALcOX4b8


[RequireComponent(typeof(Rigidbody))]
public class HoverBoardControl : MonoBehaviour
{
    Rigidbody m_body{
      get {
          return GetComponent<Rigidbody>();
      }
    }
  
    private float m_deadZone = 0.1f;
    
    // hovering
	public float m_jumpForce = 100.0f;
    public float m_hoverForce = 9.0f;
    public float m_hoverHeight = 2.0f;
    public GameObject[] m_hoverPoints;
    
    // movement
    public float m_forwardAcl = 100.0f;
    public float m_backwardAcl = 25.0f;
    private float m_currThrust = 0.0f;
    
    public float m_turnStrength = 10f;
    private float m_currTurn = 0.0f;

    public bool enableAutoMove;
    public bool enableSwerve;
    
    private bool isGrounded;
    private bool isJumping;
    
    private int m_layerMask;

    void Start()
    {
        // 

        // helps us test hits betwenn anything except the character layer mask.
        /*  https://answers.unity.com/questions/8715/how-do-i-use-layermasks.html
            layermasks work by setting bits in an integer value to 0 (false) or 1 (true), 
            which represent whether or not to test against a certain layer. 
            The first bit from the right is used for layer 1, the second for layer 2, etc. 
            So a value of 00000101 (decimal value of 5), which has the first and third bit set to 1 (true), 
            will test ONLY against layers one and three.
            Using bit shifting to create a layermask
            The easiest way to produce a layermask is using the bit shift operator (<<). 
            What this does is move each bit in an value to the left. 
            So if you have a value of 6 (binary value of 00000110), 
            and shift it two places to the left, you will be given a value of 24 (binary 00011000).
            Remember that a value of 1 in decimal is 00000001 in binary. 
            So if you bit shift 1 by n places to the left, you will end up with 
            the nth bit being 1 and all other bits being 0. Based on this, 
            the following code will produce a layermask which can be used to only test against the given layer:
         
          */ 
          
        m_layerMask = 1 << LayerMask.NameToLayer("HoverCar"); // name of the layer
        m_layerMask = ~m_layerMask;

    }

    /*
    void OnDrawGizmos()
    {

        //  Hover Force
        RaycastHit hit;
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position,
                                -Vector3.up, out hit,
                                m_hoverHeight,
                                m_layerMask))
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
                Gizmos.DrawSphere(hit.point, 0.5f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(hoverPoint.transform.position,
                               hoverPoint.transform.position - Vector3.up * m_hoverHeight);
            }
        }
    }
    
    */
    
    void Update()
    {
        Movement();
    }
    
    void FixedUpdate()
    {
        ApplyTrust();
        
        //  Hover Force
        RaycastHit hit;
        for (int i = 0; i < m_hoverPoints.Length; i++)
        {
            // Here we say
            var hoverPoint = m_hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, // from our position 
                              -Vector3.up,                   // cast a ray down
                              out hit,                       // with the raycast  hit 
                              m_hoverHeight,                 // with a distance of hover height
                              m_layerMask))                  // ignoring the character layer
            {

                float hitDistance = (1.0f - (hit.distance / m_hoverHeight));
                float liftForce = m_hoverForce * hitDistance;

                // then if you hit something then add a force to push upward
                m_body.AddForceAtPosition(
                    Vector3.up * liftForce,                  // we add force upwards, by saying how far away was the collision from our source or when hit, and we push or add force based on how far away the collision was, so if collision hit was really close, we push hard and if it was far we push low
                    hoverPoint.transform.position);          // we add the force this position

                isGrounded = true;
                isJumping = false;
            }
            else
            {
                // this if for when the car rolls over or flips, this is the behaviour that helps us back in the upright position
                // so when we down get a collision and instead of just pushing down, we are going to say: 
                // based on whether you are higher or lower on the car, we will correct its position, 
                // and notice that we are applying these forces at the position of the wheels. 
                // so this is going to add a force to turn the vehicle for us
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    // this is when the car has flipped, so we need to correct its position to stay upright
                    ApplyForceLift(hoverPoint, m_hoverForce);
                }
                else
                {
                    ApplyForceLift(hoverPoint, -m_hoverForce);
                }

                isGrounded = false;
            }
            
            ApplyJumpForce(hoverPoint);
        }
            
        
        
        if (enableSwerve)
        {
            ApplyTorque();
        } else {
            m_currTurn = Input.GetAxis("Horizontal") * Time.deltaTime * (m_turnStrength * 0.1f) ; // direction
            transform.Rotate(0.0f, m_currTurn, 0.0f);
        }
        
    }

    void Movement(){
        CalculateTrust();
        CalculateYaw();
    }
    
    void CalculateTrust() {

        // Main Thrust
        m_currThrust = 0.0f;

        if (enableAutoMove)
        {
            m_currThrust = m_forwardAcl;
        }else {
        
            float aclAxis = Input.GetAxis("Vertical");
            if (aclAxis > m_deadZone)
            {
                m_currThrust = aclAxis * m_forwardAcl;
            }
            else if (aclAxis < -m_deadZone)
            {
                m_currThrust = aclAxis * m_backwardAcl;
            }
        }
        
    }

    void CalculateYaw() {
        // Turning
        m_currTurn = 0.0f;
        float turnAxis = Input.GetAxis("Horizontal");
        if (Mathf.Abs(turnAxis) > m_deadZone)
        {
            m_currTurn = turnAxis;
        }
    }
    
    void ApplyTrust() {
        // Forward
        if (Mathf.Abs(m_currThrust) > 0)
        {
            m_body.AddForce(transform.forward * m_currThrust);
        }
    }
    
    void ApplyTorque() {
        // Turn
        if (m_currTurn > 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength); // we rotate right when +m_currTurn
        } else if (m_currTurn < 0)
        {
            m_body.AddRelativeTorque(Vector3.up * m_currTurn * m_turnStrength); // we rotate left when -m_currTurn
        }
    }

    void ApplyForceLift(GameObject hoverPoint, float force) {
         m_body.AddForceAtPosition(
              hoverPoint.transform.up * force,
              hoverPoint.transform.position,
              ForceMode.Force);
    }
    
    void ApplyImpulseLift(GameObject hoverPoint, float force) {
         m_body.AddForceAtPosition(
              hoverPoint.transform.up * force,
              hoverPoint.transform.position,
              ForceMode.Impulse);
    }
    
    void ApplyJumpForce(GameObject hoverPoint){
    
         // Jump
        if(isGrounded == true && (Input.GetButtonDown("Jump") || Input.GetButton("Jump")) && isJumping == false){
            ApplyImpulseLift(hoverPoint, m_jumpForce);
            isJumping = true;
        }

    }

 
}
