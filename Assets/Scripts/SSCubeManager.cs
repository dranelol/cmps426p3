using UnityEngine;
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

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (i.isStart == true && activeCollisionsX.Contains(i.item) == false)
            {
                activeCollisionsX.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                //Debug.Log(activeCollisionsX.Count);
                if (activeCollisionsX.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there
                    
                    foreach (GameObject item in activeCollisionsX)
                    {
                        
                        if (collisionsX.Contains(item) == false)
                        {
                            collisionsX.Add(item);
                        }
                    }
                }
            }

            if (i.isStart == false && activeCollisionsX.Contains(i.item) == true)
            {
                activeCollisionsX.Remove(i.item);
            }
             
        }

        Debug.Log("active x: " + activeCollisionsX.Count);
        
        foreach (floatToGameObject i in ylist)
        {

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (i.isStart == true && activeCollisionsY.Contains(i.item) == false)
            {
                activeCollisionsY.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                //Debug.Log(activeCollisionsX.Count);
                if (activeCollisionsY.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there
                    
                    foreach (GameObject item in activeCollisionsY)
                    {
                        
                        if (collisionsX.Contains(item) == true && collisionsY.Contains(item) == false)
                        {
                            collisionsY.Add(item);
                        }
                    }
                }
            }

            if (i.isStart == false && activeCollisionsY.Contains(i.item) == true)
            {
                activeCollisionsY.Remove(i.item);
            }
             
        }


        foreach (floatToGameObject i in zlist)
        {

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (i.isStart == true && activeCollisionsZ.Contains(i.item) == false)
            {
                activeCollisionsZ.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                //Debug.Log(activeCollisionsX.Count);
                if (activeCollisionsZ.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there

                    foreach (GameObject item in activeCollisionsZ)
                    {

                        if (collisionsX.Contains(item) == true 
                            && collisionsY.Contains(item) == true 
                            && collisionsZ.Contains(item) == false)
                        {
                            collisionsZ.Add(item);
                        }
                    }
                }
            }

            if (i.isStart == false && activeCollisionsZ.Contains(i.item) == true)
            {
                activeCollisionsZ.Remove(i.item);
            }

        }
        
        Debug.Log("x: " + collisionsX.Count.ToString());
        Debug.Log("y: " + collisionsY.Count.ToString());
        Debug.Log("z: " + collisionsZ.Count.ToString());

        
        foreach (GameObject item in cubes)
        {
            if (collisionsX.Contains(item) == true)
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
