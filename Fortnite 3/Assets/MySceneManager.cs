using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MySceneManager : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(GameObject.Find("NetworkManager"));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoHome() {
		SceneManager.LoadScene("networkLobby");
	}
}
