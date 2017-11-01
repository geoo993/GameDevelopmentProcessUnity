using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDeformerObject : MonoBehaviour {

    public float force = 10f;
    [Range(0.0f,2.0f)]public float forceOffset = 0.1f;
    
    [SerializeField, Range(5.0f, 50.0f)] float rayDistance = 10.0f;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        AddDeformerTo(gameObject, rayDistance);
	}
    
    void AddDeformerTo(GameObject obj, float rayRange){
        Vector3 downVec = (-obj.transform.up);
        float rayDirectionOffsetFromObject = 0.1f;
        RaycastHit hit;
        
        if ( Physics.Raycast( obj.transform.position + (downVec * rayDirectionOffsetFromObject), downVec, out hit, rayRange) )
        {
            
            // Use to debug the Physics.RayCast.
            //Debug.DrawRay(obj.transform.position + (downVec * rayDirectionOffsetFromObject), downVec * rayRange, Color.red);
            
            MeshDeformation deformer = hit.collider.GetComponent<MeshDeformation>();

            //Debug.Log("object: "+ hit.collider.gameObject.name);

            if (deformer) 
            {
                //Debug.Log("hit at point: "+ hit.point);
                Vector3 point = hit.point;
                point += hit.normal * forceOffset;
                deformer.AddDeformingForce(point, force);
            }

        }
    }
}
