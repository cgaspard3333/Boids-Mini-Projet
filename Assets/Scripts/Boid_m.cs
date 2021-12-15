using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_m : MonoBehaviour
{

    public Vector3 velocity;

    public float maxVelocity = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(velocity.magnitude > maxVelocity) {
            velocity = velocity.normalized * maxVelocity;
        }
        this.transform.position += velocity * Time.deltaTime;
        this.transform.rotation = Quaternion.LookRotation(velocity);
    }
}
