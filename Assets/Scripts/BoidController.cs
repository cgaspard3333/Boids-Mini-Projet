using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController : MonoBehaviour
{
    public List<GameObject> listOfBoid;
    private BoidSpawner boidSpawner;
    private BoidUI boidUI;
    // Start is called before the first frame update
    void Start()
    {
        listOfBoid = new List<GameObject>();
        boidSpawner = GetComponent<BoidSpawner>();
        boidUI = GetComponent<BoidUI>();
        listOfBoid = boidSpawner.getBoidList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
