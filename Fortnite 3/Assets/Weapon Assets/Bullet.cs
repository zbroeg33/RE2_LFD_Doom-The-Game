using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {

	void OnTriggerEnter(Collider collision)
    {
		var hit = collision.gameObject;
		var health = hit.GetComponent<Health>();
		if (health != null) {
			if (NetworkServer.active) {
				Debug.Log("bullet hit: try to destroy");
				NetworkServer.Destroy(gameObject);
			} else {
				Debug.Log("network server not active...");
			}
			health.TakeDamage(10);
		}
    }

}
