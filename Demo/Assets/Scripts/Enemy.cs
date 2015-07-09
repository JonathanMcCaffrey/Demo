using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	
	float direction = 0; //360
	
	// Use this for initialization
	void Start () {
		//direction = Random.Range (0, 360);
	}
	
	float areaRadius = 6;
	
	bool UpdateEnemyLimits () {
		bool wasFlipped = false;
		
		float xsqr = gameObject.transform.position.x * gameObject.transform.position.x;
		float zsqr = gameObject.transform.position.z * gameObject.transform.position.z;
		float hyp = Mathf.Sqrt (xsqr + zsqr);
		if (hyp > areaRadius) {
			gameObject.transform.position = previousPosition;
			
			gameObject.transform.position = new Vector3 (-gameObject.transform.position.x, gameObject.transform.position.y, -gameObject.transform.position.z);
			
			
			direction = Mathf.Atan2(gameObject.transform.position.z, gameObject.transform.position.x) * Mathf.Rad2Deg + 180;
			
			
		//	Debug.Log(direction);
			direction += Random.Range(-10,10);
			
			wasFlipped = true;
		}
		
		direction = Mathf.RoundToInt(direction);
		direction = direction % 360;
		
		return wasFlipped;
	}
	
	float speed = 12;
	
	void UpdateEnemyMovement () {
		float x = Mathf.Cos (direction);
		float z = Mathf.Sin (direction);
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
		
		
		gameObject.transform.rotation = Quaternion.Euler (new Vector3(0, direction, 0));
		
	}
}
