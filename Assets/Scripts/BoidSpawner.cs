using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidSpawner : MonoBehaviour
{
    private int swarm_size = 10; 
    public float start_separation = 12f;

    public Slider swarmSizeHanlder;

    private List<GameObject> gameObjects = new List<GameObject>();

    public void CreateSwarm()
    {
        swarm_size = (int)swarmSizeHanlder.value;
        for (int i = 0; i < swarm_size; i++)
        {
            var rand_offset = new Vector3(Random.Range(-start_separation, start_separation), Random.Range(-start_separation/3f, start_separation/3f), Random.Range(-start_separation, start_separation))*start_separation;
            gameObjects.Add(UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Gentle/Gentle"), transform.position + rand_offset, Random.rotation));
        }
    }

    public List<GameObject> getBoidList()
    {
        return gameObjects;
    }
}
