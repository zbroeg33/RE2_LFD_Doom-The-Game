using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

	public GameObject bulletPrefab;
	public GameObject pickupObject;


	public int BulletsInClip = 0;

    void Update()
    {
		  var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
		
		
		
    }
	void OnTriggerEnter(Collider other) {
		//  if(other.gameObject.CompareTag("PickUP")) {
		//  	Destroy(other.gameObject); 
		//  	Debug.Log("We hit the pickup object");
		//  	BulletsInClip++;
		// 	 Debug.Log("bullets in Clip" + BulletsInClip);
		//  }	
		//  else 
		if(other.gameObject.CompareTag("FogDamage")) {
			 Debug.Log("Hit the fog");
			var hit = this.gameObject;
			var health = hit.GetComponent<Health>();
			Debug.Log(hit);
			if(health != null) {
				health.TakeDamage(10);
			}
		 }
		
		else if(other.gameObject.CompareTag("Monster")) {
			 Debug.Log("Hit by the Monster");
			var hit = this.gameObject;
			var health = hit.GetComponent<Health>();
			Debug.Log(hit);
			if(health != null) {
				health.TakeDamage(10);
				
			}
		 }
	}


	

}