using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public Animator charModelAnimator; 


	private int BulletsInClip = 0;

	
    void Update()
    {
		if (!isLocalPlayer)
		{
			
			return;
		}
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

		if ((Input.GetAxis("Horizontal") * Time.deltaTime * 150f) < -0.10f) {
			setAnimation("runLeftBool");
		} else if ((Input.GetAxis("Horizontal") * Time.deltaTime * 150f) > 0.10f) {
			setAnimation("runRightBool");
		} else if ((Input.GetAxis("Vertical") * Time.deltaTime * 3f) > 0.01) {
			setAnimation("runBool");
		} else if ((Input.GetAxis("Vertical") * Time.deltaTime * 3f) < -0.01) {
			setAnimation("runBackBool");
		} else {
			clearAnimator();
		}

		
        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

		if (Input.GetKeyDown(KeyCode.Mouse0) && isLocalPlayer)
		{
			if(BulletsInClip >=1) {
				CmdFire();
			} else {
				//Debug.Log("No Ammo");
			}
		}
    }

	void setAnimation(string animatorBool) {
		charModelAnimator.SetBool("walkBool", false);
		//charModelAnimator.SetBool("deathBool", false);
		charModelAnimator.SetBool("runBool", false);
		//charModelAnimator.SetBool("hitBool", false);
		charModelAnimator.SetBool("runLeftBool", false);
		charModelAnimator.SetBool("runRightBool", false);
		charModelAnimator.SetBool("runBackBool", false);
		charModelAnimator.SetBool(animatorBool, true);
	}

	void clearAnimator() {
		charModelAnimator.SetBool("walkBool", false);
		//charModelAnimator.SetBool("deathBool", false);
		charModelAnimator.SetBool("runBool", false);
		//charModelAnimator.SetBool("hitBool", false);
		charModelAnimator.SetBool("runLeftBool", false);
		charModelAnimator.SetBool("runRightBool", false);
		charModelAnimator.SetBool("runBackBool", false);
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
			//bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.transform.forward * 50;

			BulletsInClip--;
			// Spawn the bullet on the clients
			if (NetworkServer.active) {
				NetworkServer.Spawn(bullet);
			}
			// Destroy the bullet after 2 seconds
			//Destroy(bullet, 4.0f);
		}


void OnTriggerEnter(Collider other) {
		 if(other.gameObject.CompareTag("Pickup")) {
		 	Destroy(other.gameObject); 
		 	//Debug.Log("We hit the pickup object");
		 	BulletsInClip++;
			// Debug.Log("bullets in Clip: " + BulletsInClip);
		 }	
}
	public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }
}
