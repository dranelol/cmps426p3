using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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

        masterSeenList = new List<GameObject>();
        masterOpenList = new List<GameObject>();
        masterClosedList = new List<GameObject>();

        if (AI == 1)
        {
            if (closedList1.Count > 0)
            {
                foreach (GameObject node in closedList1)
                {
                    if (node != null)
                    {
                        node.renderer.material.color = Color.white;
                    }
                }
                
            }
             

            if (openList1.Count > 0)
            {
                foreach (GameObject node in openList1)
                {
                    if (node != null)
                    {
                        node.renderer.material.color = Color.white;
                    }
                }
            }

            if (seenList1.Count > 0)
            {
                foreach (GameObject node in seenList1)
                {
                    if (node != null)
                    {
                        node.renderer.material.color = Color.white;
                    }
                }
            }

            

            closedList1 = new List<GameObject>();
            openList1 = new List<GameObject>();
            seenList1 = new List<GameObject>();

            closedList1.AddRange(closedList);
            openList1.AddRange(openList);
            seenList1.AddRange(seenList);
            
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

            
            if (closedList2.Count > 0)
            {
                
                foreach (GameObject node in closedList2)
                {
                    if (node != null)
                    {
                        node.renderer.material.color = Color.white;
                    }
                }
                
            }
           

            if (openList2.Count > 0)
            {
                foreach (GameObject node in openList2)
                {
                    if (node != null)
                    {
                        node.renderer.material.color = Color.white;
                    }
                    //Debug.Log(node.ToString());
                }
            }

            if (seenList2.Count > 0)
            {
                foreach (GameObject node in seenList2)
                {
                    if (node != null)
                    {
                        node.renderer.material.color = Color.white;
                    }
                    //Debug.Log(node.ToString());
                }
            }

            closedList2 = new List<GameObject>();
            openList2 = new List<GameObject>();
            seenList2 = new List<GameObject>();

            //closedList2.Concat<GameObject>(closedList);
            
            closedList2.AddRange(closedList);
            openList2.AddRange(openList);
            seenList2.AddRange(seenList);

            // take current lists from 1, combine them with new 2

            masterClosedList.AddRange(closedList1);
            masterClosedList.AddRange(closedList);

            masterOpenList.AddRange(openList1);
            masterOpenList.AddRange(openList);

            masterSeenList.AddRange(seenList1);
            masterSeenList.AddRange(seenList);

        }

        //Debug.Log(masterClosedList.Count);
        //Debug.Log(masterOpenList.Count);
        //Debug.Log(masterSeenList.Count);

        // draw
        foreach (GameObject node in masterSeenList)
        {
            if (node != null)
            {
                node.renderer.material.color = Color.grey;
            }
            
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
