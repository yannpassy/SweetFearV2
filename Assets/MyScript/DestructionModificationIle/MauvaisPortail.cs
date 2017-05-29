using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MauvaisPortail : MonoBehaviour {
    public bool activerScript;

    public TextMeshProUGUI tmp;
    private bool passage;
    private float chronoFadeIn;
    private float chronoFadeOut;
    private float chronoValidation;
    private float chronoValidationOld;
    private Color myColor;
    private float duration;

    // Use this for initialization
    void Start () {
        activerScript = false;

        passage = false;
        chronoFadeIn = 0;
        chronoFadeOut = 0;
        chronoValidation = 0;
        chronoValidationOld = 0;
        duration = 2.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if (activerScript)
        {
            tmp.text = "ce n'est pas la bonne porte";
            if (passage == false)
            {
                FadeInText();
            }
            chronoValidationOld = chronoValidation;
            chronoValidation += Time.deltaTime;
            if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
            {
                chronoValidation = 0;
                chronoValidationOld = 0;
                chronoFadeIn = 0;
                passage = true;
            }
            if (passage == true)
            {
                FadeOutText();
            }
        }
	}

    void FadeInText()
    {
        chronoFadeIn += Time.deltaTime;
        myColor = tmp.color;
        float ratio = chronoFadeIn / duration;
        myColor.a = Mathf.Lerp(0, 1, ratio);
        tmp.color = myColor;
    }

    void FadeOutText()
    {
        chronoFadeOut += Time.deltaTime;
        myColor = tmp.color;
        float ratio = chronoFadeOut / duration;
        myColor.a = Mathf.Lerp(1, 0, ratio);
        tmp.color = myColor;
        if (chronoFadeOut > duration)
        {
            chronoFadeIn = 0;
            chronoFadeOut = 0;
            passage = false;
            activerScript = false;
        }
    }
}
