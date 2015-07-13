using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
		
	private Angle angle;
	private float speed = 4;

	void Start () {
		angle = GetComponent<Angle> ();
		speed = Random.Range (4, 12);

		EnemyManager.get().enemies.Add (this);
	}

	void OnDestroy() {
		EnemyManager.get ().enemies.Remove (this);
	}

	float areaRadius = 22;
	float totalDistance = 0;

	void OnTriggerEnter2D(Collider2D other) {


		if (other.tag != "Wavepoint") {
			return;
		}

		WavePoint point = other.GetComponent<WavePoint> ();

		if (point.next == null) {
			GameObject.Destroy(gameObject);
			return;
		}

		//if (point.getAngle ().IsWithin (angle.getDegreeValue(), 90)) {

			angle.setDegreeValue(point.getAngle().getDegreeValue());
		//}
	}
	
	
	bool UpdateEnemyLimits () {
		bool wasFlipped = false;
		
		float xsqr = gameObject.transform.position.x * gameObject.transform.position.x;
		float ysqr = gameObject.transform.position.y * gameObject.transform.position.y;
		float hyp = Mathf.Sqrt (xsqr + ysqr);
		if (hyp > areaRadius) {
			gameObject.transform.position = previousPosition;
			
			gameObject.transform.position = new Vector3 (-gameObject.transform.position.x, -gameObject.transform.position.y, gameObject.transform.position.z);
			
			Vector2 direction = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
			direction = direction.normalized;

			angle.setDegreeValue(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 180);
			angle.setDegreeValue(angle.getDegreeValue() + Random.Range(-10,10));
			
			wasFlipped = true;
		}

		return wasFlipped;
	}

	void UpdateEnemyMovement () {


		float y = angle.getDirection ().y * Time.deltaTime * speed;
		float x = angle.getDirection ().x * Time.deltaTime * speed;
		float fixedX = x + gameObject.transform.position.x;
		float fixedY = y + gameObject.transform.position.y;
		gameObject.transform.position = new Vector3 (fixedX, fixedY, gameObject.transform.position.z);
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
		if (angle == null) {
			return;
		}


		Gizmos.color = Color.red;

		float y = angle.getDirection ().y;
		float x = angle.getDirection ().x;

		Vector3 vec = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);

		Gizmos.DrawLine(transform.position, vec);

		Refresh ();
	}

	void Refresh() {
		float y = angle.getDirection ().y;
		float x = angle.getDirection ().x;
		
		Vector3 vec = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
		
		gameObject.transform.LookAt (vec);


	}
}
