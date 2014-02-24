using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeColorHandler : MonoBehaviour {
    public List<GameObject> openList1;
    public List<GameObject> closedList1;
    public List<GameObject> seenList1;

    public List<GameObject> openList2;
    public List<GameObject> closedList2;
    public List<GameObject> seenList2;


    private List<GameObject> masterOpenList;
    private List<GameObject> masterClosedList;
    private List<GameObject> masterSeenList;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void NewPath(int AI, List<GameObject> closedList, List<GameObject> openList, List<GameObject> seenList)
    {
        masterSeenList.Clear();
        masterOpenList.Clear();
        masterClosedList.Clear();
        if (AI == 1)
        {
            // reset all from 1
            foreach (GameObject node in closedList1)
            {
                node.renderer.material.color = Color.white;
            }

            foreach (GameObject node in openList1)
            {
                node.renderer.material.color = Color.white;
            }

            foreach (GameObject node in seenList1)
            {
                node.renderer.material.color = Color.white;
            }
            // take current lists from 2, combine them with new 1

            masterClosedList.AddRange(closedList2);
            masterClosedList.AddRange(closedList);

            masterOpenList.AddRange(openList2);
            masterOpenList.AddRange(openList);

            masterSeenList.AddRange(seenList2);
            masterSeenList.AddRange(seenList);
        }

        else
        {
            // reset all from 2
            foreach (GameObject node in closedList2)
            {
                node.renderer.material.color = Color.white;
            }

            foreach (GameObject node in openList2)
            {
                node.renderer.material.color = Color.white;
            }

            foreach (GameObject node in seenList2)
            {
                node.renderer.material.color = Color.white;
            }
            // take current lists from 1, combine them with new 2

            masterClosedList.AddRange(closedList1);
            masterClosedList.AddRange(closedList);

            masterOpenList.AddRange(openList1);
            masterOpenList.AddRange(openList);

            masterSeenList.AddRange(seenList1);
            masterSeenList.AddRange(seenList);

        }

        // draw
        foreach (GameObject node in masterSeenList)
        {
            node.renderer.material.color = Color.grey;
        }

        foreach (GameObject node in masterOpenList)
        {
            node.renderer.material.color = Color.blue;
        }

        foreach (GameObject node in masterClosedList)
        {
            node.renderer.material.color = Color.yellow;
        }
    }
}
