using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehaviour : MonoBehaviour {
    public float RepathTime = 4.0f;
    public float MoveTimer = 0.8f;
    public MazeColorHandler colorHandler;
    private float repathTime;
    private float moveTimer;

    private List<Vector3> openList;
    private List<Vector3> closedList;
    private List<Vector3> seenList;

    private Pathfinder pathfinder;

    public GameObject player;

    public int AINum;

	void Start () 
    {
        colorHandler = transform.parent.GetComponent<MazeColorHandler>();
        repathTime = RepathTime;
        moveTimer = MoveTimer;

        pathfinder = transform.parent.GetComponent<GenerateNodes>().pathfinder;
	}
	
	void Update () 
    {
        if (MoveTimer <= 0.0f)
        {
            if (RepathTime <= 0.0f)
            {
                // choose new path

                if (player == null)
                {
                    player = GameObject.FindGameObjectWithTag("Player");
                }

                List<List<Vector3>> newPath = pathfinder.FindOptimalPath(transform.position, player.transform.position);

                closedList = newPath[0];
                openList = newPath[1];
                seenList = newPath[2];

                RepathTime = repathTime;
            }

            else
            {
                RepathTime -= Time.deltaTime;
            }

            // move if possible
            Vector3 newPosition = new Vector3(closedList[0].x, closedList[0].y, transform.position.z);

            if (newPosition.x != player.transform.position.x && newPosition.y != player.transform.position.y)
            {
                closedList.RemoveAt(0);
                transform.position = newPosition;
            }

            MoveTimer = moveTimer;
        }

        else
        {
            MoveTimer -= Time.deltaTime;
        }
	}
}
