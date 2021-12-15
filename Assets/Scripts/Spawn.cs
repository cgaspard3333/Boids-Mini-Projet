using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public static Spawn instance =null;
    public GameObject controlMenu;
    public GameObject canvas;

    public int flockSize = 10;

    public static bool isGamePaused = false;

    public GameObject flockEntityPrefab;

    private GameObject[] flockEntities;

    [Range(-10000, 10000)]
    public int randomSeed = 0;
    private System.Random randomGenerator;

    private Vector3[] flockPositions;
    private Vector3[] flockRotations;

    public float separationWeight = 0.8f;
    public float alignmentWeight = 0.5f;
    public float cohesionWeight = 0.7f;

    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public Slider separationSlider = null;
    public Slider alignmentSlider = null;
    public Slider cohesionSlider = null;

    public GameObject[] getFlockEntities(){
        return this.flockEntities;
    }

    Boid[] boids;

    int getFlockSize()
    {
        return this.flockSize;
    }

    void setFlockSize(int newSize)
    {
        this.flockSize = newSize;
    }

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
        instance = this ;
        generateFlockEntities();
        controlMenu = GameObject.Find("ControlMenu");
        //canvas.SetActive(false);
        controlMenu.SetActive(false);
        boids = FindObjectsOfType(typeof(Boid)) as Boid[];
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(isGamePaused)
                Resume(); 
            else
                Pause();
        }
    }

    

    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public void Setup()
    {
        separationSlider.value =separationWeight;
        separationSlider.onValueChanged.AddListener( ( value ) => separationWeight = value );

        alignmentSlider.value = alignmentWeight;
        alignmentSlider.onValueChanged.AddListener( ( value ) => alignmentWeight = value );

        cohesionSlider.value = cohesionWeight;
        cohesionSlider.onValueChanged.AddListener( ( value ) => cohesionWeight = value );
	}

    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        controlMenu.SetActive(false);

    }

    public void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        controlMenu.SetActive(true);
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

    public void SubmitField()
    {
        InputField flockSizeField = GameObject.Find("FlockSize").GetComponent<InputField>();
        int newFlockSize = int.Parse(flockSizeField.text);
        setFlockSize(newFlockSize);
        OnValidate();
    }

    public void Increaser(int variable)
    {    
        foreach(var boid in boids)
        {
            boid.IncreaseVariables(variable);
        }
    }

    public void Decreaser(int variable)
    {
        foreach(var boid in boids)
        {
            boid.DecreaseVariables(variable);
        }
    }

    private void OnValidate()
    {
        /* When a script setting is changed */
        generateFlockPositions(flockSize);
    }
}
