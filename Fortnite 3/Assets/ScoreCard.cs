using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour {

	private int score = 0;
	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddToScore(int points) {
		score += points;
		text.text = "Score: " + score;
	}

	public int GetScore() {
		return score;
	}
}
