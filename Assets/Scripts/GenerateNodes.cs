using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateNodes : MonoBehaviour {
    public int NodesX;
    public int NodesY;

    private GameObject startPoint;
    private GameObject endPoint;

    public GameObject node;
    public GameObject AI;
    public GameObject player;

    public GameObject[,] nodeMap;
    private List<GameObject> pathObjects;
    private List<GameObject> openListObjects;
    private List<GameObject> seenListObjects;

    public Dictionary<Vector3, GameObject> nodeDict;

    public Pathfinder pathfinder;

    private void CreateNodes()
    {
        nodeMap = new GameObject[NodesX, NodesY];
        nodeDict = new Dictionary<Vector3, GameObject>();
        pathObjects = new List<GameObject>();
        openListObjects = new List<GameObject>();
        seenListObjects = new List<GameObject>();

        for (int i = 0; i < NodesX; i++)
        {
            for (int j = 0; j < NodesX; j++)
            {
                Vector3 newPosition = new Vector3(i, j, 0f);
                GameObject newNode = (GameObject)Instantiate(node, newPosition, transform.rotation);
                newNode.renderer.material.color = Color.white;
                newNode.transform.parent = transform;
                nodeMap[i, j] = newNode;

                nodeDict[newPosition] = newNode;
            }
        }

        float MaxX = NodesX;
        float MaxY = NodesY;
        // 18 = 10
        // 100 = 53
        float scalingFactor = (NodesX - 18f) / 1.907f;

        Camera.main.transform.position = new Vector3(MaxX / 2f, MaxY / 2f, Camera.main.transform.position.z);
        Camera.main.orthographicSize += scalingFactor;
        
    }

    private void GenerateWalls()
    {
        for (int i = 0; i < NodesX; i++)
        {
            for (int j = 0; j < NodesY; j++)
            {
                if(Random.Range(0,100) % 4 == 0)
                {
                    if( (i == 0 && j == 0) || (i==NodesX-1 && j == 0) || (i==NodesX-1 && j==NodesY-1))
                    {
                        // do nothing
                    }

                    else
                    {
                        nodeMap[i, j].renderer.material.color = Color.black;
                    }
                }
            }
        }


    }
    // Use this for initialization
    private void PlacePlayerAndAIs()
    {
        GameObject newAI1 = (GameObject)Instantiate(AI, new Vector3(NodesX-1, 0f, -1f), transform.rotation);
        GameObject newAI2 = (GameObject)Instantiate(AI, new Vector3(NodesX-1, NodesY-1, -1f), transform.rotation);

        newAI1.transform.parent = transform;
        newAI2.transform.parent = transform;

        newAI1.renderer.material.color = Color.red;
        newAI2.renderer.material.color = Color.red;

        newAI1.GetComponent<AIBehaviour>().AINum = 1;
        newAI2.GetComponent<AIBehaviour>().AINum = 2;
        newAI1.GetComponent<AIBehaviour>().otherAI = newAI2;
        newAI2.GetComponent<AIBehaviour>().otherAI = newAI1;

        GameObject newPlayer = (GameObject)Instantiate(player, new Vector3(0f, 0f, -1f), transform.rotation);
        newPlayer.transform.parent = transform;
        newPlayer.GetComponent<PlayerBehaviour>().AssignAIs(newAI1, newAI2);
    }
    
    
    void Start() 
    {
        CreateNodes();
        GenerateWalls();
        pathfinder = new Pathfinder(nodeMap);
        PlacePlayerAndAIs();
        

	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            // if start and end are set
            if (startPoint != null && endPoint != null)
            {
                // return path and openlist, if either are applicable

                Debug.Log("starting: " + startPoint.transform.position);
                Debug.Log("ending: " + endPoint.transform.position);
                
                List<List<Vector3>> path = pathfinder.FindOptimalPath(startPoint.transform.position, endPoint.transform.position);
                
                foreach (Vector3 node in path[2])
                {
                    Debug.Log(node.ToString());
                    if (nodeDict[node] != startPoint && nodeDict[node] != endPoint)
                    {
                        nodeDict[node].renderer.material.color = Color.grey;
                    }
                    seenListObjects.Add(nodeDict[node]);
                }

                foreach (Vector3 node in path[1])
                {
                    if (nodeDict[node] != startPoint && nodeDict[node] != endPoint)
                    {
                        nodeDict[node].renderer.material.color = Color.blue;
                    }
                    openListObjects.Add(nodeDict[node]); 
                }

                foreach (Vector3 node in path[0])
                {
                    nodeDict[node].renderer.material.color = Color.yellow;
                    pathObjects.Add(nodeDict[node]);
                }
                

                

                
            }
        }

        if (Input.GetKeyDown(KeyCode.Backspace) == true)
        {
            ResetAllNodes();
        }

        if (Input.GetKeyDown(KeyCode.F5) == true)
        {
            Application.LoadLevel(0);
        }
	    
	}


    public void setStart(GameObject startNode)
    {
        // if there was already a start point
        if (startPoint != null)
        {
            startPoint.renderer.material.color = Color.white;
        }

        startPoint = startNode;
        startPoint.renderer.material.color = Color.green;
        Debug.Log("setting start: " + startPoint.transform.position);
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
        Debug.Log("setting end: " + endPoint.transform.position);

    }

    public void ResetAllNodes()
    {
        if (startPoint != null)
        {
            startPoint.renderer.material.color = Color.white;
        }

        if (endPoint != null)
        {
            endPoint.renderer.material.color = Color.white;
        }

        if (pathObjects.Count > 0)
        {
            foreach (GameObject node in pathObjects)
            {
                node.renderer.material.color = Color.white;
            }
        }

        if (openListObjects.Count > 0)
        {
            foreach (GameObject node in openListObjects)
            {
                node.renderer.material.color = Color.white;
            }
        }

        if (seenListObjects.Count > 0)
        {
            foreach (GameObject node in seenListObjects)
            {
                node.renderer.material.color = Color.white;
            }
        }

        startPoint = null;
        endPoint = null;
        pathObjects.Clear();
        openListObjects.Clear();
        seenListObjects.Clear();
    }
}
