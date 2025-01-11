using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWPGraphs : MonoBehaviour
{
    Transform goal;
    float speed = 5f;
    float accuracy = 5f;
    float rotSpeed = 2f;

    public GameObject WPManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWp = 0;
    Graph g;
    // Start is called before the first frame update
    void Start()
    {
        wps = WPManager.GetComponent<WPManager>().wayPoints;
        g = WPManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];
        
    }

    public void GoToHeli()
    {
        g.AStar(currentNode, wps[0]);
        currentWp = 0;
    }

    public void GoToRuin()
    {
        g.AStar(currentNode, wps[7]);
        currentWp = 0;
    }

    public void GoToTent()
    {
        g.AStar(currentNode, wps[9]);
        currentWp = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(g.pathList.Count == 0||currentWp == g.pathList.Count)
        {
            return;
        }
        if (Vector3.Distance(g.pathList[currentWp].getId().transform.position, this.transform.position) < accuracy)
        {
            currentNode = g.pathList[currentWp].getId();
            currentWp++;
        }
        if(currentWp < g.pathList.Count)
        {
            goal = g.pathList[currentWp].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);
            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
            this.transform.Translate(0, 0, speed * Time.deltaTime);
        }
    }
}
