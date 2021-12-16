using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidEntity : MonoBehaviour
{
    private List<GameObject> localSwarm;
    private Vector3 speed;
    private float maxSpeed = 10f;
    private BoidBehavior boidBehavior;
    private bool is_moving = false;

    //Flocking behaviour variable
    private Vector3 cohesionGoToVector;
    private Vector3 separationGoToVector;
    private Vector3 alignmentGoToVector;

    // Rotation variable
    private float offset_angle = 10f;
    private float offset_rotation_time = 0.5f;
    private bool is_rotating = false;
    private float lastDeltaTime;
    private Quaternion from;
    private Quaternion to;

    // public coeficient
    public float alpha = 1f;
    public float beta = 1f;
    public float gamma = 1f;
    public float boidAreaSize = 1f;
    public float boidRepulseSize = 1f;
    // Update is called once per frame
    void Update()
    {
        if (speed.magnitude > maxSpeed)
        {
            speed = speed.normalized * maxSpeed;
        }
        if (localSwarm.Count > 0)
        {
            cohesionGoToVector = boidBehavior.Cohesion(localSwarm, this.gameObject);
            separationGoToVector = boidBehavior.Separation(localSwarm, this.gameObject);
            alignmentGoToVector = boidBehavior.Alignment(localSwarm, this.gameObject);
            if (!is_rotating)
                StartCoroutine(AlignTo());
            if (!is_moving)
                StartCoroutine(MoveTo());
        }
        else
        {
            if (!is_moving)
                StartCoroutine(RandomMovement());
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("Gentle"))
        {
            localSwarm.Add(other.gameObject);
        }
    }


    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name.Contains("Gentle"))
        {
            localSwarm.Remove(other.gameObject);
        }
    }

    IEnumerator AlignTo()
    {
        is_rotating = true;
        lastDeltaTime = 0;
        from = transform.rotation;
        to = Quaternion.Euler(alignmentGoToVector.x + Random.Range(-offset_angle, offset_angle), 
                              alignmentGoToVector.y + Random.Range(-offset_angle, offset_angle), 
                              alignmentGoToVector.z + Random.Range(-offset_angle, offset_angle));
        while (lastDeltaTime < offset_rotation_time)
        {
            transform.rotation = Quaternion.Slerp(from, to, lastDeltaTime / offset_rotation_time);
            lastDeltaTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        is_rotating = false;
    }

    IEnumerator MoveTo()
    {
        is_moving = true;
        lastDeltaTime = 0;
        while (lastDeltaTime < offset_rotation_time)
        {
            // TODO
            lastDeltaTime += Time.deltaTime;
            yield return null;
        }
        is_moving = false;
    }

    IEnumerator RandomMovement()
    {
        is_moving = true;
        lastDeltaTime = 0;
        while (lastDeltaTime < offset_rotation_time)
        {
            lastDeltaTime += Time.deltaTime;
            yield return null;
        }
        //TODO : ajouter le déplacement aléatoire
        is_moving = false;
    }
}
