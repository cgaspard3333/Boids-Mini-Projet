using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidUI : MonoBehaviour
{
    private Slider swarmSizeHanlder;
    private Slider CohesionHanlder;
    private Slider SeparationeHanlder;
    private Slider AlignementHanlder;

    private int swarm_size = 10;
    private float start_separation = 12f;

    private List<GameObject> gameObjects = new List<GameObject>();


    public void set_gameObjects_List(List<GameObject> gameObjectsList)
    {
        this.gameObjects = gameObjectsList;
    }
    private void reduceSwarmSize()
    {
        int difference = swarm_size - (int)swarmSizeHanlder.value;

        for (int i = 0; i < difference; i++)
        {
            int random_id = Random.Range(0, gameObjects.Count);
            Destroy(gameObjects[random_id]);
            gameObjects.RemoveAt(random_id);
        }
        swarm_size = (int)swarmSizeHanlder.value;
    }

    private void increaseSwarmSize()
    {
        int difference = (int)swarmSizeHanlder.value - swarm_size;

        for (int i = 0; i < difference; i++)
        {
            var rand_offset = new Vector3(Random.Range(-start_separation, start_separation), Random.Range(-start_separation / 3f, start_separation / 3f), Random.Range(-start_separation, start_separation)) * start_separation;
            gameObjects.Add(UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Gentle/Gentle"), transform.position + rand_offset, Random.rotation));
        }
        swarm_size = (int)swarmSizeHanlder.value;
    }
    public void OnchangeValueSize()
    {
        if ((int)swarmSizeHanlder.value < swarm_size)
        {
            reduceSwarmSize();
        }
        if ((int)swarmSizeHanlder.value > swarm_size)
        {
            increaseSwarmSize();
        }
    }

    public void OnChangeValue()
    {
        foreach (Transform child in transform)
        {
            child.transform.GetChild(0).GetComponent<BoidEntity>().alpha = (float)CohesionHanlder.value;
            child.transform.GetChild(0).GetComponent<BoidEntity>().beta = (float)SeparationeHanlder.value;
            child.transform.GetChild(0).GetComponent<BoidEntity>().gamma = (float)AlignementHanlder.value;
            child.transform.GetChild(0).GetComponent<SphereCollider>().radius = 5 + CohesionHanlder.value;
            /*child.transform.GetChild(0).GetComponent<SphereCollider>().radiusRepulse = 5 + CohesionHanlder.value;*/
        }
    }
}
