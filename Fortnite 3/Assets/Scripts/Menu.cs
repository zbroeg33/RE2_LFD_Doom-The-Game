using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {

	public GameObject serverMenu;
    public Text serverIP;
    private bool isActive = true;

    public Button hostGameButton;
    public Button joinGameButton;
    public Button quitGameButton;
    public GameObject hostIP;
    public GameObject textField;



    public void Start() {
        hostGameButton.gameObject.SetActive(true);
        joinGameButton.gameObject.SetActive(true);
        hostIP.SetActive(true);
        textField.SetActive(true);
        quitGameButton.gameObject.SetActive(false);
    }

	 public void StartServer()
    {
        hostGameButton.gameObject.SetActive(false);
        joinGameButton.gameObject.SetActive(false);
        hostIP.SetActive(false);
        textField.SetActive(false);
        quitGameButton.gameObject.SetActive(true);
        NetworkManager.singleton.StartHost();
        serverMenu.SetActive(false);
    }

    public void Quit() {
        hostGameButton.gameObject.SetActive(true);
        joinGameButton.gameObject.SetActive(true);
        hostIP.SetActive(true);
        textField.SetActive(true);
        quitGameButton.gameObject.SetActive(false);
        NetworkManager.singleton.StopHost();
        SceneManager.LoadScene("networkLobby");
    }

    public void StartClient()
    {
        hostGameButton.gameObject.SetActive(true);
        joinGameButton.gameObject.SetActive(true);
        hostIP.SetActive(true);
        textField.SetActive(true);
        quitGameButton.gameObject.SetActive(false);
        getIP();
        NetworkManager.singleton.StartClient();
        serverMenu.SetActive(false);
    }

    private void Update()
    {
        //Debug.Log("is network active" + NetworkManager.singleton.isNetworkActive.ToString());
        //Debug.Log("is client connected" + NetworkManager.singleton.IsClientConnected());
       /*  if (NetworkManager.singleton.isNetworkActive && !NetworkManager.singleton.IsClientConnected()) {
            Debug.Log("quit");
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopHost();
                    Destroy(this.gameObject);
            this.Quit();
       } */
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
