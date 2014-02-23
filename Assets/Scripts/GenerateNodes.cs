using UnityEngine;
using System.Collections;

public class GenerateNodes : MonoBehaviour {
    public int NodesX;
    public int NodesY;

    private GameObject startPoint;
    private GameObject endPoint;

    public GameObject node;



    private void CreateNodes()
    {
        for (int i = 0; i < NodesX; i++)
        {
            for (int j = 0; j < NodesX; j++)
            {
                Vector3 newPosition = new Vector3(i / 3f, j / 3f, 0f);
                GameObject newNode = (GameObject)Instantiate(node, newPosition, transform.rotation);
                newNode.renderer.material.color = Color.white;
                newNode.transform.parent = transform;
            }
        }

        float MaxX = NodesX / 3f;
        float MaxY = NodesY / 3f;
        // 50 = 10
        // 100 = 18
        float scalingFactor = (NodesX - 50f) / 6.25f;

        Camera.main.transform.position = new Vector3(MaxX / 2f, MaxY / 2f, -1f);
        Camera.main.orthographicSize += scalingFactor;
    }

    private void GenerateWalls()
    {
        int TotalNodes = NodesX * NodesY;

    }
	// Use this for initialization
	void Start () 
    {
        CreateNodes();
        GenerateWalls();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}


    public void setStart(GameObject startNode)
    {
        // if there was already a start point
        if (startPoint != null)
        {
            startPoint.renderer.material.color = Color.white;
        }

        startPoint = startNode;
        startPoint.renderer.material.color = Color.blue;
    }

    public void setEnd(GameObject endNode)
    {
        // if there was already an end point
        if (endPoint != null)
        {
            endPoint.renderer.material.color = Color.white;
        }

        endPoint = endNode;
        endPoint.renderer.material.color = Color.red;

    }
}
