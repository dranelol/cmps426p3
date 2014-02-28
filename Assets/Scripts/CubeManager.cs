using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeManager : MonoBehaviour 
{

    public List<GameObject> cubes;

    private List<GameObject> collisions;

	// Use this for initialization
	void Start () 
    {
        collisions = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        collisions.Clear();
        
        foreach (GameObject item1 in cubes)
        {
            foreach (GameObject item2 in cubes)
            {
                if(item1.GetInstanceID() != item2.GetInstanceID())
                {                
                   if (isColliding(item1, item2))
                   {
                        
                        if(collisions.Contains(item1) == false)
                            collisions.Add(item1);
                   }

                    
                }
                
            }
        }

        //Debug.Log(collisions.Count.ToString());

        foreach (GameObject item in cubes)
        {
            if (collisions.Contains(item))
            {
                item.renderer.material.color = Color.black;
            }
            else
            {
                item.renderer.material.color = Color.white;
            }
        }

	}

    bool isColliding(GameObject a, GameObject b)
    {

        /*
         if (a.renderer.bounds.max.x < b.renderer.bounds.min.x ||
            a.renderer.bounds.max.y < b.renderer.bounds.min.y ||
            a.renderer.bounds.min.x > b.renderer.bounds.max.x ||
            a.renderer.bounds.min.y > b.renderer.bounds.max.y ||
            a.renderer.bounds.max.z < b.renderer.bounds.min.z ||
             a.renderer.bounds.min.z > b.renderer.bounds.max.z)
        {
            return false;
        }
        
        return true; */

        if (a.renderer.bounds.max.x < b.renderer.bounds.min.x)
        {
            return false;
        }
        if (a.renderer.bounds.max.y < b.renderer.bounds.min.y)
        {
            return false;
        }
        if (a.renderer.bounds.min.x > b.renderer.bounds.max.x)
        {
            return false;
        }
        if (a.renderer.bounds.min.y > b.renderer.bounds.max.y)
        {
            return false;
        }
        if (a.renderer.bounds.max.z < b.renderer.bounds.min.z)
        {
            return false;
        }
        if (a.renderer.bounds.min.z > b.renderer.bounds.max.z)
        {
            return false;
        }
        return true;
            
        
    }
}
