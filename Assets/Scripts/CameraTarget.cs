using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] GameObject _player;

    [SerializeField] float aheadDistance = 2f; 
    [SerializeField] float smoothSpeed = 5f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPosition = _player.transform.position + _player.transform.forward * aheadDistance;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / smoothSpeed);
    }
}
