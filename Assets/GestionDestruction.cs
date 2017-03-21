using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GestionDestruction : MonoBehaviour {
    public float[] dur = new float[10];
	public float[] durationAvantFade = new float[10];
    public GameObject[] mesGameObject = new GameObject[10];
	public float chrono;
	private int compteur;
	private Tween fade;
	private bool passage;
	private float ratio;
	public float reduction;
	// Use this for initialization
	void Start () {
		fade = null;
		passage = false;
		compteur = 0;
		reduction = 1.0f;
    }

    // Update is called once per frame
    void Update() {
        float chronoOld = chrono;
        chrono += Time.deltaTime;
		if (chrono >= dur[compteur] / 2) {
			if (passage == false) {
				fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (Color.black, reduction);
				StartCoroutine (CompleteTweenBlack ());
			}
			if (passage == true) {
				fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (Color.white,  reduction);
				StartCoroutine (CompleteTweenWhite());
			}
		}
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
		{
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
		}
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
        {
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			compteur++;
        }
			
    }

	IEnumerator CompleteTweenBlack(){
		yield return fade.WaitForCompletion();
		passage = true;
		
	}

	IEnumerator CompleteTweenWhite(){
		yield return fade.WaitForCompletion();
		passage = false;
	}
}
