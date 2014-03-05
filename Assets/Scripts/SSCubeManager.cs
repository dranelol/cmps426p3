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

    private List<GameObject> activeCollisionsX;
    private List<GameObject> activeCollisionsY;
    private List<GameObject> activeCollisionsZ;

    private List<GameObjectWrapper> xlist;
    private List<GameObjectWrapper> ylist;
    private List<GameObjectWrapper> zlist;

    private List<GameObject> currentCollisionsX;
    private List<GameObject> currentCollisionsY;
    private List<GameObject> currentCollisionsZ;

    private bool listsPopulated = false;
    

    // Use this for initialization
    void Start()
    {

        activeCollisionsX = new List<GameObject>();
        activeCollisionsY = new List<GameObject>();
        activeCollisionsZ = new List<GameObject>();

        xlist = new List<GameObjectWrapper>();
        ylist = new List<GameObjectWrapper>();
        zlist = new List<GameObjectWrapper>();

        currentCollisionsX = new List<GameObject>();
        currentCollisionsY = new List<GameObject>();
        currentCollisionsZ = new List<GameObject>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (cubes.Count <= 200)
        {
            xlist = new List<GameObjectWrapper>();
            ylist = new List<GameObjectWrapper>();
            zlist = new List<GameObjectWrapper>();

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

                item.renderer.material.color = Color.white;
            }
        }

        activeCollisionsX = new List<GameObject>();
        activeCollisionsY = new List<GameObject>();
        activeCollisionsZ = new List<GameObject>();

        currentCollisionsX = new List<GameObject>();
        currentCollisionsY = new List<GameObject>();
        currentCollisionsZ = new List<GameObject>();

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
                        if (currentCollisionsX.Contains(item) == false)
                        {
                            currentCollisionsX.Add(item);
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
                        if (currentCollisionsX.Contains(item) == true)
                        {
                            if (currentCollisionsY.Contains(item) == false)
                            {
                                currentCollisionsY.Add(item);
                            }
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
                        if (currentCollisionsX.Contains(item) == true)
                        {
                            if (currentCollisionsY.Contains(item) == true)
                            {
                                if (currentCollisionsZ.Contains(item) == false)
                                {
                                    currentCollisionsZ.Add(item);
                                }
                            }
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

        foreach (GameObject item1 in currentCollisionsZ)
        {
            foreach (GameObject item2 in currentCollisionsZ)
            {
                if (item1.GetInstanceID() != item2.GetInstanceID())
                {
                    if (isColliding(item1, item2))
                    {
                        if (totalCollisions.Contains(item1) == false)
                        {
                            totalCollisions.Add(item1);
                        }
                    }


                }

            }
        }

        foreach (GameObject item in cubes)
        {
            if (totalCollisions.Contains(item))
            {
                if (item.tag == "Player")
                {
                    item.renderer.material.color = Color.red;
                }

                else
                {
                    item.renderer.material.color = Color.black;
                }
            }

            else
            {
                if (item.tag == "Player")
                {
                    item.renderer.material.color = Color.cyan;
                }
                else
                {
                    item.renderer.material.color = Color.white;
                }
            }
        }
    }

    

    int CompareGameObjects(GameObjectWrapper a, GameObjectWrapper b)
    {
        return (a.f.CompareTo(b.f));
    }

    bool isColliding(GameObject a, GameObject b)
    {

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
