using UnityEngine;
using System.Collections;

public class bulletReset : MonoBehaviour {
	
	public bool inFlight; 
	
	private float timer; 
	
	private Vector3 resetPosition; 
	
	private Vector3 resetVelocity; 
	
	// Use this for initialization
	void Start () {
		
		inFlight = false; 
	
		resetPosition = new Vector3(500, 500, 0); 
		resetVelocity = new Vector3(0,0,0); 
		//Destroy (gameObject, 15); 
	}
	
	// Update is called once per frame
	void Update () {
	
		if (timer + Time.deltaTime > 15)
		{
			transform.position = resetPosition; 
			this.rigidbody.velocity = resetVelocity;
		}
		
		
	}
	
	
	
	private void OnCollisionEnter(Collision collision)
	{
		inFlight = false; 
			transform.position = resetPosition; 
			this.rigidbody.velocity = resetVelocity; 
		
	}
	
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "boundary")
		{
			inFlight = false; 
			transform.position = resetPosition; 
			this.rigidbody.velocity = resetVelocity; 
		}
		
		if (collider.tag == "enemy")
		{
			inFlight = false; 
			transform.position = resetPosition; 
			this.rigidbody.velocity = resetVelocity; 
		}
		
	}
}
