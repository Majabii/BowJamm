// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{
   
    private Transform goal;
    NavMeshAgent agent;

    void Start()
    {
        goal  = XROriginManager.GetXROriginTransform();
        agent = GetComponent<NavMeshAgent>();
        agent.destination = goal.position;
    }

    private void Update()
    {
        agent.destination = goal.position;
    }
}
