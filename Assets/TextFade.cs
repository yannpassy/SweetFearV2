using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour {
	public TextMeshProUGUI tmp;
	private float duration;
	private float chrono;
	private float chronoOld;
	private bool passage;
	Color myColor;
	// Use this for initialization
	void Start () {
		passage = false;
		duration = 1;
		tmp.text = "Bienvenue dans ce monde";
	}
	
	// Update is called once per frame
	void Update () {
		chrono += Time.deltaTime;

		if (passage == false) {
			FadeInText ();
		}
		if (passage == true) {
			FadeOutText ();
		}

	}

	void FadeOutText(){
		myColor = tmp.color;
		float ratio = chrono / duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		tmp.color = myColor;
		if (chrono > duration) {
			passage = false;
			chrono = 0;
		}
	}	

	void FadeInText(){
		myColor = tmp.color;
		float ratio = chrono / duration;
		myColor.a = Mathf.Lerp (0, 1, ratio);
		tmp.color = myColor;
		if (chrono > duration) {
			passage = true;
			chrono = 0;
		}
	}
}
