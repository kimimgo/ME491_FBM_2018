using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour {

    // Made by Imgyu Kim : kimimgoo@kaist.ac.kr
    // 2018 ME491 FBS class

    MeshFilter filter;
    new MeshRenderer renderer;

    public MeshFilter Filter
    {
        get
        {
            if (filter == null)
            {
                filter = GetComponent<MeshFilter>();
            }
            return filter;
        }
    }

    public MeshRenderer Renderer
    {
        get
        {
            if (renderer == null)
            {
                renderer = GetComponent<MeshRenderer>();
            }
            return renderer;
        }
    }

    float size = 1.0f;

	// Use this for initialization
	void Start () {
        Filter.sharedMesh = Build();

    }

    // Update is called once per frame
    void Update () {
		
	}

    Mesh Build()
    {
        var mesh = new Mesh();

        var hsize = size * 0.5f;

        var vertices = new Vector3[] {
                new Vector3(-hsize,  hsize, 0f),
                new Vector3( hsize,  hsize, 0f),
                new Vector3( hsize, -hsize, 0f),
                new Vector3(-hsize, -hsize, 0f)
                };

        var uv = new Vector2[] {
                new Vector2(0f, 0f), 
				new Vector2(1f, 0f), 
				new Vector2(1f, 1f), 
				new Vector2(0f, 1f)  
			};

        var normals = new Vector3[] {
                new Vector3(0f, 0f, -1f), 
				new Vector3(0f, 0f, -1f), 
				new Vector3(0f, 0f, -1f), 
				new Vector3(0f, 0f, -1f)  
			};

        var triangles = new int[] {
                0, 1, 2, 
				2, 3, 0  
			};

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.normals = normals;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();

        return mesh;
    }
}
