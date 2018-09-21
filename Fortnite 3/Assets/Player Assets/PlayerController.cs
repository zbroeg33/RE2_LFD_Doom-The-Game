using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	public GameObject bulletPrefab;
	public Transform bulletSpawn; 


	private int BulletsInClip = 0;


    void Update()
    {
		if (!isLocalPlayer)
		{
			return;
		}
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			if(BulletsInClip >=1) {
				CmdFire();
			} else {
				Debug.Log("No Ammo");
			}
		}
    }

	[Command]
	void CmdFire()
		{
			// Create the Bullet from the Bullet Prefab
			var bullet = (GameObject)Instantiate (
				bulletPrefab,
				bulletSpawn.position,
				bulletSpawn.rotation);
			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 50;

			// Spawn the bullet on the clients
			NetworkServer.Spawn(bullet);

			// Destroy the bullet after 2 seconds
			Destroy(bullet, 4.0f);
		}


void OnTriggerEnter(Collider other) {
		 if(other.gameObject.CompareTag("Pickup")) {
		 	Destroy(other.gameObject); 
		 	Debug.Log("We hit the pickup object");
		 	BulletsInClip++;
			 Debug.Log("bullets in Clip" + BulletsInClip);
		 }	
}
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
