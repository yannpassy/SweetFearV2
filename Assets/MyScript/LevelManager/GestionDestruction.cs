using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GestionDestruction : MonoBehaviour {
	public float[] dur = new float[10];
    public GameObject[] mesGameObject = new GameObject[10];
	public GameObject clef;
	public float chrono;
	private int compteur;
	private Tween fade;
	private bool passage;
	private float ratio;
	public float reduction;

	private Color32 couleurBleu;
	private Color32 couleurRougeClair;
	private Color32 couleurRouge;
	private Color32 couleurRougeFonce;
	// Use this for initialization
	void Start () {
		couleurBleu = new Color32 (176, 202, 206, 255);
		couleurRougeClair = new Color32 (255, 180, 180, 255);
		couleurRouge = new Color32 (255, 129, 129, 255);
		couleurRougeFonce = new Color32 (255, 49, 49, 255);

		fade = null;
		passage = false;
		compteur = 1;
		reduction = 4.0f;
    }

    // Update is called once per frame
    void Update() {
        float chronoOld = chrono;
        chrono += Time.deltaTime;
		if (chronoOld < dur[compteur] - 20 && chrono >= dur[compteur] - 20) {
			fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurRougeClair, reduction);
			StartCoroutine (CompleteTweenRouge ());
			reduction -= 2.0f;
		}
		if (chronoOld < dur[compteur] - 10 && chrono >= dur[compteur] - 10) {
			fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurRouge, reduction);
			StartCoroutine (CompleteTweenRouge ());
			reduction -= 1.25f;
		}
		if (chrono >= dur[compteur] - 5) {
			if (passage == false) {
				fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurRougeFonce, reduction);
				StartCoroutine (CompleteSwitchTweenRouge ());
			}
			if (passage == true) {
				fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurBleu,  reduction);
				StartCoroutine (CompleteSwitchTweenBleu ());
			}
		}
		if (chronoOld < dur[compteur] && chrono >= dur[compteur])
		{
			mesGameObject[compteur].GetComponent<ActiveTrueFalse>().enabled = true;
			reduction = 4.0f;
			compteur++;
		}
			
    }

	IEnumerator CompleteTweenRouge(){
		yield return fade.WaitForCompletion();
		fade = mesGameObject [compteur].transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurBleu,  reduction);
		
	}

	IEnumerator CompleteSwitchTweenRouge(){
		yield return fade.WaitForCompletion();
		passage = true;

	}

	IEnumerator CompleteSwitchTweenBleu(){
		yield return fade.WaitForCompletion();
		passage = false;
	}
}
