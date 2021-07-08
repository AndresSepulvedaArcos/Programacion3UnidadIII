using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerControllerCommand : MonoBehaviour
{
    //FIFO LIFO

    NavMeshAgent agent;
   
    public Queue<Vector3> waypoints = new Queue<Vector3>();
    public List<Vector3> waypointsToView;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddActionToQueue()
    {
        Ray ray=   Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            waypoints.Enqueue(hit.point);
            DisplayQueue();


        }


    }

    void DisplayQueue()
    {
        waypointsToView.Clear();
        waypointsToView.AddRange(waypoints.ToArray());

    }
    void MoveToNextWaypoint()
    {
        waypoints.Dequeue();
        DisplayQueue();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AddActionToQueue();
        }
         
    }
}
