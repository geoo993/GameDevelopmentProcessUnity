using UnityEngine;
using System.Collections;

namespace mattatz.MeshSmoothingSystem.Demo {

	[RequireComponent (typeof(MeshRenderer))]
	[RequireComponent (typeof(MeshFilter))]
	public class Demo : MonoBehaviour {

		[System.Serializable] 
		enum FilterType {
			Laplacian, HC
		};

        /*
		MeshFilter _filter;
        MeshFilter filter {
            get {
                if(_filter == null) {
                    _filter = GetComponent<MeshFilter>();
                }
                return _filter;
            }
        }
        */
        
        private MeshFilter filter;
        private Mesh mesh;
        private Renderer rend;

		[SerializeField, Range(0f, 1f)] float intensity = 0.5f;
		[SerializeField] FilterType type;
		[SerializeField, Range(0, 20)] int times = 3;
		[SerializeField, Range(0f, 1f)] float hcAlpha = 0.5f;
		[SerializeField, Range(0f, 1f)] float hcBeta = 0.5f;
        
        private Vector3[] vertices;
		private int[] triangles;
        private Vector3[] normals;
        private Vector2[] uv;

        void Start()
        {

            rend = GetComponent<MeshRenderer>();

            filter = GetComponent<MeshFilter>();
            
            if (filter==null){
                Debug.LogError("MeshFilter not found!");
                return;
            }
    
            mesh = filter.sharedMesh;
            mesh = filter.mesh;
            if (mesh == null){
                filter.mesh = new Mesh();
                mesh = filter.sharedMesh;
                //mesh = meshFilter.mesh;
            }
            FetchMeshData(mesh);
            
            //mesh.Clear();

            CalculateVertices(mesh);
            
            SetMeshData(mesh);
            
			switch(type) {
			case FilterType.Laplacian:
				filter.mesh = MeshSmoothing.LaplacianFilter(filter.mesh, times);
				break;
			case FilterType.HC:
				filter.mesh = MeshSmoothing.HCFilter(filter.mesh, times, hcAlpha, hcBeta);
				break;
			}
            
            rend.material.color = Color.blue;
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
      

	}

}

