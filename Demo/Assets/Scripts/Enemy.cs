using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
		

	float speed = 4;

	void Start () {
		direction = Random.Range (0, 360);

		speed = Random.Range (4, 12);
	}
	
	float areaRadius = 18;

	float direction = 0;
	
	float totalDistance = 0;
	
	bool UpdateEnemyLimits () {
		bool wasFlipped = false;
		
		float xsqr = gameObject.transform.position.x * gameObject.transform.position.x;
		float zsqr = gameObject.transform.position.z * gameObject.transform.position.z;
		float hyp = Mathf.Sqrt (xsqr + zsqr);
		if (hyp > areaRadius) {
			gameObject.transform.position = previousPosition;
			
			gameObject.transform.position = new Vector3 (-gameObject.transform.position.x, gameObject.transform.position.y, -gameObject.transform.position.z);
			
			
			direction = Mathf.Atan2(gameObject.transform.position.z, gameObject.transform.position.x) * Mathf.Rad2Deg + 180;
			
			direction += Random.Range(-10,10);
			
			wasFlipped = true;
		}
		
		direction = Mathf.RoundToInt(direction);
		direction = direction % 360;
		
		return wasFlipped;
	}

	void UpdateEnemyMovement () {
		float x = Mathf.Cos (direction) * Time.deltaTime * speed;
		float z = Mathf.Sin (direction) * Time.deltaTime * speed;
		float fixedX = x + gameObject.transform.position.x;
		float fixedZ = z + gameObject.transform.position.z;
		gameObject.transform.position = new Vector3 (fixedX, gameObject.transform.position.y, fixedZ);
	}
	
	bool wasRecentlyFlipped = false;
	
	Vector3 previousPosition = Vector3.zero;
	
	float flipTimer = 0;
	float timerMax = 0.2f;
	void Update () {
		
		previousPosition = gameObject.transform.position;
		
		UpdateEnemyMovement ();
		
		if (!wasRecentlyFlipped || true) {
			wasRecentlyFlipped = UpdateEnemyLimits (); 
			flipTimer = timerMax;
		} else {
			flipTimer -= Time.deltaTime;
			if(flipTimer < 0) {
				wasRecentlyFlipped = false;
			}
		}

		Refresh ();
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.red;

		float x = Mathf.Cos (direction) * 2;
		float z = Mathf.Sin (direction) * 2;

		Vector3 vec = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

		Gizmos.DrawLine(transform.position, vec);

		Refresh ();
	}

	void Refresh() {
		float x = Mathf.Cos (direction) * 2;
		float z = Mathf.Sin (direction) * 2;
		
		Vector3 vec = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
		
		gameObject.transform.LookAt (vec);

	}
}
