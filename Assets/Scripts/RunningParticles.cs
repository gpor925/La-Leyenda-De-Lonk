using UnityEngine;

public class RunningParticles : MonoBehaviour
{
    [SerializeField] CharacterController player;
    private ParticleSystem runningParticles;
    private ParticleSystem.EmissionModule emission;

    void Start()
    {
        runningParticles = GetComponent<ParticleSystem>();
        emission = runningParticles.emission;
        emission.enabled = false;
    }

    void Update()
    {
        if (player.velocity.magnitude > 0f)
        {
            emission.enabled = true;
        }
        else
        {
            emission.enabled = false;
        }
    }
}
