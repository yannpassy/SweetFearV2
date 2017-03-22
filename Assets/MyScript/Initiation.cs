using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Initiation : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;
	private enum Etat {texte1, texte2, texte3, texte4, tp, texte5, demiTour, texte6, destructionFragment, cristauxPowers, fadeOut, fadeIn };
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

    Tween tweenPioche;
    private bool destructionCristaux;
    private double chronoFadeOut;
    private double chronoFadeIn;

    // Use this for initialization
    void Start () {
        destructionCristaux = false;
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

        animPioche["pioche anim"].speed = 6.0f;

        if (etat == Etat.cristauxPowers)
        {
            cristauxPowers.transform.GetChild(0).gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            tweenPioche = pioche.transform.DOMove(cristauxPowers.transform.position, 0.35f);
            StartCoroutine(Pioche());
            destructionCristaux = true;
            cristauxPowers.GetComponent<MeshCollider>().enabled = false;
            chrono = 0;
            etat = Etat.texte4;
        }
        else if (etat == Etat.fadeOut)
        {
            chronoFadeOut += Time.deltaTime;
            if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
            {
                etat = Etat.tp;
                chronoFadeOut = 0;
            }
        }
        else if (etat == Etat.tp)
        {
            this.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y + 0.066f, nouvellePosition.z);
            FMODUnity.RuntimeManager.PlayOneShot("event:/instant-teleport", this.transform.position);
            cam.GetComponent<OVRScreenFadeOut>().StartFadeIn();

            etat = Etat.fadeIn;
        }
        else if (etat == Etat.fadeIn)
        {
            curseur.transform.GetChild(0).GetComponent<Animation>().Stop();
            chronoFadeIn += Time.deltaTime;
            if (chronoFadeIn > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
            {
                //on part sur un renderer noir pour le curseur
                curseur.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
                curseur.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.black;
                curseur.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.black;
                curseur.transform.GetChild(0).gameObject.transform.GetChild(3).GetComponent<Renderer>().material.color = Color.black;
                curseur.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<Renderer>().material.color = Color.black;
                chronoFadeIn = 0;
                chrono = 0;
                cam.GetComponent<OVRScreenFadeOut>().enabled = false;
                etat = Etat.texte5;
            }
        }
        else if (etat == Etat.demiTour)
        {
            cam.GetComponent<OVRScreenFadeOut>().enabled = true;
            cam.GetComponent<OVRScreenFadeOut>().StarFadeOut();
            chronoFadeOut += Time.deltaTime;
            if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
            {
                chronoFadeOut = 0;
                FMODUnity.RuntimeManager.PlayOneShot("event:/instant-teleport", this.transform.position);
                this.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
                cam.GetComponent<OVRScreenFadeOut>().StartFadeIn();
                // fade in
                curseur.transform.GetChild(0).GetComponent<Animation>().Stop();
                chronoFadeIn += Time.deltaTime;
                if (chronoFadeIn > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
                {
                    curseur.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
                    curseur.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.black;
                    curseur.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.black;
                    curseur.transform.GetChild(0).gameObject.transform.GetChild(3).GetComponent<Renderer>().material.color = Color.black;
                    curseur.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<Renderer>().material.color = Color.black;
                    chronoFadeIn = 0;
                    chrono = 0;
                    cam.GetComponent<OVRScreenFadeOut>().enabled = false;
                    etat = Etat.texte6;
                }
            }

        } else if (etat == Etat.destructionFragment)
        {

        }
    }

    IEnumerator Pioche()
    {
        yield return new WaitForSeconds(0.55f);
        //pioche.transform.position = cam.transform.position + cam.transform.rotation * Vector3.forward * 2.0f;
        pioche.SetActive(false);
    }

    IEnumerator FinDestructionCristaux()
    {
        yield return new WaitForSeconds(0.55f);
        destructionCristaux = false;
    }
}
