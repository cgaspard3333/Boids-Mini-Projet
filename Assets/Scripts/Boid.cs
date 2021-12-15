using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boid : MonoBehaviour
{
    private GameObject spawn;
    private GameObject[] theFlock;

    //-----------------------------------------------------------------------------
    // Const Data
    //-----------------------------------------------------------------------------
    private static readonly float mRadiusSquaredDistance = 5.0f;
    private static readonly float mMaxVelocity = 15.0f;
    private static readonly float mMaxCubeExtent = 80.0f;
    private static readonly float mMaxCubeExtentX = 80.0f;
    private static readonly float mMaxCubeExtentY = 50.0f;

    public float separationWeight = 0.8f;
    public float alignmentWeight = 0.5f;
    public float cohesionWeight = 0.7f;

    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    private Vector3 mVelocity = new Vector3();
    float velocity;
    private float neighborDist = 5.0f;


    //-----------------------------------------------------------------------------
    private void Reposition()
    {
        Vector3 position = transform.position;

        if( position.x >= mMaxCubeExtentX )
        {
            position.x = mMaxCubeExtentX - 0.2f;
            mVelocity.x *= -1;
        }
        else if( position.x <= -mMaxCubeExtentX )
        {
            position.x = -mMaxCubeExtentX + 0.2f;
            mVelocity.x *= -1;
        }

        if( position.y >= mMaxCubeExtent )
        {
            position.y = mMaxCubeExtent - 0.2f;
            mVelocity.y *= -1;
        }
        else if( position.y <= -mMaxCubeExtent )
        {
            position.y = -mMaxCubeExtent + 0.2f;
            mVelocity.y *= -1;
        }

        if( position.z >= mMaxCubeExtent )
        {
            position.z = mMaxCubeExtent - 0.2f;
            mVelocity.z *= -1;
        }
        else if( position.z <= -mMaxCubeExtent )
        {
            position.z = -mMaxCubeExtent + 0.2f;
            mVelocity.z *= -1;
        }

        transform.forward = mVelocity.normalized;
        transform.position = position;
    }

    //-----------------------------------------------------------------------------
    // Flocking Behavior
    //-----------------------------------------------------------------------------
    private Vector3 FlockingBehaviour()
    {
        Vector3 cohesionVector = new Vector3();
        Vector3 separateVector = new Vector3();
        Vector3 alignmentVector = new Vector3();

        int count = 0;

        for( int index = 0; index < theFlock.Length; index++ )
        {
            if( this != theFlock[ index ] )
            {
                float distance = ( transform.position - theFlock[ index ].transform.position ).sqrMagnitude;

                if( distance > 0 && distance < mRadiusSquaredDistance )
                {
                    cohesionVector += theFlock[ index ].transform.position;
                    separateVector += theFlock[ index ].transform.position - transform.position;
                    alignmentVector += theFlock[ index ].transform.forward;

                    count++;
                }
            }
        }

        if( count == 0 )
        {
            return Vector3.zero;
        }

        // revert vector
        // separation step
        separateVector /= count;
        separateVector *= -1;

        // forward step
        alignmentVector /= count;

        // cohesione step
        cohesionVector /= count;
        cohesionVector = ( cohesionVector - transform.position );

        // Add All vectors together to get flocking
        Vector3 flockingVector = ( ( separateVector.normalized * separationWeight ) +
                                    ( cohesionVector.normalized * cohesionWeight ) +
                                    ( alignmentVector.normalized * alignmentWeight ) );

        return flockingVector;
    }

    public void IncreaseVariables(int variable)
    {
        if(variable == 0)
        {
            this.velocity += 1f;
            print("Velocity now is " + this.velocity.ToString());
        }
        else if(variable == 1)
        { 
            this.neighborDist += 1f;
            print("Minimal distance between boids now is " + this.neighborDist.ToString());
        }
    }

    public void DecreaseVariables(int variable)
    {
        if(variable == 0)
        {
            if(this.velocity == 0f)
                print("Speed cannot go lower than 0.");
            else
            {
                this.velocity -= 1f;
                print("Velocity now is " + this.velocity.ToString());
            }
        }
        else if(variable == 1)
        {
            if(this.neighborDist == 0f)
                print("Distance between boids cannot be lower than 0.");
            else
            {
                this.neighborDist -= 1f;
                print("Minimal distance between boids now is " + this.neighborDist.ToString());
            }
        }
    }

    void Start()
    {    
        Spawn spawnScript = GameObject.FindObjectOfType(typeof(Spawn)) as Spawn;
        theFlock = spawnScript.getFlockEntities();
        mVelocity = transform.forward;
        mVelocity = Vector3.ClampMagnitude( mVelocity, mMaxVelocity );
    } 

    void Update()
    {
        //BoidBehavior();
        mVelocity += FlockingBehaviour();

        mVelocity = Vector3.ClampMagnitude( mVelocity, mMaxVelocity );

        transform.position += mVelocity * Time.deltaTime;

        transform.forward = mVelocity.normalized;

        Reposition();
    }


}
