using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class shipControl : MonoBehaviour {
	
	
	private Vector3 simpleBulletVelocity; 
	
	private int bulletsInFlight; 
	
	private int bulletType; 
	
	private Vector3 sampleSpawn; 
	
	public GameObject simpleBullet; 
	public GameObject vulcanBullet; 
	
	private int currentBullet; 
	public int currentvulcanBullet;
	
	public Vector3 tempPos; 
	
	private float wepTimer; 
	
	public float rndmNum; 
	
	/// <summary>
	/// The horizontal momentum.
	/// + = right, - = left
	/// </summary>
	private float horizontalMomentum; 
	
	/// <summary>
	/// + = up, - = down
	/// </summary>
	private float verticalMomentum;
	
	private float horizontalAdd;
	private float verticalAdd; 
	
	
	private List<GameObject> simpleBulletArray;
	private List<GameObject> vulcanBulletArray;
	
	// Use this for initialization
	void Start () {
		wepTimer = 0; 
		
		bulletType = 0;
		
		bulletsInFlight = 0; 
		
		simpleBulletVelocity = new Vector3(0, 75, 0);
		
		currentBullet = 0; 
		currentvulcanBullet = 0; 
		
		sampleSpawn = new Vector3(1000, 1000, 0);		
		
		simpleBulletArray = new List<GameObject>(); 
		vulcanBulletArray = new List<GameObject>(); 
		
		for (int i = 0; i < 15; i++)
		{
			GameObject simpleBulletBuffer = Instantiate(simpleBullet, sampleSpawn, Quaternion.identity) as GameObject;
			
			simpleBulletArray.Add(simpleBulletBuffer);
		}
		for (int i = 0; i < 251; i++)
		{
			GameObject vulcanBulletBuffer = Instantiate(vulcanBullet, sampleSpawn, Quaternion.identity) as GameObject;
			
			vulcanBulletArray.Add(vulcanBulletBuffer);
		}
	
		horizontalMomentum = 0.0f;
		verticalMomentum = 0.0f;
	}
	
	public Vector3 shipPos()
	{
		//Provides the enemy ships with slightly off positions as to make the weapons fired appear to be based on imperfect sensors
		 tempPos = new Vector3();
		//tempPos.x = gameObject.transform.position.x + Random.Range(-0.5f, 0.5f);
		//tempPos.y = gameObject.transform.position.y + Random.Range(-0.5f, 0.5f);
		//tempPos.z = gameObject.transform.position.z + Random.Range(-0.5f, 0.5f);
		rndmNum = Random.Range(-10, 10); 
		tempPos.x = gameObject.transform.position.x + rndmNum;
		tempPos.y = gameObject.transform.position.y + rndmNum;
		tempPos.z = 5;
		
		return tempPos;
		
	}
	void Update()
	{
		
		
		///if (Input.GetKeyDown(KeyCode.Space))
		//{
			//if (bulletsInFlight <= 5)
			//fireWeapon(); 			
		//}
		
		
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			if (transform.position.x >= -200)
			{
				if (horizontalAdd <= 0.75f && horizontalAdd >= -0.75f)
				{
					horizontalAdd -= 1;
				}	
			}
			else
			{
				horizontalMomentum = 0; 	
			}
		}		
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			if (transform.position.x <= 200)
			{
				if (horizontalAdd <= 0.75f && horizontalAdd >= -0.75f)
				{
					horizontalAdd += 1;
				}
			}			
			else
			{
				horizontalMomentum = 0; 	
			}
		}
		else
		{
			horizontalAdd = 0;
			horizontalMomentum = decreaseVelocity(horizontalMomentum);
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			if (transform.position.y <= -38)
			{
			if (verticalAdd <= 0.75f && verticalAdd >= -0.75f)
			{
				verticalAdd++;
			}
			}
		}
		
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			if (transform.position.y >= -90)
			{
				if (verticalAdd <= 0.75f && verticalAdd >= -0.75f)
				{
					verticalAdd--;
				}
			}
		}
		else
		{
			verticalAdd = 0;
			verticalMomentum = decreaseVelocity(verticalMomentum); 
		}
		
		
		
		
		if (Input.GetKey(KeyCode.Space))
		{
			if (wepTimer + Time.deltaTime >= 0.01)
			{
				fireWeapon();
				wepTimer = 0; 
			}
		}
		
		
		verticalMomentum += verticalAdd;
		horizontalMomentum += horizontalAdd;
		
		gameObject.rigidbody.velocity = new Vector2(horizontalMomentum, verticalMomentum);
		
		//horizontalMomentum = decreaseVelocity(horizontalMomentum);
		//verticalMomentum = decreaseVelocity(verticalMomentum); 
		
		
	}
	
	float decreaseVelocity(float momentum)
	{
		if (momentum < 0f)
		{
			return momentum + 1.0f;		
		}
		else if (momentum > 0f)
		{
			return momentum - 1.0f; 
		}
		
		return momentum; 
		
	}
	
	void fireWeapon()
	{
		///instantiate weapon fire	
		///bullet type:
		///0 = standard torpedo
		///1 = vulcan cannon 
		///2 = laser
		///3 = arc field 
		///4 = gravity mines
		///5 = rail gun 
		bulletType = 2; 
		switch(bulletType)
		{
		case 1:
		{
			if (currentBullet < 15)
			{		
				simpleBulletArray[currentBullet].rigidbody.position = gameObject.rigidbody.position;
		
				simpleBulletArray[currentBullet].rigidbody.velocity = simpleBulletVelocity;
			
				simpleBulletArray[currentBullet].GetComponent<bulletReset>().inFlight = true; 
		
				currentBullet++;
			}
			else
				currentBullet = 0; 
			
			break;
		}
		case 2:
			{
			if (currentvulcanBullet < 250)
			{		
				vulcanBulletArray[currentvulcanBullet].rigidbody.position = gameObject.rigidbody.position;
		
				vulcanBulletArray[currentvulcanBullet].rigidbody.velocity = simpleBulletVelocity;
			
				vulcanBulletArray[currentvulcanBullet].GetComponent<bulletReset>().inFlight = true; 
		
				currentvulcanBullet++;
			}
			else
				currentvulcanBullet = 0; 
			
			
			break; 
		}
		default: 
		{
			break;
		}
			
		}
		
		
	}
}
