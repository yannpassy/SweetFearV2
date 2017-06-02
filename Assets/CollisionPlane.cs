using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlane : MonoBehaviour {
	// Use this for initialization
	public GameObject LevelManager;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Plane") {
			LevelManager.GetComponent<GestionCristaux> ().Reset ();

		}
	}
}
