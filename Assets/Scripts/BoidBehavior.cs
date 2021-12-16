using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidBehavior : MonoBehaviour
{
    private readonly float minimal_distance = 10f;
    private readonly float repulsion_force = 2f;
    private readonly int repulsion_direction = -1;


    //In fixedupdate
    public Vector3 Separation(List<GameObject> flockmates, GameObject me)
    {
        Vector3 difference = Vector3.zero;
        if (flockmates.Count <= 0 || me == null)
            return new Vector3(me.transform.position.x, me.transform.position.y, me.transform.position.z);
        for(int i = 0; i < flockmates.Count; i++)
        {
            difference +=flockmates[i].transform.position - me.transform.position;
        }
        Vector3 average = difference / flockmates.Count;
        return average;

    }


    public Vector3 Alignment(List<GameObject> flockmates, GameObject me)
    {
        if (flockmates.Count <= 0 || me == null)
            return new Vector3(me.transform.position.x, me.transform.position.y, me.transform.position.z);
        return AverageOrientationOfFlockmates(flockmates);
}

    public Vector3 Cohesion(List<GameObject> flockmates, GameObject me)
    {
        if (flockmates.Count <= 0 || me == null)
            return new Vector3(me.transform.position.x, me.transform.position.y, me.transform.position.z);
        return AveragePositionOfFlockmates(flockmates);

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

        for(int i = 0; i < flockmates.Count; i++)
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

        return new Vector3((average_rot_x%360) / flockmates.Count, (average_rot_y%360) / flockmates.Count, (average_rot_z%360) / flockmates.Count);
    }

}
