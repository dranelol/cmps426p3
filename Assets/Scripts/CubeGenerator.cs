using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubeGenerator : MonoBehaviour 
{
    public GameObject cube;
    public GameObject player;

    private float nextGenTime;
    private CubeManager manager;

	// Use this for initialization
	void Start () 
    {
        nextGenTime = 0.0f;
        manager = GetComponent<CubeManager>();

        GameObject playerCube = (GameObject)Instantiate(player, Vector3.zero, transform.rotation);
        playerCube.transform.parent = transform;
        playerCube.renderer.material.color = Color.cyan;
        manager.cubes.Add(playerCube);
	}
	
	// Update is called once per frame
	void Update () 
    {
        


        if (nextGenTime < 4.0f && Time.time > nextGenTime)
        {
            
            
            int spawnCount = 50;

            while (spawnCount > 0)
            {
                Vector3 newScale = new Vector3((Random.Range(5, 20) / 10.0f), (Random.Range(5, 20) / 10.0f), (Random.Range(5, 20) / 10.0f));

                GameObject newCube = (GameObject)Instantiate(cube, Vector3.zero, transform.rotation);
                newCube.renderer.material.color = Color.white;
                newCube.transform.parent = transform;
                newCube.transform.localScale = newScale;

                

                newCube.transform.Rotate(new Vector3((Random.Range(-180, 180)), (Random.Range(-180, 180)), (Random.Range(-180, 180))));

                newCube.constantForce.force = new Vector3((Random.Range(-10, 10)), (Random.Range(-10, 10)), (Random.Range(-10, 10))) / 5.0f;

                manager.cubes.Add(newCube);

                spawnCount--;
            }


            nextGenTime += 1.0f;
        }
	}
}
