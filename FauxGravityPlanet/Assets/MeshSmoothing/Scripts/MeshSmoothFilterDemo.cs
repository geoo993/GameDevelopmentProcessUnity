using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  http://wiki.unity3d.com/index.php?title=MeshSmoother
    Apply to any meshed gameobject for smoothing.
 
        Works also by replacing MeshFilter with SkinnedMeshRenderer and use sharedMesh
 
    At present tests Laplacian Smooth Filter and HC Reduced Shrinkage Variant Filter
*/
[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshFilter))]
public class MeshSmoothFilterDemo : MonoBehaviour 
{
 
    private Mesh sourceMesh;
    private Mesh workingMesh;
    
    [SerializeField, Range(0, 20)] int iterations = 3;
    [SerializeField, Range(0f, 1f)] float hcAlpha = 0.5f;
    [SerializeField, Range(0f, 1f)] float hcBeta = 0.5f;
 
    void Start () 
    {
        MeshFilter meshfilter = GetComponent<MeshFilter>();
       
        // Clone the cloth mesh to work on
        sourceMesh = new Mesh();
        // Get the sourceMesh from the originalSkinnedMesh
        sourceMesh = meshfilter.mesh;
        // Clone the sourceMesh 
        workingMesh = CloneMesh(sourceMesh);
        // Reference workingMesh to see deformations
        meshfilter.mesh = workingMesh;


        // Apply Laplacian Smoothing Filter to Mesh
        for (int i = 0; i < iterations; i++)
        {
            workingMesh.vertices = MeshSmoothFilter.laplacianFilter(workingMesh.vertices, workingMesh.triangles);
            //workingMesh.vertices = MeshSmoothFilter.hcFilter(sourceMesh.vertices, workingMesh.vertices, workingMesh.triangles, hcAlpha, hcBeta);
        }
    }
 
    // Clone a mesh
    private static Mesh CloneMesh(Mesh mesh)
    {
        Mesh clone = new Mesh();
        clone.vertices = mesh.vertices;
        clone.normals = mesh.normals;
        clone.tangents = mesh.tangents;
        clone.triangles = mesh.triangles;
        clone.uv = mesh.uv;
        clone.uv2 = mesh.uv2;
		clone.uv3 = mesh.uv3;
        clone.uv4 = mesh.uv4;
        clone.bindposes = mesh.bindposes;
        clone.boneWeights = mesh.boneWeights;
        clone.bounds = mesh.bounds;
        clone.colors = mesh.colors;
		clone.colors32 = mesh.colors32;
		clone.subMeshCount = mesh.subMeshCount;
        clone.name = mesh.name;
        
        //TODO : Are we missing anything?
        return clone;
    }
}