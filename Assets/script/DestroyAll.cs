using UnityEngine;
using System.Collections;

public class DestroyAll : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	/*
	private void OnTriggerEnter(Collider collider) {
	    Destroy(collider.gameObject);
	}
	*/
	
	private void OnTriggerExit(Collider collider) {
		Debug.Log("exit");
	}
}
