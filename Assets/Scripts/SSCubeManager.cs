using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct GameObjectWrapper
{
    public float f;
    public bool isStart;
    public GameObject item;
    public List<GameObject> collidingWith;

    public GameObjectWrapper(float fl, bool start, GameObject newItem)
    {
        this.f = fl;
        this.isStart = start;
        this.item = newItem;
        this.collidingWith = new List<GameObject>();
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

    private List<GameObjectWrapper> xlist;
    private List<GameObjectWrapper> ylist;
    private List<GameObjectWrapper> zlist;

    private Dictionary<GameObject, GameObjectWrapper> objectDictionary;

    private Dictionary<GameObject, List<GameObject>> currentCollisionsX;
    private Dictionary<GameObject, List<GameObject>> currentCollisionsY;
    private Dictionary<GameObject, List<GameObject>> currentCollisionsZ;
    

    // Use this for initialization
    void Start()
    {
        collisionsX = new List<GameObject>();
        collisionsY = new List<GameObject>();
        collisionsZ = new List<GameObject>();

        activeCollisionsX = new List<GameObject>();
        activeCollisionsY = new List<GameObject>();
        activeCollisionsZ = new List<GameObject>();

        xlist = new List<GameObjectWrapper>();
        ylist = new List<GameObjectWrapper>();
        zlist = new List<GameObjectWrapper>();

        currentCollisionsX = new Dictionary<GameObject, List<GameObject>>();
        currentCollisionsY = new Dictionary<GameObject, List<GameObject>>();
        currentCollisionsZ = new Dictionary<GameObject, List<GameObject>>();

        objectDictionary = new Dictionary<GameObject, GameObjectWrapper>();
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

        xlist = new List<GameObjectWrapper>();
        ylist = new List<GameObjectWrapper>();
        zlist = new List<GameObjectWrapper>();

        currentCollisionsX = new Dictionary<GameObject, List<GameObject>>();
        currentCollisionsY = new Dictionary<GameObject, List<GameObject>>();
        currentCollisionsZ = new Dictionary<GameObject, List<GameObject>>();

        objectDictionary = new Dictionary<GameObject, GameObjectWrapper>();

        foreach (GameObject item in cubes)
        {

            GameObjectWrapper tempXMin = new GameObjectWrapper(item.renderer.bounds.min.x, true, item);
            GameObjectWrapper tempXMax = new GameObjectWrapper(item.renderer.bounds.max.x, false, item);
            
            
            xlist.Add(tempXMin);
            xlist.Add(tempXMax);

            GameObjectWrapper tempYMin = new GameObjectWrapper(item.renderer.bounds.min.y, true, item);
            GameObjectWrapper tempYMax = new GameObjectWrapper(item.renderer.bounds.max.y, false, item);

            ylist.Add(tempYMin);
            ylist.Add(tempYMax);

            GameObjectWrapper tempZMin = new GameObjectWrapper(item.renderer.bounds.min.z, true, item);
            GameObjectWrapper tempZMax = new GameObjectWrapper(item.renderer.bounds.max.z, false, item);

            zlist.Add(tempZMin);
            zlist.Add(tempZMax);

            objectDictionary[item] = new GameObjectWrapper(0, false, item);

            
        }

        xlist.Sort(delegate(GameObjectWrapper a, GameObjectWrapper b) { return (a.f.CompareTo(b.f)); });
        ylist.Sort(delegate(GameObjectWrapper a, GameObjectWrapper b) { return (a.f.CompareTo(b.f)); });
        zlist.Sort(delegate(GameObjectWrapper a, GameObjectWrapper b) { return (a.f.CompareTo(b.f)); });


        foreach (GameObjectWrapper i in xlist)
        {

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (activeCollisionsX.Contains(i.item) == false)
            {
                activeCollisionsX.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                //Debug.Log(activeCollisionsX.Count);
                if (activeCollisionsX.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there
                    
                    foreach (GameObject item in activeCollisionsX)
                    {
                        if (currentCollisionsX.ContainsKey(i.item) == false)
                        {
                            
                            currentCollisionsX[i.item] = new List<GameObject>();
                            currentCollisionsX[i.item].Add(item);
                        }

                        else
                        {
                            currentCollisionsX[i.item].Add(item);
                        }

                        if (currentCollisionsX.ContainsKey(item) == false)
                        {
                            currentCollisionsX[item] = new List<GameObject>();
                            currentCollisionsX[item].Add(i.item);
                        }

                        else
                        {
                            currentCollisionsX[item].Add(i.item);
                        }
                        
                    }
                }
            }

            else if (activeCollisionsX.Contains(i.item) == true)
            {
                activeCollisionsX.Remove(i.item);
            }
             
        }
        
        foreach (GameObjectWrapper i in ylist)
        {

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (activeCollisionsY.Contains(i.item) == false)
            {
                activeCollisionsY.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                //Debug.Log(activeCollisionsX.Count);
                if (activeCollisionsY.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there
                    
                    foreach (GameObject item in activeCollisionsY)
                    {
                        if (currentCollisionsY.ContainsKey(i.item) == false)
                        {
                            currentCollisionsY[i.item] = new List<GameObject>();
                            currentCollisionsY[i.item].Add(item);
                        }

                        if (currentCollisionsY[i.item].Contains(item) == false)
                        {
                            currentCollisionsY[i.item].Add(item);
                        }
                    }
                }
            }

            else if (activeCollisionsY.Contains(i.item) == true)
            {
                activeCollisionsY.Remove(i.item);
            }
             
        }


        foreach (GameObjectWrapper i in zlist)
        {

            // i is a starting point, and active collisions doesn't already contain the gameobject we're looking at
            if (activeCollisionsZ.Contains(i.item) == false)
            {
                activeCollisionsZ.Add(i.item);

                // if we've got more than 2 things colliding on x axis
                //Debug.Log(activeCollisionsX.Count);
                if (activeCollisionsZ.Count >= 2)
                {
                    // add everything in active collisions if it already isnt there

                    foreach (GameObject item in activeCollisionsZ)
                    {
                        if (currentCollisionsZ.ContainsKey(i.item) == false)
                        {
                            currentCollisionsZ[i.item] = new List<GameObject>();
                            currentCollisionsZ[i.item].Add(item);

                        }
                        if (currentCollisionsZ[i.item].Contains(item) == false)
                        {
                            currentCollisionsZ[i.item].Add(item);
                        }
                    }
                }
            }

            else if (activeCollisionsZ.Contains(i.item) == true)
            {
                activeCollisionsZ.Remove(i.item);
            }

        }


        List<GameObject> totalCollisions = new List<GameObject>();

        Debug.Log("current collisions x: " + currentCollisionsX.Count);

        //search through the things that have collisions on the x-axis
        foreach (GameObject item in currentCollisionsX.Keys)
        {
            // search through everything that the item is colliding with on the x axis

            foreach (GameObject i in currentCollisionsX[item])
            {
                if (currentCollisionsX.ContainsKey(i))
                {
                    if (currentCollisionsX[i].Contains(item))
                    {
                        // i and item are colliding on the x-axis
                        if (totalCollisions.Contains(item) == false)
                        {
                            totalCollisions.Add(item);
                        }

                        if (totalCollisions.Contains(i) == false)
                        {
                            totalCollisions.Add(i);
                        }
                    }
                }
            }
        }

        Debug.Log("total collisions: " + totalCollisions.Count);

        foreach (GameObject item in cubes)
        {
            if(totalCollisions.Contains(item))
            {
                item.renderer.material.color = Color.black;
            }

            else
            {
                item.renderer.material.color = Color.white;
            }
        }

    }

    int CompareGameObjects(GameObjectWrapper a, GameObjectWrapper b)
    {
        return (a.f.CompareTo(b.f));
    }
}
