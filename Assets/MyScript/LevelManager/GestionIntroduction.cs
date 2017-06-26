using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GestionIntroduction : MonoBehaviour {
	public string myAmbiance = "event:/Musique intro";

	FMOD.Studio.EventInstance musiqueIntro;
	FMOD.Studio.ParameterInstance parameterIntro;

	public float chrono;
    public float chronoText;

    public string[] texte = new string[5];
	public float[] duration = new float[5];
	public float[] transition = new float[5];

	public GameObject[] lightScene = new GameObject[5];

    public TextMeshProUGUI TextIntro;

	public int compteur;
    public Tweener tweenIntro;
	// Use this for initialization
	void Start () {
		musiqueIntro = FMODUnity.RuntimeManager.CreateInstance (myAmbiance);
		musiqueIntro.start ();
		musiqueIntro.getParameter ("transition", out parameterIntro);
        chronoText = 0;
    }

	// Update is called once per frame
	void Update () {
		chrono += Time.deltaTime;

		if (compteur < 5 && chrono >= duration[compteur]) {
            Debug.Log(chronoText);
			lightScene [compteur].GetComponent<Light> ().DOIntensity (1.5f, 2.0f);
			parameterIntro.setValue (transition[compteur]);
            TextIntro.text = texte[compteur];
            if (chronoText == 0)
            {
                TextIntro.DOFade(1, 1);
                chronoText += Time.deltaTime;
            }
            else if (chronoText > 0)
            {
                chronoText += Time.deltaTime;
            }
            if (chronoText > 3)
            {
                TextIntro.DOFade(0, 1);
                chronoText = 0;
                compteur++;
            }
			
		}

		if (compteur == 5 && chrono >= duration[compteur]) {
			Debug.Log(duration[compteur]);
			musiqueIntro.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
			SceneManager.LoadScene ("NiveauHiver");
		}

	}
}
