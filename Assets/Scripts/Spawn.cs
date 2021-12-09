using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public int flockSize = 10;

    public GameObject flockEntityPrefab;

    private GameObject[] flockEntities;

    [Range(-10000, 10000)]
    public int randomSeed = 0;
    private System.Random randomGenerator;

    private Vector3[] flockPositions;
    private Vector3[] flockRotations;


    Vector3 getRandomVector3()
    {
        return new Vector3((float)randomGenerator.NextDouble() - .5f,
                           (float)randomGenerator.NextDouble() - .5f,
                           (float)randomGenerator.NextDouble() - .5f);
    }

    void generateFlockPositions(int flockSize)
    {
        randomGenerator = new System.Random(randomSeed);

        flockPositions = new Vector3[flockSize];
        flockRotations = new Vector3[flockSize];

        for (int i = 0; i < flockSize; i++)
        {
            flockPositions[i] = getRandomVector3();
            flockRotations[i] = getRandomVector3();
        }
    }

    void generateFlockEntities()
    {
        flockEntities = new GameObject[flockSize];
        
        for (int i = 0; i < flockSize; i++)
        {
            GameObject newEntity = Instantiate(flockEntityPrefab,
                                               Vector3.zero,
                                               Quaternion.identity);

            newEntity.transform.position = this.transform.position + Vector3.Scale(this.transform.localScale, flockPositions[i]);
            newEntity.transform.rotation = Quaternion.Euler(flockRotations[i]);

            flockEntities[i] = newEntity;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        generateFlockEntities();
    }

    // Update is called once per frame
    void Update()
    {
        // TODO
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 pos = this.transform.position;
        Vector3 scale = this.transform.lossyScale;


        /* Draw bounding box */
        /* Gizmos use global world coordinates */
        Gizmos.DrawWireCube(pos, scale);

        Gizmos.DrawRay(pos - scale / 2, scale);
        Gizmos.DrawRay(pos - Vector3.Reflect(scale, Vector3.up) / 2, Vector3.Reflect(scale, Vector3.up));

        scale = Vector3.Reflect(scale, Vector3.left);

        Gizmos.DrawRay(pos - scale / 2, scale);
        Gizmos.DrawRay(pos - Vector3.Reflect(scale, Vector3.up) / 2, Vector3.Reflect(scale, Vector3.up));

        /* Draw flock entities */
        for (int i = 0; i < flockSize; i++)
        {
            Vector3 localPos = flockPositions[i];
            Vector3 realPos = pos + Vector3.Scale(localPos, this.transform.lossyScale);

            Gizmos.color = Color.Lerp(Color.cyan, Color.magenta, localPos.sqrMagnitude * 2);
            Gizmos.DrawCube(realPos, Vector3.one);
        }
    }

    private void OnValidate()
    {
        /* When a script setting is changed */
        generateFlockPositions(flockSize);
    }
}
