using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GestionIntroduction : MonoBehaviour {
	public string myAmbiance = "event:/Musique intro";
	FMOD.Studio.EventInstance musiqueIntro;
	FMOD.Studio.ParameterInstance parameterIntro;
	public float chrono;
	public float[] duration = new float[4];
	public float[] transition = new float[4];
	public GameObject[] lightScene = new GameObject[4];
	public int compteur;
	// Use this for initialization
	void Start () {
		musiqueIntro = FMODUnity.RuntimeManager.CreateInstance (myAmbiance);
		musiqueIntro.start ();
		musiqueIntro.getParameter ("transition", out parameterIntro);
	}

	// Update is called once per frame
	void Update () {
		chrono += Time.deltaTime;

		if (chrono >= duration[compteur] && compteur <= 4) {
			lightScene [compteur].GetComponent<Light> ().DOIntensity (1.5f, 2.0f);
			parameterIntro.setValue (transition[compteur]);
			compteur++;
		}

	}
}
