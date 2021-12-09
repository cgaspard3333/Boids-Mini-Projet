using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spawner : MonoBehaviour
{
    private int swarm_size = 10; 
    public float start_separation = 10f;
    public Slider swarmSizeHanlder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSwarm()
    {

        swarm_size = (int)swarmSizeHanlder.value;
        for (int i = 0; i < swarm_size; i++)
        {
            var rand_offset = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
            GameObject NewSpawn = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Entite"), transform.position + rand_offset ,Random.rotation);


        }

    }

}
