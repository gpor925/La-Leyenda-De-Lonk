using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController _control;

    [SerializeField] float playerSpeed;

    void Start()
    {
        _control = GetComponent<CharacterController>();

    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized;
        _control.Move(movement * playerSpeed * Time.deltaTime);

        if(movement != Vector3.zero)
        {
            transform.forward = movement;
            //Walk anim
        } 
        else
        {
            //Idle anim
        }
    }
}
