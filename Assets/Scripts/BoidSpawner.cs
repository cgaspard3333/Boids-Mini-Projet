using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BoidSpawner : MonoBehaviour
{
    private int swarm_size = 10; 
    public float start_separation = 12f;
    public Slider swarmSizeHanlder;

    public Slider CohesionHanlder;
    public Slider SeparationeHanlder;
    public Slider AlignementHanlder;





    public void CreateSwarm()
    {

        swarm_size = (int)swarmSizeHanlder.value;
        for (int i = 0; i < swarm_size; i++)
        {
            var rand_offset = new Vector3(Random.Range(-start_separation, start_separation), Random.Range(-start_separation/3f, start_separation/3f), Random.Range(-start_separation, start_separation))*start_separation;
            GameObject NewSpawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Gentle/Gentle"), transform.position + rand_offset ,Random.rotation);
  
        }

    }

    public void OnchangeValue()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("change value");
            child.transform.GetChild(0).GetComponent<Boid>().alpha = CohesionHanlder.value;
            child.transform.GetChild(0).GetComponent<Boid>().beta = SeparationeHanlder.value;
            child.transform.GetChild(0).GetComponent<Boid>().gamma = AlignementHanlder.value;
            child.GetComponent<Gentle_handle>().gamma = AlignementHanlder.value;
            Debug.Log("change value btw");

            child.transform.GetChild(0).GetComponent<SphereCollider>().radius = 5 + CohesionHanlder.value;
            Debug.Log("change value finnished");
        }

    }

}
