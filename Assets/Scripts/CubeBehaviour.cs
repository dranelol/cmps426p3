using UnityEngine;
using System.Collections;

public class CubeBehaviour : MonoBehaviour {

	// Use this for initialization
    GenerateNodes parentGeneration;

	void Start () 
    {
        parentGeneration = transform.parent.GetComponent<GenerateNodes>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnMouseOver()
    {
        Debug.Log("asd");
        // left click, try to set start point
        if (Input.GetMouseButtonDown(0))
        {
            // if im not a wall
            if (renderer.material.color != Color.black)
            {
                parentGeneration.setStart(transform.gameObject);
            }
        }

        //right click, try to set end point
        if (Input.GetMouseButtonDown(1))
        {
            // if im not a wall
            if (renderer.material.color != Color.black)
            {
                parentGeneration.setEnd(transform.gameObject);
            }
        }
    }
}
