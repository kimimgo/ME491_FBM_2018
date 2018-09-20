using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRectangle : MonoBehaviour {
    bool drawStart = false;

    public Vector3 startVertex;
    public Vector3 curVertex;
    int numLine = 4;
    LineRenderer lr;
    Vector3[] lineVertices;
    Camera cam;
    
	// Use this for initialization
	void Start () {
        lr = gameObject.AddComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Additive"));
        lr.positionCount = numLine;
        lr.startWidth = 0.005f;
        lr.endWidth = 0.005f;
        lr.loop = true;

        lineVertices = new Vector3[numLine];

        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0) && !drawStart)
        {
            drawStart = true;
            Vector2 mousePos = Input.mousePosition;
            startVertex = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            lineVertices[0] = startVertex;
        }
        if (Input.GetMouseButton(0) && drawStart)
        {
            Vector2 mousePos = Input.mousePosition;
            curVertex = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
            lineVertices[1] = new Vector3(startVertex.x,curVertex.y, curVertex.z);
            lineVertices[2] = new Vector3(curVertex.x, curVertex.y, curVertex.z);
            lineVertices[3] = new Vector3(curVertex.x, startVertex.y, curVertex.z);
            //lineVertices[4] = startVertex;

            lr.SetPositions(lineVertices);
        }
        if(Input.GetMouseButtonUp(0))
        {
            drawStart = false;
        }

        Debug.Log(startVertex);
    }
}
