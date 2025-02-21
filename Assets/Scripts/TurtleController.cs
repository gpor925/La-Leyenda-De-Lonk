using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TurtleController : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _turtleAnim;

    public GameObject _player;
    public PlayerController _playerScript;

    private float distance;

    [SerializeField] float turtleSpeed = 2f;

    void Start()
    {
    _agent = GetComponent<NavMeshAgent>();  
    _turtleAnim = GetComponent<Animator>();
    
    _agent.speed = turtleSpeed;
    }

    void Update()
    {
        //Turtle movement
        distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance < 5f)
        {
            _agent.destination = _player.transform.position;
        }

        //Turtle animation
        
        _turtleAnim.SetFloat("Speed", _agent.velocity.magnitude);

        if (distance <= _agent.stoppingDistance)
        {
            _turtleAnim.SetTrigger("Attack");
        }

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player Attack")
        {
            _turtleAnim.SetTrigger("Hit");
        }
    }

}
