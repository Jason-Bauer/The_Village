using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MovementForces : MonoBehaviour
{
	public Vector3 position;//position of the object
	public Vector3 direction; //where we are facing
	public Vector3 velocity;//current velocity moving the object
	public Vector3 acceleration;//sum of forces acting on the object
	
	public Vector3 wind = new Vector3(0.0f, 0.0f, 0.0f);//force of wind
	public Vector3 force1 = new Vector3 (0.0f, 0.0f, 0.0f);
	public Vector3 gravity = new Vector3(0.0f, 0.0f, 0.0f);//force of gravity
	Vector3 seekingForce;
	public float mass = 1.0f;// mass of the object
	public float maxSpeed = 0.010f;
   public List<GameObject> targets = new List<GameObject>();
    public GameObject closest = null;
	private BehaviourManager behaviourMngr; //behaviour manager to calculate forces
    public GameObject gameMngr;

    private Vector3 worldSize; //store the world size
    public GameObject townhall;
    public GameObject thisBuilding;
	public GameObject target;

	public void SetTarget(GameObject theTarget)
	{
		target = theTarget;
	}

	// Use this for initialization
	void Start ()
	{
        maxSpeed = UnityEngine.Random.Range(0.01f, 0.08f);
        townhall = GameObject.Find("GameManager").GetComponent<TileManager>().Tiles[GameObject.Find("GameManager").GetComponent<TileManager>().numrows][GameObject.Find("GameManager").GetComponent<TileManager>().numcolumns].GetComponent<Tile>().Buildingattached; 
		gameMngr = GameObject.Find("GameManager");
		if(null == gameMngr)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": Requires a GameManager object in the scene.");
			Debug.Break();
		}
		position = transform.position;
		behaviourMngr = gameMngr.GetComponent<BehaviourManager>();

		//Check that mas is initialized to something. Mass cannot be negative
		if (mass <= 0.0f)
		{
			mass = 0.01f;
		}
        target = townhall;
    }
	
	// Update is called once per frame
	void Update ()
	{
       
		//ApplyForce (wind);//Apply a force to our object
		//ApplyForce (force1);
		UpdatePosition (); //Update the position based on forces
		//worldSize = behaviourMngr.worldSize;//Check the size of the world
		//BounceBoundry();
        //Check the border of the terrain
		SetTransform();//Set the transform before render
	}

    // Update the position based on the velocity and acceleration
    void UpdatePosition()
    {
        //Step 0: update position to current tranform
        position = transform.position;
        closest = target;
        //Step 0.5: Calculate desire velocity
        try
        {
            if (gameObject.tag == "Player")
            {
                seekingForce = pursuit();
            }
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
        catch (MissingReferenceException e)
        {
            target = townhall;
            thisBuilding = null;
           // gameMngr.GetComponent<Gamemanager>().Villagers -= 4;
            GameObject.Find("GameManager").GetComponent<Gamemanager>().Villagers -= 4;
            return;
        }
    }
    public GameObject findclosest()
    {
        GameObject closest = townhall;
        float greatestdist = 1000;
        foreach (GameObject a in targets)
        {
            float possible =Vector3.Distance(this.transform.position, a.transform.position);

            if (possible < greatestdist)
            {
                greatestdist = possible;
                closest = townhall;
            }
        }
        greatestdist = 1000;
        return closest;
    }
    Vector3 Seek(Vector3 position)
    {
        Vector3 desiredVelocity=new Vector3(0,0,0);
		//Step 1: Calculate the desired velocity (unclamped and unnormalize)
		
			//this is the target position minus my current position
			 desiredVelocity = position - this.position;
			
			
			//Step 2: Calculate the maximum speed
		
		desiredVelocity = Vector3.ClampMagnitude (desiredVelocity, maxSpeed);
		desiredVelocity.y = 0;
		//Step 3: Calculate Steering force
		Vector3 steeringForce = desiredVelocity - velocity;

		//Step 4: return that force
		return steeringForce;
	}
	Vector3 Flee(Vector3 position)
	{
        Vector3 desiredVelocity=new Vector3(0,0,0);
		
			//this is the target position minus my current position
			 desiredVelocity = this.position- position;
			
			//Step 2: Calculate the maximum speed
		
		desiredVelocity = Vector3.ClampMagnitude (desiredVelocity, maxSpeed);
		desiredVelocity.y = 0;
		Vector3 steeringForce = desiredVelocity - velocity;
		//Step 4: return that force
		return steeringForce;
    }
    Vector3 pursuit()
    {
        return Seek(closest.transform.position);
    }

    Vector3 evasion()
    {
        return Flee(closest.transform.position);
    }

    public Vector3 fowardposition()
    {
        return (position + velocity);
    }
   // void OnRenderObject()
   // {
   //     //velocity
   //     GL.PushMatrix();
   //     behaviourMngr.red.SetPass(0);
   //     GL.Begin(GL.LINES);
   //     GL.Vertex(position);
   //     GL.Vertex(position + direction * 5.0f);
   //     GL.End();
   //
   //
   //     //To target
   //     if (null != targets)
   //     {
   //         foreach (GameObject v in targets)
   //         {
   //             behaviourMngr.white.SetPass(0);
   //             GL.Begin(GL.LINES);
   //             GL.Vertex(position);
   //             GL.Vertex(v.transform.position);
   //             GL.End();
   //         }
   //     }
   //
   //     GL.PopMatrix();
   // }
    //Apply a force to the vehicle
    void ApplyForce(Vector3 force)
	{
		//F = M * A
		//F / M = M * A / M
		//F / M = A * (M / M)
		//F / M = A * 1
		//A = F / M
		acceleration += force / mass;
	}

	//Apply friction to the vehicle based on the coefficient
	void ApplyFriction(float coeff)
	{
		// Step 1: Oposite velocity
		Vector3 friction = velocity * -1.0f;
		// Step 2: Normalize so is independent of velocity
		friction.Normalize ();
		// Step 3: Multiply by coefficient
		friction = friction * coeff;
		// Step 4: Add friction to acceleration
		acceleration += friction;
	}
	
	//Apply the trasformation
	void SetTransform()
	{
		transform.position = position;
		transform.right = direction;
	}
    
    // Position the object inside of the screen
    void WrapBoundry()
	{	
		//Check within X
		//if(position.x > worldSize.x)
		//	position.x = 0;
		//else if(position.x < 0)
		//	position.x = worldSize.x;
		//
		////check within Z
		//if(position.z > worldSize.z)
		//	position.z = 0;
		//else if(position.z < 0)
		//	position.z = worldSize.z;
	}

	void BounceBoundry()
	{
		//Check within X
		//if (position.x > worldSize.x)
		//{
        //    position.x = worldSize.x;
        //    velocity.x *= -1.0f;
		//}
		//else if (position.x < 0)
		//{
		//	velocity.x *= -1.0f;
        //    position.x = 0;
        //
        //}
		//
		////check within Z
		//if (position.z > worldSize.z)
		//{
        //    position.z = worldSize.z;
        //       velocity.z *= -1.0f;
		//}
		//else if (position.z < 0)
		//{
        //    position.z = 0;
        //    velocity.z *= -1.0f;
		//}
	}

    void centerforce()
    {
        //Check within X
      //  if (position.x > worldSize.x-5)
      //  {
      //      
      //      velocity.x-= -.1f;
      //  }
      //  else if (position.x < 5)
      //  {
      //      velocity.x -= -.1f;
      //    
      //
      //  }
      //
      //  //check within Z
      //  if (position.z > worldSize.z-5)
      //  {
      //      
      //      velocity.z -= -.1f;
      //  }
      //  else if (position.z < 5)
      //  {
      //      
      //      velocity.z -= -.10f;
      //  }
    }
}
