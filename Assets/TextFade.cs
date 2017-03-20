using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour {
	public TextMeshProUGUI tmp;
	private float duration;
	private float chrono;
	private float chronoOld;
	// Use this for initialization
	void Start () {
		duration = 4;
		tmp.text = "Bienvenue dans ce monde";
	}
	
	// Update is called once per frame
	void Update () {
		FadeOutText ();
			
	}

	void FadeOutText(){
		Color myColor = tmp.color;
		float ratio = Time.time / duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		tmp.color = myColor;
	}	
}
