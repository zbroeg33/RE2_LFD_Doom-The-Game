using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public RectTransform healthBar;
	public bool destroyOnDeath;
	public GameObject self;
	private Animator animator;

	public const int maxHealth = 100;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth = maxHealth;
	private NetworkStartPosition[] spawnPoints;

	void Start ()
	{
		//animator = GetComponent<Animator>();
		if (isLocalPlayer)
		{
			spawnPoints = FindObjectsOfType<NetworkStartPosition>();		
		}
	}

	public void TakeDamage(int amount)
	{
		if (!isServer)
		{
			return;
		}

		currentHealth -= amount;
		Debug.Log("Health went down" + amount);
		if (currentHealth <= 0)
		{
			
			/*if (animator != null) {
				animator.SetBool("isIdle", false);
				animator.SetBool("isDead", true);
			} 
			*/
			DestroyGameObject();
			if (!destroyOnDeath)
			{
				// existing Respawn code     
				currentHealth = maxHealth;
				// called on the Server, but invoked on the Clients
				RpcRespawn(); 
			}
			
		}

		healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
	}

	public void DestroyGameObject() {
		Debug.Log("destroying: " + self);
		Destroy(self);
	}

	void OnChangeHealth (int health)
	{
		healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn()
	{
		if (isLocalPlayer)
		{
			// Set the spawn point to origin as a default value
			Vector3 spawnPoint = Vector3.zero;

			// If there is a spawn point array and the array is not empty, pick a spawn point at random
			if (spawnPoints != null && spawnPoints.Length > 0)
			{
				spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
			}

			// Set the player’s position to the chosen spawn point
			transform.position = spawnPoint;
		}
	}

}


