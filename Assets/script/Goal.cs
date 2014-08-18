using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public GameObject targetObject;
	
	private void OnTriggerEnter(Collider collider) {
		Debug.Log("target: " + this.targetObject.name);
		Debug.Log("hit: " + collider.name);
		if (targetObject.name == collider.name) {
			Debug.Log("goal: ");			
		}
	}	
}
