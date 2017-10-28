using UnityEngine;
using System.Collections;

// https://github.com/mattatz/unity-mesh-smoothing

[RequireComponent (typeof(MeshRenderer))]
[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshSmoothing))]
[RequireComponent (typeof(VertexConnection))]
public class MeshSmoothingDemo : MonoBehaviour {

	[System.Serializable] 
	enum FilterType {
		Laplacian, HC
	};

    

    [SerializeField, Range(0f, 1f)] float intensity = 0.5f;
    [SerializeField] FilterType type;
    [SerializeField, Range(0, 20)] int times = 3;
    [SerializeField, Range(0f, 1f)] float hcAlpha = 0.5f;
    [SerializeField, Range(0f, 1f)] float hcBeta = 0.5f;
    
  
	MeshFilter _filter;
	MeshFilter meshFilter {
		get {
			if(_filter == null) {
				_filter = GetComponent<MeshFilter>();
			}
			return _filter;
		}
		set{
			_filter = value;
		}
	}
    
    MeshRenderer _meshRenderer;
    MeshRenderer meshRenderer {
        get {
            if(_meshRenderer == null) {
                _meshRenderer = GetComponent<MeshRenderer>();
            }
            return _meshRenderer;
        }
        set{
            _meshRenderer = value;
        }
    }
    private MeshCollider meshCollider; 
	private MeshSmoothing meshSmoothing;
	private VertexConnection vertexConnection;
    private Mesh mesh;
    
    private Vector3[] vertices;
	private int[] triangles;
    private Vector3[] normals;
    private Vector2[] uv;

     void Start () {
        vertexConnection = GetComponent<VertexConnection>();
		meshSmoothing = GetComponent<MeshSmoothing>();
        meshSmoothing.SetVertexConnection(vertexConnection);
    
        //mesh = meshFilter.sharedMesh;
        
        mesh = meshFilter.mesh;
        mesh = ApplyNormalNoise(mesh);

        switch(type) {
        case FilterType.Laplacian:
            mesh = meshSmoothing.LaplacianFilter(mesh, times);
            break;
        case FilterType.HC:
            mesh = meshSmoothing.HCFilter(mesh, times, hcAlpha, hcBeta);
            break;
        }
       
    }
    
    Mesh ApplyNormalNoise (Mesh m) {

        vertices = mesh.vertices;
        normals = mesh.normals;

        for(int i = 0, n = m.vertexCount; i < n; i++) {
            vertices[i] = vertices[i] + normals[i] * Random.value * intensity;
        }
        
        mesh.vertices = vertices;

        return mesh;
    }
    
    
    /*
    void Update(){

        meshRenderer = GetComponent<MeshRenderer>();

        meshFilter = GetComponent<MeshFilter>();
        
        if (meshFilter==null){
            Debug.LogError("MeshFilter not found!");
            return;
        }

        mesh = meshFilter.sharedMesh;
        //mesh = meshFilter.mesh;
        if (mesh == null){
            meshFilter.mesh = new Mesh();
            mesh = meshFilter.sharedMesh;
            //mesh = meshFilter.mesh;
        }
        mesh.name = "Planet";
        
        FetchMeshData(mesh);
        
        //mesh.Clear();

        CalculateVertices(mesh);
        
		SetMeshData(mesh);
        
        switch(type) {
        case FilterType.Laplacian:
            mesh = meshSmoothing.LaplacianFilter(mesh, times);
            break;
        case FilterType.HC:
            mesh = meshSmoothing.HCFilter(mesh, times, hcAlpha, hcBeta);
            break;
        }
        
        //meshCollider = gameObject.AddComponent<MeshCollider> ();
        //meshCollider.isTrigger = false;
    
        meshRenderer.material.color = Color.blue;
	}
    
    
    void FetchMeshData(Mesh m){
        vertices = m.vertices;
		triangles = m.triangles;
        normals = m.normals;
        uv = m.uv;
    }
    
    void SetMeshData(Mesh m){
        m.vertices = vertices;
        m.triangles = triangles;
		m.normals = normals;
        m.uv = uv;
    }
    
    void CalculateVertices(Mesh m){
        for(int i = 0, n = m.vertexCount; i < n; i++) {
            vertices[i] = vertices[i] + normals[i] * Random.value * intensity;
        }
    }
    */

}


