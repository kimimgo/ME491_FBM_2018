using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMesh : MonoBehaviour {
    public Material mtl;

	// Use this for initialization
	void Start () {
        int nvertex = 4;
        float extrude_length = 5.0f;
        Vector2[] pos = new Vector2[nvertex];

        pos[0] = new Vector2(-1, -1);
        pos[1] = new Vector2(1, -1);
        pos[2] = new Vector2(1, 1);
        pos[3] = new Vector2(-1, 1);

        // Use triangulator to get indices for creating triangles
        Triangulator tr = new Triangulator(pos);
        int[] indices = tr.Triangulate();

        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[pos.Length];
        for (int j = 0; j < vertices.Length; j++)
        {
            vertices[j] = new Vector3(pos[j].x, 0, pos[j].y);
        }

        // Create the mesh
        Mesh msh = new Mesh();
        msh.vertices = vertices;
        msh.triangles = indices;
        msh.RecalculateNormals();
        msh.RecalculateBounds();

        float[] length = new float[vertices.Length];
        float net_length = 0;
        for (int j = 0; j < vertices.Length; j++)
        {
            if (j < vertices.Length - 1)
            {
                length[j] = (vertices[j] - vertices[j + 1]).magnitude;
                net_length += length[j];
            }
            else
            {
                length[j] = (vertices[j] - vertices[0]).magnitude;
                net_length += length[j];
            }
        }


        Vector3[] tvertices = msh.vertices;
        Vector2[] uvs = new Vector2[tvertices.Length];
        float cummulated_length = 0;
        for (int j = 0; j < uvs.Length; j++)
        {
            //uvs[j] = new Vector2 (vertices[j].x, vertices[j].z);

            uvs[j] = new Vector2(cummulated_length / net_length, 0);
            cummulated_length += length[j];

        }
        msh.uv = uvs;

        Matrix4x4[] finalSections = new Matrix4x4[2];
        finalSections[0] = Matrix4x4.identity;
        finalSections[1] = Matrix4x4.TRS(new Vector3(0, extrude_length, 0), Quaternion.identity, Vector3.one);
        MeshExtrusion.Edge[] precomputedEdges = MeshExtrusion.BuildManifoldEdges(msh);
        MeshExtrusion.ExtrudeMesh(msh, msh, finalSections, precomputedEdges, true);


        // Set up game object with mesh;
        GameObject extruded_object = new GameObject();
        MeshFilter filter = extruded_object.AddComponent<MeshFilter>();
        filter.mesh = msh;
        MeshRenderer mr = extruded_object.AddComponent<MeshRenderer>();
        mr.material = mtl;

        MeshCollider mc = extruded_object.AddComponent<MeshCollider>();
        //mc.sharedMesh = null;
        mc.sharedMesh = msh;

    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
