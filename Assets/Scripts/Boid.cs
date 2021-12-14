using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private GameObject spawn;
    private GameObject[] theFlock;

    [Range(0.1f, 20.0f)]
    public float velocity = 6.0f;

    [Range(0.0f, 0.9f)]
    public float velocityVariation = 0.5f;

    [Range(0.1f, 20.0f)]
    public float rotationCoeff = 4.0f;

    [Range(0.1f, 10.0f)]
    public float neighborDist = 2.0f;


    // Caluculates the separation vector with a target.
    Vector3 GetSeparationVector(Transform target)
    {
        var diff = transform.position - target.transform.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / neighborDist);
        return diff * (scaler / diffLen);
    }

    void BoidBehavior(){
        var currentPosition = transform.position;
        var currentRotation = transform.rotation;

        // Initializes the vectors.
        var separation = Vector3.zero;
        var alignment = transform.forward;
        var cohesion = transform.position;

        // Looks up nearby boids.
        var nearbyBoids = Physics.OverlapSphere(currentPosition, neighborDist);

        // Accumulates the vectors.
        foreach (var boid in nearbyBoids)
        {
            if (boid.gameObject == gameObject) continue;
            var t = boid.transform;
            separation += GetSeparationVector(t);
            alignment += t.forward;
            cohesion += t.position;
        }

        var avg = 1.0f / nearbyBoids.Length;
        alignment *= avg;
        cohesion *= avg;
        cohesion = (cohesion - currentPosition).normalized;

        
        // Calculates a rotation from the vectors.
        var direction = separation + alignment + cohesion;
        var rotation = Quaternion.FromToRotation(Vector3.forward, direction.normalized);

        // Applys the rotation with interpolation.
        if (rotation != currentRotation)
        {
            var ip = Mathf.Exp(-rotationCoeff * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(rotation, currentRotation, ip);
        }
        
        // Moves forawrd.
        transform.position = currentPosition + transform.forward * (velocity * Time.deltaTime);
    }

    void Start()
    {    
        Spawn spawnScript = GameObject.FindObjectOfType(typeof(Spawn)) as Spawn;
        theFlock = spawnScript.getFlockEntities();
    } 

    void Update()
    {
        BoidBehavior();
    }


}
