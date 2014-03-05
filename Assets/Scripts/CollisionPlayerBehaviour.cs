using UnityEngine;
using System.Collections;

public class CollisionPlayerBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    // handle x,z movement
        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal"), transform.position.y, transform.position.z + Input.GetAxis("Vertical"));


        // handle y movement

        if (Input.GetKey(KeyCode.Q) == true)
        {
            transform.Translate(Vector3.up * -1);
        }

        if (Input.GetKey(KeyCode.E) == true)
        {
            transform.Translate(Vector3.up);
        }
	}
}
