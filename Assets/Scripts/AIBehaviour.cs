﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviour : MonoBehaviour {
    public float RepathTime = 4.0f;
    public float MoveTimer = 0.8f;
    public MazeColorHandler colorHandler;
    private float repathTime;
    private float moveTimer;

    private List<Vector3> openList;
    private List<Vector3> closedList;
    private List<Vector3> seenList;
    private Dictionary<Vector3, GameObject> objectDict;

    private Pathfinder pathfinder;

    public GameObject player;
    public GameObject otherAI;
    public GameObject stateSpace;

    int pathCounter = 0;

    public int AINum;

	void Start () 
    {
        colorHandler = stateSpace.GetComponent<MazeColorHandler>();
        repathTime = RepathTime;
        moveTimer = MoveTimer;
        RepathTime = 0.0f;

        pathfinder = new Pathfinder(transform.parent.GetComponent<GenerateNodes>().nodeMap);

        objectDict = transform.parent.GetComponent<GenerateNodes>().nodeDict;

        closedList = new List<Vector3>();
        openList = new List<Vector3>();
        seenList = new List<Vector3>();
        
	}
	
	void Update () 
    {
        if (MoveTimer <= 0.0f)
        {
            if (RepathTime <= 0.0f)
            {
                // choose new path
                
                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                }

                List<List<Vector3>> newPath = pathfinder.FindOptimalPath(transform.position, player.transform.position);


                closedList = new List<Vector3>();
                openList = new List<Vector3>();
                seenList = new List<Vector3>();

                closedList = newPath[0];
                openList = newPath[1];
                seenList = newPath[2];

                RepathTime = repathTime;

                //Debug.Log("AI: " + AINum + " finding new path");
                //Debug.Log(closedList.ToString());

                List<GameObject> closedObjects = new List<GameObject>();
                List<GameObject> openObjects = new List<GameObject>();
                List<GameObject> seenObjects = new List<GameObject>();

                foreach (Vector3 item1 in closedList)
                {
                    closedObjects.Add(objectDict[item1]);
                }

                foreach (Vector3 item2 in openList)
                {
                    openObjects.Add(objectDict[item2]);
                }

                foreach (Vector3 item3 in seenList)
                {
                    seenObjects.Add(objectDict[item3]);
                }

                pathCounter = 0;

                //Debug.Log("closed: " + closedObjects.Count.ToString());
                //Debug.Log("open: " + openObjects.Count.ToString());
                //Debug.Log("seen: " + seenObjects.Count.ToString());

                colorHandler.NewPath(AINum, closedObjects, openObjects, seenObjects);
            }

            else
            {
                RepathTime -= Time.deltaTime;
            }

            // move if possible
            if (closedList.Count > 0)
            {

                Vector3 newPosition = new Vector3(closedList[0].x, closedList[0].y, transform.position.z);
                Vector3 playerPositionNormalized = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
                if (newPosition != playerPositionNormalized && newPosition != otherAI.transform.position)
                {
                    closedList.RemoveAt(0);
                    transform.position = newPosition;
                    MoveTimer = moveTimer;
                    pathCounter++;
                }
            }

            
        }

        else
        {
            MoveTimer -= Time.deltaTime;
        }
	}
}
