using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentle_handle : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 vboid;
    public Vector3 v3;

    /*public float speed = 4f ;*//**/
    public float rotSpeed = 30f;

    public float minSpeed = 3f ;  // minimum range of speed to move
    public float maxSpeed = 10f;  // maximum range of speed to move
    float speed;     // speed is a constantly changing value from the random range of minSpeed and maxSpeed 

    public string[] collisionTags;             //  What are the GO tags that will act as colliders that trigger a
                                               //  direction change? Tags like for walls, room objects, etc.
    public AudioClip collisionSound;

    float step = Mathf.PI / 60;
    float timeVar = 0;
    float rotationRange = 235;                  //  How far should the object rotate to find a new direction?
    float baseDirection = 0;
    float lastDeltaTime;
    public float gamma;
    Quaternion from;
    Quaternion to;

    Vector3 aimEulerAngles;
    Quaternion aimRotation;

    Vector3 randomDirection;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        coroutine = Boid_control();
        StartCoroutine(coroutine);
        gamma = transform.GetChild(0).GetComponent<Boid>().gamma;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {

        vboid = transform.GetChild(0).GetComponent<Boid>().vtot;
        vboid = transform.GetChild(0).GetComponent<Boid>().v3;
        RotationTo(v3);


    }


    IEnumerator RotationTo(Vector3 target)
    {
        aimEulerAngles = target;
        aimRotation.eulerAngles = aimEulerAngles + transform.parent.rotation.eulerAngles;
        float duration = 1.5f;
        float time = 0;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, aimRotation, time / duration);
            time += Time.deltaTime*gamma;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }

/*void RotationTo(Vector3 target)
    { 
        lastDeltaTime += Time.fixedDeltaTime;

        while (lastDeltaTime >= 0.1f)
        {
            lastDeltaTime -= 0.1f;
            from = transform.rotation;
            to = Quaternion.Euler(transform.rotation.eulerAngles + (target - transform.rotation.eulerAngles));
        }
    transform.rotation = Quaternion.Slerp(from, to, );
    }*/


    IEnumerator Boid_control()
    {
        while(true)
        {
            rb.velocity = Vector3.zero;
            
    /*        steer = transform.GetChild(0).GetComponent<Boid>().v3;*/
            /*vboid = new Vector3(1f, 0f, 1f) * 50;*/

            transform.Rotate(randomDirection * Time.deltaTime * 5.0f);


            if (transform.GetChild(0).gameObject.GetComponent<Boid>().localSwarm.Count > 0 | vboid != Vector3.zero)
            {

            /*              transform.LookAt(vboid);
                          transform.Rotate(randomDirection * Time.deltaTime * 5.0f);*/
                
                speed = 2f;
                rb.velocity = (transform.up * speed);
                Debug.DrawLine(transform.position, transform.position + vboid, Color.red);

            }
            else
            {

                randomDirection = new Vector3(Random.Range(-1f, 1f) * (rotationRange / 2) + baseDirection, 0, Random.Range(-1f, 1f) * (rotationRange / 2) + baseDirection); //   Moving at random angles 
                speed = Random.Range(minSpeed, maxSpeed);              //      Change this range of numbers to change speed
                speed = 4f;
                rb.velocity = (transform.up * speed);
                transform.Rotate(randomDirection * Time.deltaTime * 5.0f);
            }

            yield return new WaitForSeconds(Time.deltaTime);

        }
 
    }


    


    IEnumerator Aleatoire_Movement()
    {
        /*   rb.velocity += transform.up * speed;
           GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * rotSpeed;
           yield return new WaitForSeconds(1);*/
        while (true)
        {
            randomDirection = new Vector3(0, Mathf.Sin(timeVar) * (rotationRange / 2) + baseDirection, 0); //   Moving at random angles 
            timeVar += step;
            speed = Random.Range(minSpeed, maxSpeed);              //      Change this range of numbers to change speed
            GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            transform.Rotate(randomDirection * Time.deltaTime * 5.0f);
        }
              



    }
}
