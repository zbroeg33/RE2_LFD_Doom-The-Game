using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	 public GameObject serverMenu;
    public Text serverIP;
    private bool isActive = false;

    public GameObject lobbyUI;
    public GameObject inGameUI;

    public void Start() {
        lobbyUI.SetActive(true);
        inGameUI.SetActive(false);
    }

	 public void StartServer()
    {
        lobbyUI.SetActive(false);
        inGameUI.SetActive(true);
        NetworkManager.singleton.StartHost();
        serverMenu.SetActive(false);
    }

    public void Quit() {
        lobbyUI.SetActive(true);
        inGameUI.SetActive(false);
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("networkLobby");
    }

    public void StartClient()
    {
        lobbyUI.SetActive(true);
        inGameUI.SetActive(false);
        getIP();
        NetworkManager.singleton.StartClient();
        serverMenu.SetActive(false);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "networkLobby") {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                serverMenu.SetActive(isActive);
                isActive = !isActive;
            }
        }
    }
    public void getIP(){
        if (serverIP.text.Length > 0 && serverIP.text!= null){
            NetworkManager.singleton.networkAddress = serverIP.text;
        }
    }
}
