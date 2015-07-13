using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 20;
	
	void Start ()
	{
		GetComponent<Rigidbody2D>().velocity = transform.up * speed;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Enemy") {
			//Destroy(other.gameObject);
			Destroy(gameObject);

			ScoreManager.KilledEnemy();
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "Boundary") {
			Destroy(gameObject);
		}
	}
}
