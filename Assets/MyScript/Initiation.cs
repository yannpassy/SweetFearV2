using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Initiation : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;

	public TextMeshProUGUI tmp;

	private enum Etat {texte1, texte2, texte3, texte4, tp, texte5, demiTour, texte6, destructionFragment, cristauxPowers};

	Etat etat;

	private Vector3 centreCamera;
	private Vector3 nouvellePosition;

	private float chronoFadeIn;
	private float chronoFadeOut;
	private float chronoValidation;
	private float chronoValidationOld;
	private bool passage;

	private string tagTouchee;

	private OVRScreenFadeOut fadeout;
	private OVRScreenFadeIn fadein;

	private Vector3 anciennePositionCurseur;
	private Vector3 positionPioche;	
	private Vector3 directionCurseur;

	private Quaternion quatX;
	private Quaternion quatZ;
	private Quaternion quatResultat;

	public Camera cam;

	public GameObject curseur;
	public GameObject cristauxPowers;
	public GameObject clefOuverture;
	public GameObject pioche;
	public GameObject Timmy;

	private Rigidbody rb;
	private float vitesseAnimation;
	private Animation anim;
	private Animation animPioche;

	private Color myColor;
	private float duration;
	private float ratio;
	// Use this for initialization
	void Start () {
		etat = 0;
		duration = 4;
		
	}
	
	// Update is called once per frame
	void Update () {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			tagTouchee = hit.collider.tag;
			directionCurseur = hit.point - cam.transform.position;
			directionCurseur.Normalize();

		}

		if (etat == Etat.texte1) {
			tmp.text = "Bienvenue dans SWEETFEAR";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
				if (chronoValidationOld < 4 && chronoValidation >= 4) {
					Debug.Log ("on arrive a 4");
					chronoValidation = 0;
					chronoValidationOld = 0;
					passage = true;
				}
			}

			if (passage == true) {
				FadeOutText ();
			}
		}

		if (etat == Etat.texte2) {
			tmp.text = "Pouvez-vous vous en sortir?";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
				if (chronoValidationOld < 4 && chronoValidation >= 4) {
					chronoValidation = 0;
					chronoValidationOld = 0;
					passage = true;
				}
			}

			if (passage == true) {
				FadeOutText ();
			}
		}


	}
	void FadeInText(){
		chronoFadeIn += Time.deltaTime;
		myColor = tmp.color;
		float ratio = chronoFadeIn / duration;
		myColor.a = Mathf.Lerp (0, 1, ratio);
		tmp.color = myColor;
	}

	void FadeOutText(){
		chronoFadeOut += Time.deltaTime;
		myColor = tmp.color;
		float ratio = chronoFadeOut / duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		tmp.color = myColor;
		if (chronoFadeOut > duration) {
			chronoFadeIn 	= 0;
			chronoFadeOut 	= 0;
			passage = false;
			etat += 1;
		}
	}	
}
