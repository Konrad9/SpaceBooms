using UnityEngine;
using System.Collections;

public class enemySpawner : MonoBehaviour {
	
	private float spawnTimer = 0.0f; 
	
	public GameObject enemyShip; 
	
	private Vector3 spawnLocation;
	
	// Use this for initialization
	void Start () {
		
		spawnLocation = new Vector3(-140, 83, 5);
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((spawnTimer += Time.deltaTime) >= 10.0f)
		{
		
			Instantiate(enemyShip, spawnLocation, Quaternion.identity); 
			
			spawnTimer = 0; 
			
		}
	
	}
}
