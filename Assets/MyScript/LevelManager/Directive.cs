using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directive : MonoBehaviour {

	public GameObject Suzie;
	public GameObject cylinderZoneTp;
	public GameObject canvasPorteInitiation;
	public GameObject canvasFractureInitiation;
	public GameObject barreProgression;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ApplicationMode.passlevel == 1) {
			Suzie.GetComponent<MoveTP> ().enabled = true;
			Suzie.GetComponent<Initiation> ().enabled = false;
			cylinderZoneTp.SetActive (false);
			canvasPorteInitiation.SetActive (false);
			canvasFractureInitiation.SetActive (false);
		}
	}
}
