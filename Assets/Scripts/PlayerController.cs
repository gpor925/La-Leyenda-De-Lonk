using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _control;
    Animator _anim;

    [SerializeField] float playerSpeed;

    void Start()
    {
        //Get components
        _control = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
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

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        _anim.SetBool("playerAttack", true);
        yield return new WaitForSeconds(0.7f * Time.deltaTime);
        _anim.SetBool("playerAttack", false);
    }
}
