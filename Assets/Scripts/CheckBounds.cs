using UnityEngine;
using System.Collections;

public class CheckBounds : MonoBehaviour 
{
    public float posBounds;
    public float resetDistance;

    private float negBounds;

	// Use this for initialization
	void Start () 
    {
        negBounds = posBounds * -1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (transform.position.x < negBounds || transform.position.x > posBounds || transform.position.y < negBounds || transform.position.y > posBounds || transform.position.z < negBounds || transform.position.z > posBounds)
        {
            if (transform.position.x < negBounds || transform.position.x > posBounds)
            {
                if (transform.position.x > 0)
                {
                    transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
                }
            }

            if (transform.position.y < negBounds || transform.position.y > posBounds)
            {
                if (transform.position.y > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
                }
            }

            if (transform.position.z < negBounds || transform.position.z > posBounds)
            {
                if (transform.position.z > 0)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
                }
            }

            rigidbody.velocity = rigidbody.velocity * -1;
        }

	}
}
