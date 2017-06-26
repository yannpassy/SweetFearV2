using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviour {
    public GameObject Timmy2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Timmy2.activeInHierarchy == true)
        {
            this.GetComponent<GestionDestruction>().enabled = true;
        }
	}
}
