using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] float destroyTime;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
