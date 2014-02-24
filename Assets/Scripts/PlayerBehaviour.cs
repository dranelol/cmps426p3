using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {
    public GameObject AI1;
    public GameObject AI2;

    public GenerateNodes parentBehaviour;

	// Use this for initialization
	void Start () 
    {
        parentBehaviour = transform.parent.GetComponent<GenerateNodes>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.W) == true)
        {
            // move up
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            if (newPosition.y != parentBehaviour.NodesY)
            {
                if (parentBehaviour.nodeMap[(int)transform.position.x, (int)transform.position.y + 1].renderer.material.color != Color.black)
                {
                    if (newPosition != AI1.transform.position && newPosition != AI2.transform.position)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                    }

                }
            }


        }

        if (Input.GetKeyDown(KeyCode.S) == true)
        {
            // move down
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            if (newPosition.y != -1)
            {
                if (parentBehaviour.nodeMap[(int)transform.position.x, (int)transform.position.y - 1].renderer.material.color != Color.black)
                {
                    if (newPosition != AI1.transform.position && newPosition != AI2.transform.position)
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            // move left

            Vector3 newPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            if (newPosition.x != -1)
            {
                if (parentBehaviour.nodeMap[(int)transform.position.x - 1, (int)transform.position.y].renderer.material.color != Color.black)
                {


                    if (newPosition != AI1.transform.position && newPosition != AI2.transform.position)
                    {
                        transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                    }
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.D) == true)
        {
            // move right
            Vector3 newPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            if (newPosition.x != parentBehaviour.NodesX)
            {
                if (parentBehaviour.nodeMap[(int)transform.position.x + 1, (int)transform.position.y].renderer.material.color != Color.black)
                {


                    if (newPosition != AI1.transform.position && newPosition != AI2.transform.position)
                    {
                        transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                    }

                }
            }
        }
	}

    public void AssignAIs(GameObject ai1, GameObject ai2)
    {
        AI1 = ai1;
        AI2 = ai2;
    }
}
