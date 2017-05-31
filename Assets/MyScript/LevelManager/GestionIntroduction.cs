using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GestionIntroduction : MonoBehaviour {
	public float chrono;
	public float[] duration = new float[4];
	public GameObject[] lightScene = new GameObject[4];
	public int compteur;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		chrono += Time.deltaTime;

		if (chrono >= duration[compteur] && compteur <= 4) {
			lightScene [compteur].GetComponent<Light> ().DOIntensity (1.5f, 2.0f);
			compteur++;
		}

	}
}
