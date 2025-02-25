using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TurtleController : MonoBehaviour
{
    NavMeshAgent _agent;
    Animator _turtleAnim;

    public GameObject _player;
    public GameObject _turtle;
    public GameObject _smoke;
    public PlayerController _playerScript;

    private float distance;

    private float turtleSpeed;

    [SerializeField] float chaseSpeed = 2f;
    [SerializeField] float knockbackForce = 10f;
    [SerializeField] float knockbackDuration = 0.2f;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _turtleAnim = GetComponent<Animator>();

        turtleSpeed = chaseSpeed;
        _agent.speed = turtleSpeed;
    }

    void Update()
    {
        //Turtle movement
        distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance < 5f)
        {
            turtleSpeed = chaseSpeed;
            _agent.destination = _player.transform.position;
        }

        //Turtle animation
        _turtleAnim.SetFloat("Speed", _agent.velocity.magnitude);

        if (distance <= _agent.stoppingDistance)
        {
            _turtleAnim.SetTrigger("Attack");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("Enemy Hit");
            _playerScript.HitParticle();
            ApplyKnockback(other.transform.position);
            StartCoroutine(EnemyDeath());
        }
    }

    //Knockback
    void ApplyKnockback(Vector3 attackerPosition)
    {
        Vector3 knockbackDirection = (transform.position - attackerPosition).normalized;
        _agent.velocity = knockbackDirection * knockbackForce;
        StartCoroutine(ResetKnockback());
    }

    IEnumerator ResetKnockback()
    {
        _agent.speed = 0f;
        yield return new WaitForSeconds(knockbackDuration);
        _agent.velocity = Vector3.zero;
    }

    //Death
    IEnumerator EnemyDeath()
    {
        _turtleAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(3f);
        Instantiate(_smoke, transform.position, Quaternion.identity); 
        Destroy(gameObject);
    }
}
