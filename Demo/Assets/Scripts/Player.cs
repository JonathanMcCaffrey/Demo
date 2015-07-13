using UnityEngine;
using System.Collections;


[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, yMin, yMax;
}

public class Player : MonoBehaviour {
	
	public float speed;
	public float tilt;
	public Boundary boundary;
	
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	
	private float nextFire;
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			//	GetComponent<AudioSource>().Play ();
		}
	}
	
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
		
		Vector2 velocity = movement * speed;
		
		GetComponent<Rigidbody2D>().velocity = velocity;
		
		
		GetComponent<Rigidbody2D>().position = new Vector2 (
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.x, boundary.xMin, boundary.xMax), 
			Mathf.Clamp (GetComponent<Rigidbody2D>().position.y, boundary.yMin, boundary.yMax)
			);
		
		if (Mathf.Sqrt (velocity.x * velocity.x) >= 0.6f || Mathf.Sqrt (velocity.y * velocity.y) >= 0.6f) {
			float direction = Mathf.Atan2 (velocity.y, velocity.x) * Mathf.Rad2Deg;
			GetComponent<Rigidbody2D> ().rotation = direction - 90;
		} else {
			float clampRotation = GetComponent<Rigidbody2D> ().rotation / 45.0f;
			GetComponent<Rigidbody2D> ().rotation = ((Mathf.Round(clampRotation) * 45.0f 
			                                         + GetComponent<Rigidbody2D> ().rotation) / 2.0f);
		}
	}
}

