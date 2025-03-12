using UnityEngine;

public class SawControl : MonoBehaviour
{
    [SerializeField]
    private Vector3 pointA; // First point for saw movement
    [SerializeField]
    private Vector3 pointB; // Second point for saw movement
    [SerializeField]
    private float speed = 2.0f; // Speed of the saw movement

    private float journeyLength;
    private float startTime;
    private bool movingToB = true;

    void Start()
    {
        journeyLength = Vector3.Distance(pointA, pointB);
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        if (movingToB)
        {
            transform.position = Vector3.Lerp(pointA, pointB, fractionOfJourney);
            if (fractionOfJourney >= 1.0f)
            {
                movingToB = false;
                startTime = Time.time; // Reset start time for the return journey
            }
        }
        else
        {
            transform.position = Vector3.Lerp(pointB, pointA, fractionOfJourney);
            if (fractionOfJourney >= 1.0f)
            {
                movingToB = true;
                startTime = Time.time; // Reset start time for the return journey
            }
        }
    }
}
