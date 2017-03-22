using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initiation : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;
	private enum Etat {texte1, texte2, texte3, texte4, tp, texte5, demiTour, texte6, destructionFragment, cristauxPowers};
	Etat etat;

	private Vector3 centreCamera;
	private Vector3 nouvellePosition;

	private double chrono;

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

	// Use this for initialization
	void Start () {
		
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


	}
}
