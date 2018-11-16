using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCard : MonoBehaviour {

	private int score = 0;
	public Text text;

	private float delay = 2.0f;
	private float time = 0.0f;

	Camera[] cameras = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		cameras = GameObject.FindObjectsOfType<Camera>();
		if (cameras != null && cameras.Length == 0) {
			//Debug.Log("no cameras found");
			if (time >= delay) {
				//GameObject.FindObjectOfType<Menu>().Quit();
				//Destroy(GameObject.Find("NetworkManager"));
				//SceneManager.LoadScene("networkLobby");
				
			}
		}
	}

	public void AddToScore(int points) {
		score += points;
		text.text = "Score: " + score;
	}

	public int GetScore() {
		return score;
	}
}
