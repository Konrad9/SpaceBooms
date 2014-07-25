using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemyAI : MonoBehaviour {
	/// <summary>
	/// Used to grab the checkpoint array that is attached to the main camera 
	/// </summary>
	GameObject mainCamera; 
	
	/// <summary>
	/// Used to grab information about the player's ship. For now this is just the position. 
	/// </summary>
	GameObject playerShip; 
	
	public GameObject bullet; 
	
	public Texture healthBar; 
	
	public int i_currentHealth;
	
	private string currentHealth; 
	
	Vector3 healthBarLoc; 
	
	public Vector3 firingVector; 
	
	private float firingTimer; 
	
	private List<GameObject> bulletList; 
	
	private Vector3 sampleSpawn; 
	
	private int activeBullet; 
	
	// Use this for initialization
	
	public GameObject[] pathPoints; 

	private GameObject[] rndmPathPoints;
	
	private GameObject tempPathpoint;
	
	public int currentPathIndex; 
	
	private Vector3 movementDirection;
	
	public float speed = 0.5f; 
	
	
	void Start () {
		
		mainCamera = GameObject.Find("Main Camera");
		
		playerShip = GameObject.Find("player"); 
		
		
		pathPoints = mainCamera.GetComponent<checkpoints>().checkPoints; 
		rndmPathPoints = pathPoints; 
		
		shuffleCheckpoints();
		
		//speed = 20 * Time.deltaTime; 
		
		currentPathIndex = 1; 
		
		bulletList = new List<GameObject>();
		
		sampleSpawn = new Vector3(500, 500, 0);
		
		i_currentHealth = 100; 
		
		activeBullet = 0; 
		
		firingTimer = 0; 
	
		/*for (int i = 0; i < 15; i++)
		{
			GameObject simpleBulletBuffer = Instantiate(bullet, sampleSpawn, Quaternion.identity) as GameObject;			
			
			bulletList.Add(simpleBulletBuffer); 
		}*/
		movementDirection = (pathPoints[currentPathIndex].transform.position - transform.position).normalized; 
	}
	
	void shuffleCheckpoints()
	{
		//Random rnd = new Random();
		int arrLength = pathPoints.Length; 
		int[] tmpPoints = new int[arrLength];
		
		//Fisher-Yates shuffle
		while (arrLength > 1)
		{
			arrLength--;
			int h = Random.Range(0, arrLength);
			tempPathpoint = pathPoints[h];
			pathPoints[h] = pathPoints[arrLength];
			pathPoints[arrLength] = tempPathpoint; 
		}
		
	}
	
	void OnCollisionEnter(Collision collision)
	{
		///if the ship is hit by a bullet
		if (collision.collider.tag == "bullet")
		{
			i_currentHealth -= 50;
			if ((i_currentHealth -= 75) <= 0 )
			{
				///the ship got hit, blow it up
				destroyShip();
			}
			
		}		
		
	}
	
	private void destroyShip()
	{
		///will hopefully hook up a destroy animation of some kind later
		Destroy(gameObject);
	}
	
	private void OnTriggerEnter(Collider collider)
	{
		///cycle through checkpoints
		if (collider.gameObject == pathPoints[currentPathIndex])
		{
			
			currentPathIndex+= 1; 
			movementDirection = (pathPoints[currentPathIndex].transform.position - transform.position).normalized; 
		}
	}
	
	
	private void OnGUI ()
	{
		//GUI.TextArea(new Rect(healthBarLoc.y, healthBarLoc.x, 100, 100), "HP: " + currentHealth);
	}
	
	private void FireWeapon()
	{	
		///Debug.Log(playerShip.GetComponent<shipControl>().shipPos()); 
		GameObject simpleBulletBuffer = Instantiate(bullet, sampleSpawn, Quaternion.identity) as GameObject;
		
		///grabs the position of the player object which is slightly off as to make the aim of the enemy ships appear imperfect 		
		
		firingVector = ((playerShip.GetComponent<shipControl>().shipPos()) - transform.position).normalized;
		
		///start the projectile in the position of the ship, might be slightly altered later to be more aesthetic
		simpleBulletBuffer.transform.position = transform.position;
		
		///40 seems like a good starting speed. might up it for lighter attacks or lower it for heavier attacks
		//simpleBulletBuffer.rigidbody.velocity = firingVector * 40; 
		
		
		//simpleBulletBuffer.collider.enabled = true; 
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		speed = 40 * Time.deltaTime; 
		//currentHealth = i_currentHealth.ToString();
		
		//healthBarLoc = Camera.main.WorldToScreenPoint(transform.position); 
				
		///fire every 1.75 seconds
		if ((firingTimer += Time.deltaTime) >= 1.75f)
		{
			FireWeapon();
			firingTimer = 0; 
		}
		
		///move the ship
		transform.position += movementDirection * speed; 
		
	}
}
