using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Boid : MonoBehaviour
{
    private readonly float minimal_distance = 10f;
    private readonly float repulsion_force = 5f;
    private readonly int repulsion_direction = -1;


    private float translationx;
    private float translationz;
    private float thrusty;

    public bool IsOn = true;

    private float speedxy = 8f;
    private float speedz = 16f;

    private Vector3 v1;
    private Vector3 v2;
    public Vector3 v3;
    public Vector3 vtot;

    public float alpha = 1f;
    public float beta = 1f;
    public float gamma = 1f;

    private Rigidbody rigidbodyComponent;
    Vector3 DirectionObstable;
    float smooth;
    float collider_size;
    public List<GameObject> localSwarm;


    // Start is called before the first frame update
    void Start()
    {
        collider_size = this.gameObject.GetComponent<SphereCollider>().radius;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    



    void FixedUpdate()
    {


        v1 = Cohesion(localSwarm, this.gameObject);
        /**//*v2 = Separation(localSwarm, this.gameObject);*/
        v3 = Alignment(localSwarm, this.gameObject);

        vtot =(alpha * v1 + gamma*v3 );
        /*Debug.Log(vtot);*/
        

        /*update shere radius*/

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

/*    void OnTriggerStay(Collider other)
    {
        if (!(other.gameObject.name.Contains("Gentle")))
        {
            DirectionObstable = other.transform.position - transform.position;

            smooth = 0.0001f * (collider_size - (float)DirectionObstable.magnitude) / (float)Math.Pow(DirectionObstable.magnitude, 3f);
            *//*transform.parent.GetComponent<Rigidbody>().velocity += new Vector3(-DirectionObstable.x, 0, -DirectionObstable.z) * smooth;*//*
            transform.parent.GetComponent<Rigidbody>().AddForce(-smooth*DirectionObstable);
        }

    }*/




    public void UpdateList(List<GameObject> resfeshedList)
    {

        localSwarm = resfeshedList;

    }




    public void Separation(GameObject collider, GameObject me)
    {
        Vector3 directionToCollider = collider.transform.position - me.transform.position;
        float distance = Vector3.Distance(collider.transform.position, me.transform.position);
        float repulsionDistanceStrenght = (minimal_distance / distance) * repulsion_force;

        me.GetComponent<Rigidbody>().AddForce(repulsionDistanceStrenght * (directionToCollider * repulsion_direction), ForceMode.Force);
    }


    public Vector3 Alignment(List<GameObject> flockmates, GameObject me)
    {
        if (flockmates.Count <= 0 || me == null)
            return new Vector3(0f, 0f, 0f);
        return AverageOrientationOfFlockmates(flockmates);
    }

    public Vector3 Cohesion(List<GameObject> flockmates, GameObject me)
    {
        if (flockmates.Count <= 0 || me == null)
            return new Vector3(0f, 0f, 0f);
        return AveragePositionOfFlockmates(flockmates);

    }

    private Vector3 NewPositionInCrowdingEnvironment(List<GameObject> near_flockmates, GameObject me)
    {
        // find a new pos
        // check distance 
        // calculer un cercle autour de chaque mate et trouver le point de croisement entre tous
        // ou décaler au fur et à mesure 

        for (int i = 0; i < near_flockmates.Count; i++)
        {

        }
        return new Vector3(0.0f, 0.0f, 0.0f);
    }


    private float DistanceBetweenBoidAndPosition(GameObject boid1, Vector3 boid2)
    {
        Vector3 boid1_position = boid1.transform.position;
        Vector3 boid2_position = boid2;
        float square_x = Mathf.Pow(boid2_position.x - boid1_position.x, 2f);
        float square_y = Mathf.Pow(boid2_position.y - boid1_position.y, 2f);
        float square_z = Mathf.Pow(boid2_position.z - boid1_position.z, 2f);

        float distance = Mathf.Sqrt(square_x + square_y + square_z);
        return distance;
    }

    private Vector3 AveragePositionOfFlockmates(List<GameObject> flockmates)
    {
        float average_x = 0f;
        float average_y = 0f;
        float average_z = 0f;

        for (int i = 0; i < flockmates.Count; i++)
        {
            average_x += flockmates[i].transform.position.x;
            average_y += flockmates[i].transform.position.y;
            average_z += flockmates[i].transform.position.z;
        }

        return new Vector3(average_x / flockmates.Count, average_y / flockmates.Count, average_z / flockmates.Count);
    }

    private Vector3 AverageOrientationOfFlockmates(List<GameObject> flockmates)
    {
        float average_rot_x = 0f;
        float average_rot_y = 0f;
        float average_rot_z = 0f;

        for (int i = 0; i < flockmates.Count; i++)
        {
            average_rot_x += flockmates[i].transform.rotation.eulerAngles.x;
            average_rot_y += flockmates[i].transform.rotation.eulerAngles.y;
            average_rot_z += flockmates[i].transform.rotation.eulerAngles.z;
        }

        return new Vector3((average_rot_x % 360) / flockmates.Count, (average_rot_y % 360) / flockmates.Count, (average_rot_z % 360) / flockmates.Count);
    }



}
