using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCristaux : MonoBehaviour {
	
	public GameObject[] mesCristaux = new GameObject[20];
	private GameObject cristauxHasard;
	public GameObject particule;
	public GameObject clef;

	// Use this for initialization
	void Start () {
		Reset ();
	}
	
	// Update is called once per frame
	void Update () {
		
		clef.transform.parent = cristauxHasard.transform;
		clef.transform.position = cristauxHasard.transform.position;
		particule.transform.position = cristauxHasard.transform.position;

	}

	public void Reset(){
		cristauxHasard = mesCristaux [Random.Range (0, mesCristaux.Length)];
	}
}
