using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _control;
    Animator _anim;

    [SerializeField] GameObject _attackHitbox;
    [SerializeField] ParticleSystem _particleSystem;

    [SerializeField] float playerSpeed;

    void Start()
    {
        //Get components
        _control = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        _attackHitbox.SetActive(false);
    }

    void Update()
    {
        //Movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        //Movement
        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized; 

        _control.Move(movement * playerSpeed * Time.deltaTime); 

        if(movement != Vector3.zero)
        {
            transform.forward = movement;
        } 
        _anim.SetFloat("speed", movement.magnitude);

        //Lock on

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetTrigger("playerAttack");
        }
    }

    public void HitParticle()
    {
        _particleSystem.Emit(1);
        Debug.Log("hit particle active");
    }
}
