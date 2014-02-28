﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct floatToGameObject
{
    public float f;
    public bool isStart;
    public GameObject item;

    public floatToGameObject(float fl, bool start, GameObject newItem)
    {
        this.f = fl;
        this.isStart = start;
        this.item = newItem;
    }
};

public class SSCubeManager : MonoBehaviour
{

    public List<GameObject> cubes;

    private List<GameObject> collisionsX;
    private List<GameObject> collisionsY;
    private List<GameObject> collisionsZ;

    private List<GameObject> activeCollisionsX;
    private List<GameObject> activeCollisionsY;
    private List<GameObject> activeCollisionsZ;

    private List<floatToGameObject> xlist;
    private List<floatToGameObject> ylist;
    private List<floatToGameObject> zlist;

    

    // Use this for initialization
    void Start()
    {
        collisionsX = new List<GameObject>();
        collisionsY = new List<GameObject>();
        collisionsZ = new List<GameObject>();

        activeCollisionsX = new List<GameObject>();
        activeCollisionsY = new List<GameObject>();
        activeCollisionsZ = new List<GameObject>();

        xlist = new List<floatToGameObject>();
        ylist = new List<floatToGameObject>();
        zlist = new List<floatToGameObject>();

        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject item in cubes)
        {

            item.renderer.material.color = Color.white;
        }

        collisionsX = new List<GameObject>();
        collisionsY = new List<GameObject>();
        collisionsZ = new List<GameObject>();

        activeCollisionsX = new List<GameObject>();
        activeCollisionsY = new List<GameObject>();
        activeCollisionsZ = new List<GameObject>();

        xlist = new List<floatToGameObject>();
        ylist = new List<floatToGameObject>();
        zlist = new List<floatToGameObject>();

        

        foreach (GameObject item in cubes)
        {

            floatToGameObject tempXMin = new floatToGameObject(item.renderer.bounds.min.x, true, item);
            floatToGameObject tempXMax = new floatToGameObject(item.renderer.bounds.max.x, false, item);
            
            
            xlist.Add(tempXMin);
            xlist.Add(tempXMax);

            floatToGameObject tempYMin = new floatToGameObject(item.renderer.bounds.min.y, true, item);
            floatToGameObject tempYMax = new floatToGameObject(item.renderer.bounds.max.y, false, item);

            ylist.Add(tempYMin);
            ylist.Add(tempYMax);

            floatToGameObject tempZMin = new floatToGameObject(item.renderer.bounds.min.z, true, item);
            floatToGameObject tempZMax = new floatToGameObject(item.renderer.bounds.max.z, false, item);

            zlist.Add(tempZMin);
            zlist.Add(tempZMax);
        }

        xlist.Sort(delegate(floatToGameObject a, floatToGameObject b) { return (a.f.CompareTo(b.f)); });
        ylist.Sort(delegate(floatToGameObject a, floatToGameObject b) { return (a.f.CompareTo(b.f)); });
        zlist.Sort(delegate(floatToGameObject a, floatToGameObject b) { return (a.f.CompareTo(b.f)); });



        foreach (floatToGameObject i in xlist)
        {

            //Debug.Log(i.f.ToString());

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (i.isStart == true && activeCollisionsX.Contains(i.item) == false)
            {
                activeCollisionsX.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                if (activeCollisionsX.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there
                    foreach (GameObject item in activeCollisionsX)
                    {
                        if (collisionsX.Contains(i.item) == false)
                        {
                            collisionsX.Add(i.item);
                        }
                    }
                }
            }

            if (i.isStart == false && activeCollisionsX.Contains(i.item) == true)
            {
                activeCollisionsX.Remove(i.item);
            }
             
        }

        foreach (floatToGameObject i in ylist)
        {

            //Debug.Log(i.f.ToString());

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (i.isStart == true && activeCollisionsX.Contains(i.item) == false)
            {
                activeCollisionsX.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                if (activeCollisionsX.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there
                    foreach (GameObject item in activeCollisionsX)
                    {
                        if (collisionsX.Contains(i.item) == false)
                        {
                            collisionsX.Add(i.item);
                        }
                    }
                }
            }

            if (i.isStart == false && activeCollisionsX.Contains(i.item) == true)
            {
                activeCollisionsX.Remove(i.item);
            }
        }

        foreach (floatToGameObject i in zlist)
        {
            //Debug.Log(i.f.ToString());
            
            /*
            // i is a start
            if (zStart.ContainsKey(i) && !activeCollisionsZ.Contains(zStart[i]))
            {
                activeCollisionsZ.Add(zStart[i]);

                if (activeCollisionsZ.Count >= 2)
                {
                    foreach (GameObject item in activeCollisionsZ )
                    {
                        if (!collisionsZ.Contains(item) && collisionsX.Contains(item) && collisionsY.Contains(item))
                        {
                            collisionsZ.Add(item);
                        }
                    }
                }
            }

            if (zEnd.ContainsKey(i) && activeCollisionsZ.Contains(zEnd[i]))
            {
                activeCollisionsZ.Remove(zEnd[i]);
            }
             * */
        }

        //handleColliding();

        //Debug.Log(collisions.Count.ToString());
        
        foreach (GameObject item in cubes)
        {
            if (collisionsX.Contains(item))
            {
                item.renderer.material.color = Color.black;
            }
            else
            {
                item.renderer.material.color = Color.white;
            }
        }
        

    }

    void handleColliding()
    {


        Debug.Log("Collisions X "+collisionsX.Count.ToString());
        Debug.Log("Collisions Y " + collisionsY.Count.ToString());
        Debug.Log("Collisions Z " + collisionsZ.Count.ToString());
      

        
        foreach (GameObject item in collisionsZ)
        {

            item.renderer.material.color = Color.black;
        } 
        



    }

    int CompareGameObjects(floatToGameObject a, floatToGameObject b)
    {
        return (a.f.CompareTo(b.f));
    }
}