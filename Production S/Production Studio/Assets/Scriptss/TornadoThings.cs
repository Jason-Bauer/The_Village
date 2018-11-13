using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoThings : MonoBehaviour {
    public Vector3 position;//position of the object
    public Vector3 direction; //where we are facing
    public Vector3 velocity;//current velocity moving the object
    public Vector3 acceleration;//sum of forces acting on the object
    public float maxSpeed = 0.050f;
    public float mass = 1.0f;// mass of the object
    public GameObject target;
    Vector3 seekingForce;
    private IEnumerator coroutine;
    public CapsuleCollider collider;
    void Start () {
        collider = this.gameObject.GetComponent<CapsuleCollider>();
        coroutine = WaitAndChange();
        StartCoroutine(coroutine);
    }
	
	// Update is called once per frame
	void Update () {
        UpdatePosition(); //Update the position based on forces
                          //worldSize = behaviourMngr.worldSize;//Check the size of the world
        this.gameObject.transform.rotation = Quaternion.identity;
                          //BounceBoundry();
                          //Check the border of the terrain
        SetTransform();//Set the transform before render
        if (this.transform.position.x== target.transform.position.x&& this.transform.position.z == target.transform.position.z)
        {
            Destroy(this.gameObject);
        }
    }
    private IEnumerator WaitAndChange()
    {
       
        yield return new WaitForSeconds(60.0f);
        Destroy(this.gameObject);

    }
    void SetTransform()
    {
        transform.position = position;
        //transform.right = direction;
    }
    void UpdatePosition()
    {
        //Step 0: update position to current tranform
        position = transform.position;

        //Step 0.5: Calculate desire velocity

       
        seekingForce = pursuit();
        
        ApplyForce(seekingForce);
        //Step 1: Add Acceleration to Velocity * Time
        velocity += acceleration * Time.deltaTime * 10;
        //Step 2: Add vel to position * Time
        position += Vector3.ClampMagnitude(velocity * Time.deltaTime * 10, maxSpeed);
        //Step 3: Reset Acceleration vector
        acceleration = Vector3.zero;
        //Step 4: Calculate direction (to know where we are facing)
        direction = velocity.normalized;
    }
    Vector3 Seek(Vector3 position)
    {
        Vector3 desiredVelocity = new Vector3(0, 0, 0);
        //Step 1: Calculate the desired velocity (unclamped and unnormalize)

        //this is the target position minus my current position
        desiredVelocity = position - this.position;


        //Step 2: Calculate the maximum speed

        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxSpeed);
      //  desiredVelocity.y = 0;
        //Step 3: Calculate Steering force
        Vector3 steeringForce = desiredVelocity - velocity;

        //Step 4: return that force
        return steeringForce;
    }
    Vector3 pursuit()
    {
        return Seek(new Vector3( target.transform.position.x, target.transform.position.y+1, target.transform.position.z));
    }
    void ApplyForce(Vector3 force)
    {
        //F = M * A
        //F / M = M * A / M
        //F / M = A * (M / M)
        //F / M = A * 1
        //A = F / M
        acceleration += force / mass;
    }
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision detected");
        
            if (col.gameObject.GetComponent<Tile>().buildingtype != 0)
            {
                Destroy(col.gameObject.GetComponent<Tile>().Buildingattached);
                Debug.Log("Tornado destroyed something");
            }
        
    }
}
