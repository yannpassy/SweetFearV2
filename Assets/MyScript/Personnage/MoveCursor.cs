using UnityEngine;
using System.Collections;

public class MoveCursor : MonoBehaviour {
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StopMoveCursor(){
		rb.isKinematic = true;
	}
	public void StartMoveCursor(){
		rb.isKinematic = false;
	}
}
