using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireProjectile : NetworkBehaviour {

    public GameObject projectilePrefab;
    GameObject instantiatedProjectile;
    public Transform projectileLaunchPoint;
    public int BulletsInClip = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if((Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0))&& (isLocalPlayer)){
           if(BulletsInClip >=1) {
				CmdFire();
			} else {
				Debug.Log("out of ammo :(");
			}
        }
	}
    [Command]
    void CmdFire()
    {
        instantiatedProjectile = Instantiate(projectilePrefab, projectileLaunchPoint.position, transform.rotation);
        if (NetworkServer.active)
        {
            NetworkServer.Spawn(instantiatedProjectile);
           
        }

        BulletsInClip--;
        if (NetworkServer.active)
        {
          //  NetworkServer.Destroy(instantiatedProjectile);
          // Debug.Log("Destoroyed" + instantiatedProjectile);
        }
        
    }

    void OnTriggerEnter(Collider other) {
		 if(other.gameObject.CompareTag("Pickup")) {
		 	Destroy(other.gameObject); 
		 	Debug.Log("We hit the pickup object");
		 	BulletsInClip+=3;
			 Debug.Log("bullets in Clip" + BulletsInClip);
		 }	
        //  else if(other.gameObject.CompareTag("Zombie")) {
		// 	 Debug.Log("Shot the Zombie");
		// 	var hit = this.gameObject;
		// 	var health = hit.GetComponent<Health>();
		// 	Debug.Log(hit);
		// 	if(health != null) {
		// 		health.TakeDamage(10);
        //         Destroy(instantiatedProjectile);
				
		// 	}
		//  }
    }
}
