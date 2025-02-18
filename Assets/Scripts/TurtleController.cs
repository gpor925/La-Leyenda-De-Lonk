using UnityEngine;
using UnityEngine.AI;

public class TurtleController : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;

    void Start()
    {
    agent = GetComponent<NavMeshAgent>();   
    
    }

    void Update()
    {
        if (player)
    }
}
