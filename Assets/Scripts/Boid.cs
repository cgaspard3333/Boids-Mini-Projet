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
    private static readonly float mMaxVelocity = 15.0f;
    private static readonly float mMaxCubeExtentZ = 100.0f;
    private static readonly float mMaxCubeExtentX = 100.0f;
    private static readonly float mMaxCubeExtentY = 80.0f;

    public float separationWeight;
    public float alignmentWeight;
    public float cohesionWeight;

    //-----------------------------------------------------------------------------
    // Data
    //-----------------------------------------------------------------------------
    public float mRadiusSquaredDistance = 1.0f;
    public Vector3 mVelocity = new Vector3();

    //-----------------------------------------------------------------------------
    private void Reposition()
    {
        Vector3 position = transform.position;

        if (position.x >= mMaxCubeExtentX)
        {
            position.x = mMaxCubeExtentX - 0.2f;
            mVelocity.x *= -1;
        }
        else if (position.x <= -mMaxCubeExtentX)
        {
            position.x = -mMaxCubeExtentX + 0.2f;
            mVelocity.x *= -1;
        }

        if (position.y >= mMaxCubeExtentY)
        {
            position.y = mMaxCubeExtentY - 0.2f;
            mVelocity.y *= -1;
        }
        else if (position.y <= 0)
        {
            position.y = 0.2f;
            mVelocity.y *= -1;
        }

        if (position.z >= mMaxCubeExtentZ)
        {
            position.z = mMaxCubeExtentZ - 0.2f;
            mVelocity.z *= -1;
        }
        else if (position.z <= -mMaxCubeExtentZ)
        {
            position.z = -mMaxCubeExtentZ + 0.2f;
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

        separationWeight = Spawn.instance.separationWeight;
        alignmentWeight = Spawn.instance.alignmentWeight;
        cohesionWeight = Spawn.instance.cohesionWeight;

        int count = 0;

        for (int index = 0; index < theFlock.Length; index++)
        {
            if (this != theFlock[index])
            {
                float distance = (transform.position - theFlock[index].transform.position).sqrMagnitude;

                if (distance > 0 && distance < mRadiusSquaredDistance)
                {
                    cohesionVector += theFlock[index].transform.position;
                    separateVector += theFlock[index].transform.position - transform.position;
                    alignmentVector += theFlock[index].transform.forward;

                    count++;
                }
            }
        }

        if (count == 0)
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
        cohesionVector = (cohesionVector - transform.position);

        // Add All vectors together to get flocking
        Vector3 flockingVector = ((separateVector.normalized * separationWeight) +
                                    (cohesionVector.normalized * cohesionWeight) +
                                    (alignmentVector.normalized * alignmentWeight));

        return flockingVector;
    }

    public void IncreaseVariables(int variable)
    {
        if (variable == 0)
        {
            this.mVelocity.x += 0.1f;
            this.mVelocity.y += 0.1f;
            this.mVelocity.z += 0.1f;
            print("Velocity now is " + this.mVelocity.ToString());
        }
        else if (variable == 1)
        {
            this.mRadiusSquaredDistance += 0.2f;
            print("Minimal distance between boids now is " + this.mRadiusSquaredDistance.ToString());
        }
    }

    public void DecreaseVariables(int variable)
    {
        if (variable == 0)
        {
            this.mVelocity.x -= 0.1f;
            this.mVelocity.y -= 0.1f;
            this.mVelocity.z -= 0.1f;
            if(this.mVelocity.x < 0)
                this.mVelocity.x = 0;
            if(this.mVelocity.y < 0)
                this.mVelocity.y = 0;
            if(this.mVelocity.z < 0)
                this.mVelocity.z = 0;
            print("Velocity now is " + this.mVelocity.ToString());
        }
        else if (variable == 1)
        {
            this.mRadiusSquaredDistance -= 0.2f;
            if(this.mRadiusSquaredDistance < 0)
                this.mRadiusSquaredDistance = 0;    
            print("Minimal distance between boids now is " + this.mRadiusSquaredDistance.ToString());
        }
    }

    void Start()
    {
        Spawn spawnScript = GameObject.FindObjectOfType(typeof(Spawn)) as Spawn;
        theFlock = spawnScript.getFlockEntities();
        mVelocity = transform.forward;
        mVelocity = Vector3.ClampMagnitude(mVelocity, mMaxVelocity);
    }

    void Update()
    {
        //BoidBehavior();
        mVelocity += FlockingBehaviour();

        //mVelocity = Vector3.ClampMagnitude( mVelocity, mMaxVelocity );

        transform.position += mVelocity * Time.deltaTime;

        transform.forward = mVelocity.normalized;

        Reposition();
    }


}
