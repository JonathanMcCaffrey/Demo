using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour {
	public List<Enemy> enemies = new List<Enemy>();

	public GameObject enemy = null;

	public float maxNumber = 20;
	public float spawnRate = 1;

	private static EnemyManager instance = null;
	void Awake() {
		if (instance) {
			GameObject.Destroy (this);
		} else { 
			instance = this;
		}
	}

	public static EnemyManager get() {
		return instance;
	}

	private float currentSpawnTime = 0;
	void Update () {
		
		currentSpawnTime -= Time.deltaTime;

		if (currentSpawnTime < 0) {
			currentSpawnTime = spawnRate;

			var createdEnemy = GameObject.Instantiate(enemy);


			createdEnemy.GetComponent<Angle>().setDegreeValue(Random.Range(0, 360));
			var direction = createdEnemy.GetComponent<Angle>().getDirection();
			createdEnemy.transform.position = new Vector3(direction.x * 6, direction.y * 6, 0);
		}

	}
}
