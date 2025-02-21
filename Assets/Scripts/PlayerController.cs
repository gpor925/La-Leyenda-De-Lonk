using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _control;
    Animator _anim;
    [SerializeField] GameObject _attackHitbox;

    [SerializeField] float playerSpeed;
    public bool isAttacking;

    void Start()
    {
        //Get components
        _control = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();

        _attackHitbox.SetActive(false);
        isAttacking = false;
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
        _anim.SetFloat("speed", movement.magnitude); //Idle to run animation

        //Lock on

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(attack());
        }

        if (isAttacking)
        {
            _attackHitbox.SetActive(true);
        } 
        else
        {
            _attackHitbox.SetActive(false);
        }
    }

    IEnumerator attack()
    {
        _anim.SetTrigger("playerAttack");
        isAttacking = true;
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
    }
}
