using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	private Text text;

	private int currentScore = 0;
	public int scoreMax = 2500;
	public int nextLevel = 1;

	private static ScoreManager instance = null;
	private static ScoreManager get() { return instance; }
	void Awake() {
		if (instance) {
			GameObject.Destroy (this);
		} else { 
			instance = this;
		}
	}

	void Start () {
		text = GetComponent<Text> ();
		text.text = currentScore.ToString ();
	}

	public static void KilledEnemy() {
		get ().currentScore += 140;

		get ().text.text = get ().currentScore.ToString ();

		if (get ().currentScore > get ().scoreMax) {

			Application.LoadLevel(get().nextLevel);
		}
	}
}
