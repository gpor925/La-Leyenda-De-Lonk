using UnityEngine;

using System.Collections; // Add this line to include the necessary namespace for IEnumerator

public class Level2Door : MonoBehaviour

{
    [SerializeField] private Vector3 targetPosition; // Target position for the door to move to
    [SerializeField] Animator _anim;

    public void Open()
    {
        _anim.SetTrigger("Open");
        Debug.Log("Door open");
        Destroy(gameObject); 
    }
}
