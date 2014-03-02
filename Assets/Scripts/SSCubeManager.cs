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
                        

                        if (i.collidingWith.Contains(item) == false)
                        {
                            i.collidingWith.Add(item);
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
        
        foreach (GameObjectWrapper i in ylist)
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
                        if (collisionsY.Contains(item) == false)
                        {
                            collisionsY.Add(item);
                        }
                        if (i.collidingWith.Contains(item) == false)
                        {
                            i.collidingWith.Add(item);
                        }
                    }
                }
            }

            if (i.isStart == false && activeCollisionsY.Contains(i.item) == true)
            {
                activeCollisionsY.Remove(i.item);
            }
             
        }


        foreach (GameObjectWrapper i in zlist)
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
                        if (collisionsZ.Contains(item) == false)
                        {
                            collisionsZ.Add(item);
                        }

                        if (i.collidingWith.Contains(item) == false)
                        {
                            i.collidingWith.Add(item);
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

        // search through every possible collision
        foreach (GameObject item in collisionsX)
        {
            // for each x collision, search through the objects it collided with
            foreach (GameObject collisionY in objectDictionary[item].collidingWith)
            {
                //if the item being searched is in THIS collisionY's colliding list, they are colliding together

                // do the same for z
            }
        }
        

    }

    void handleColliding()
    {


        Debug.Log("Collisions X "+collisionsX.Count.ToString());
        Debug.Log("Collisions Y " + collisionsY.Count.ToString());
        Debug.Log("Collisions Z " + collisionsZ.Count.ToString());
      

        
        foreach (GameObjectWrapper item in xlist)
        {
            
        } 
        



    }

    int CompareGameObjects(GameObjectWrapper a, GameObjectWrapper b)
    {
        return (a.f.CompareTo(b.f));
    }
}
