using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour {
	public TextMeshProUGUI tmp;
	private float duration;
	// Use this for initialization
	void Start () {
		duration = 3;
	}
	
	// Update is called once per frame
	void Update () {
		Color myColor = tmp.color;
		float ratio = Time.time/duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		tmp.color = myColor;
			
	}
}
