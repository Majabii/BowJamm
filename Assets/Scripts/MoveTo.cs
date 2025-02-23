// MoveTo.cs
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class MoveTo : MonoBehaviour
{
   
    private Vector3 goal = new Vector3 (-21.4f, -1.27f, -22.3f);
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.destination = goal;
    }
}
