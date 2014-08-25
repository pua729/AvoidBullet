using UnityEngine;
using System.Collections;

public class KillBullet : MonoBehaviour {
	
	public string targetTag;

	private void OnTriggerEnter(Collider collider) {
		if (collider.tag == targetTag) {
		    Destroy(collider.gameObject);			
		}
	}

}
