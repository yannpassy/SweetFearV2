using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Initiation : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;

	public TextMeshProUGUI tmp;
	public TextMeshProUGUI tmpSecondePartie;
	public GameObject canvasPremierePartie;
	public GameObject canvasSecondePartie;
	public int compteur;


	private enum Etat {texte1, texte2, texte3, texte4, texteDestructionCristaux, cristauxPowers, texte5, tp, texteGauche, gauche, texte6, rougeClair, texte7, rouge,  texte8, rougeFonce , texte9, texte12, fin };


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
	public GameObject fragment;
	public GameObject levelManager;
	public GameObject fragmentMesh;

	private Rigidbody rb;
	private float vitesseAnimation;
	private Animation anim;
	private Animation animPioche;

	private Color myColor;
	private float duration;
	private float ratio;

	private Color32 couleurBleu;
	private Color32 couleurRougeClair;
	private Color32 couleurRouge;
	private Color32 couleurRougeFonce;

	public float reduction;

	Tween fade;
    Tween color;
    Tween color2;
    Tween color3;
    Tween color4;
    Tween color5;
    Tween tweenPioche;
    private bool destructionCristaux;
    private double chrono;
    private float dist;
    private float distZoneTp = 7.0f;
    private double chronoOld;
    public GameObject cylindreZoneTp;
    private float chronoFadeOutTp;

    private GameObject tutoProgressBar;
    private GameObject tutoProgressBar2;

    // Use this for initialization
    void Start () {
		compteur = 1;
		etat = 0;
		duration = 2.5f;
        destructionCristaux = false;
        Screen.lockCursor = true;
        animPioche = pioche.transform.GetChild(0).gameObject.GetComponent<Animation>();
        anim = curseur.transform.GetChild(0).gameObject.GetComponent<Animation>();
        chrono = 0;
        cylindreZoneTp.SetActive(false);
        chronoFadeOut = 0;
		couleurBleu = new Color32 (176, 202, 206, 255);
		couleurRougeClair = new Color32 (255, 180, 180, 255);
		couleurRouge = new Color32 (255, 129, 129, 255);
		couleurRougeFonce = new Color32 (255, 49, 49, 255);
		reduction = 4.0f;
        tutoProgressBar = GameObject.Find("tutoProgressBar");
    }
	
	// Update is called once per frame
	void Update () {
        animPioche["pioche anim"].speed = 6.0f;
        anim.Play();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
			if (etat == Etat.cristauxPowers || etat == Etat.tp || etat == Etat.gauche)
            {
                anciennePositionCurseur = curseur.transform.position;
                if (chrono < 1)
                {
                    nouvellePosition = hit.point;
                    curseur.transform.position = nouvellePosition;
                }
                else
                {
                    curseur.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                cristauxPowers = hit.collider.gameObject;
            }
            
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
				Debug.Log ("tu me touches");
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					Debug.Log ("on arrive a 4");
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
            }

			if (passage == true) {
                FadeOutText ();
			}

		}

		if (etat == Etat.texte2) {
			tmp.text = "Peux-tu t'en sortir?";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                Debug.Log (chronoValidation);
				if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					Debug.Log ("on y est");
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
			}

			if (passage == true) {
				FadeOutText ();
			}
		}

		if (etat == Etat.texte3) {
			tmp.text = "avant que Timmy ne te trouve?";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					chronoValidation = 0;
					chronoValidationOld = 0;
                    chronoFadeIn = 0;
                    passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                    tutoProgressBar.transform.position += new Vector3(0, -1, 0) ;
                }
			}

			if (passage == true)
            {
                FadeOutText ();
			}
		}

		if (etat == Etat.texte4) {
            Debug.Log("on est dans texte 4");
			tmp.text = "trouve la clef pour passer la porte Rouge, elle se trouve dans l'un des cristaux violet...";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
			}

			if (passage == true) {
				FadeOutText ();
			}
		}

		if (etat == Etat.texteDestructionCristaux) {
			tmp.text = "fixe le cristaux devant la porte quand le texte disparaitra, seul ces derniers peuvent etre detruit";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                    tutoProgressBar.transform.position += new Vector3(0, 1, 0);
                }
			}

			if (passage == true) {
				FadeOutText ();
			}
		}
        
        if (etat == Etat.cristauxPowers)
        {
            dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);
			if (destructionCristaux == false && tagTouchee == "CristauxPowers") {
				quatX = Quaternion.AngleAxis (-90, Vector3.right);
				quatZ = Quaternion.LookRotation (directionCurseur);
				Debug.Log (quatZ);
				quatResultat = quatZ * quatX;
				curseur.transform.rotation = quatResultat;
				pioche.SetActive (true);
				pioche.transform.position = cam.transform.position + cam.transform.rotation * new Vector3 (0, 0, 0.4f);

				if (focusCurseur ()) {
					Debug.Log ("dans le bloc");
					cristauxPowers.transform.GetChild (0).gameObject.transform.GetComponent<Rigidbody> ().isKinematic = false;
					tweenPioche = pioche.transform.DOMove (cristauxPowers.transform.position, 0.35f);
					StartCoroutine (Pioche ());
					destructionCristaux = true;
					cristauxPowers.GetComponent<MeshCollider> ().enabled = false;
					chrono = 0;
					curseur.transform.rotation = Quaternion.AngleAxis (0, Vector3.right);
					resetColorCurseur ();
					//FadeOutText();
					etat = Etat.texte5;
					curseur.SetActive (false);
				}
			} else {
				curseur.SetActive (false);
			}
        }
        if (etat == Etat.texte5)
        {
            Debug.Log("on est dans texte 5");
            tmp.text = "teleporte toi maitenant en fixant la zone indique ...";
            if (passage == false)
            {
                FadeInText();
            }

            if (tagTouchee == "Canvas" && chronoFadeIn > duration)
            {
                chronoValidationOld = chronoValidation;
                chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
                {
                    chronoValidation = 0;
                    chronoValidationOld = 0;
					chronoFadeIn = 0;
                    passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
            }

            if (passage == true)
            {
                FadeOutText();
            }
        }
        if (etat == Etat.tp)
        {
			if (tagTouchee == "CylindreZoneTp") {
				chrono += Time.deltaTime;
				curseur.SetActive (true);
			} else {
				chrono = 0;
				curseur.SetActive (false);
			
			}

			if (compteur == 1) { 
				cylindreZoneTp.SetActive(true);
				dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);
				if (tagTouchee == "CylindreZoneTp") {
					if (focusCurseur ()) {
						compteur += 1;
					}
				} else {
					chrono = 0;
				}
			}

			if (compteur == 2) {
				curseur.GetComponent<Rigidbody>().isKinematic = true;
				cam.GetComponent<OVRScreenFadeOut>().enabled = true;
				cam.GetComponent<OVRScreenFadeOut>().StarFadeOut();
				compteur += 1;
			}

			if (compteur == 3) {
				chronoFadeOutTp += Time.deltaTime;
				if (chronoFadeOutTp > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
					this.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y + 0.066f, nouvellePosition.z);
					FMODUnity.RuntimeManager.PlayOneShot("event:/instant-teleport", this.transform.position);
					cam.GetComponent<OVRScreenFadeOut>().StartFadeIn();
					curseur.transform.GetChild(0).GetComponent<Animation>().Stop();
					compteur += 1;
				}
			}

			if (compteur == 4) {
				chronoFadeIn += Time.deltaTime;
				if (chronoFadeIn > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
					compteur += 1;
					curseur.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.black;
					curseur.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.black;
					curseur.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.black;
					curseur.transform.GetChild(0).gameObject.transform.GetChild(3).GetComponent<Renderer>().material.color = Color.black;
					curseur.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<Renderer>().material.color = Color.black;
					chronoFadeIn = 0;
					chrono = 0;
					cam.GetComponent<OVRScreenFadeOut>().enabled = false;
					curseur.SetActive (false);
					etat += 1;
                    compteur = 1;
				}
            }
        }

		if (etat == Etat.texteGauche)
		{
			tmp.text = "Fixe a present la fleche de gauche";
			if (passage == false)
			{
				FadeInText();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration)
			{
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
				{
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                    tutoProgressBar.transform.position = new Vector3(-20, 7, -3);
                    tutoProgressBar.transform .rotation *= Quaternion.AngleAxis(-90, Vector3.up);
                }
			}

			if (passage == true)
			{
				FadeOutText();
			}
		}

		if (etat == Etat.gauche) {
			if (tagTouchee == "gauche") {
				curseur.SetActive (true);
			} else {
				curseur.SetActive (false);
			}

			if (compteur == 1) {
				dist = Vector3.Distance (anciennePositionCurseur, curseur.transform.position);
				if (focusCurseur () && tagTouchee == "gauche") {
					compteur += 1;
				}
			}
			if (compteur == 2) {
				cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
				cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
				compteur += 1;
			}
			if (compteur == 3) {
				chronoFadeOut += Time.deltaTime;
				if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
					chronoFadeOut = 0;
					FMODUnity.RuntimeManager.PlayOneShot ("event:/instant-teleport", this.transform.position);
					this.transform.rotation *= Quaternion.AngleAxis (-90, Vector3.up);
					cam.GetComponent<OVRScreenFadeOut> ().StartFadeIn ();
					curseur.transform.GetChild (0).GetComponent<Animation> ().Stop ();
					compteur += 1;
				}
			}

			if (compteur == 4) {
				chronoFadeIn += Time.deltaTime;
				if (chronoFadeIn > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
					curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer> ().material.color = Color.black;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer> ().material.color = Color.black;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer> ().material.color = Color.black;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer> ().material.color = Color.black;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer> ().material.color = Color.black;
					chronoFadeIn = 0;
					chrono = 0;
					curseur.SetActive (false);
					cam.GetComponent<OVRScreenFadeOut> ().enabled = false;
					etat += 1;
				}

			}
		}

		if (etat == Etat.texte6)
		{

			tmpSecondePartie.text = "Mais tu n'as pas encore tout vu";
			if (passage == false)
			{
				FadeInTextSecondePartie();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration)
			{
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
				{
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
			}

			if (passage == true)
			{
				FadeOutTextSecondePartie();
			}
		}
				
		if (etat == Etat.rougeClair) {
			fade = fragment.transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurRougeClair, reduction);
			StartCoroutine (CompleteTweenRouge ());
			etat += 1;
		}


		if (etat == Etat.texte7)
		{
			tmpSecondePartie.text = "Ce monde est instable";
			tmp.text = "Mais tu n'as pas encore tout vu, vise la fleche au centre";
			if (passage == false)
			{
				FadeInTextSecondePartie();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration)
			{
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
				{
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
			}

			if (passage == true)
			{
				FadeOutTextSecondePartie();
			}
		}



		if (etat == Etat.rouge) {
			fade = fragment.transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurRouge, reduction);
			StartCoroutine (CompleteTweenRouge ());
			etat += 1;
		}

		if (etat == Etat.texte8)
		{
			tmpSecondePartie.text = "A tout moment...";
			if (passage == false)
			{
				FadeInTextSecondePartie();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration)
			{
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
				{
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
			}

			if (passage == true)
			{
				FadeOutTextSecondePartie();
			}
		}

		if (etat == Etat.rougeFonce) {
			chronoOld = chrono;
			chrono += Time.deltaTime;
			if (passage == false) {
				fade = fragment.transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurRougeFonce, reduction);
				StartCoroutine (CompleteSwitchTweenRouge ());
			}
			if (passage == true) {
				fade = fragment.transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurBleu,  reduction);
				StartCoroutine (CompleteSwitchTweenBleu ());
			}

			if (chronoOld < 1.5f && chrono >= 1.5f) {
				fragment.GetComponent<ActiveTrueFalse>().enabled = true;
				//FMODUnity.RuntimeManager.PlayOneShot ("event:/Effondrement", fragmentMesh.transform.position);
				etat += 1;
			}
		}

		if (etat == Etat.texte9)
		{
			tmpSecondePartie.text = "Tu peux tout perdre...";
			if (passage == false)
			{
				FadeInTextSecondePartie();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration)
			{
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
				{
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                    tutoProgressBar.transform.position += new Vector3(0, -0.5f, 0);
                }
			}

			if (passage == true)
			{
				FadeOutTextSecondePartie();
			}
		}


		if (etat == Etat.texte12)
		{
			tmpSecondePartie.text = "Tu as maintenant les pleins pouvoirs, a toi de jouer";
			if (passage == false)
			{
				FadeInTextSecondePartie();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration)
			{
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
                tutoProgressBar.GetComponent<Image>().fillAmount += Time.deltaTime / 2.5f;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
				{
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
                    tutoProgressBar.GetComponent<Image>().fillAmount = 0;
                }
			}

			if (passage == true)
			{
				FadeOutTextSecondePartie();
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

	void FadeInTextSecondePartie(){
		chronoFadeIn += Time.deltaTime;
		myColor = tmpSecondePartie.color;
		float ratio = chronoFadeIn / duration;
		myColor.a = Mathf.Lerp (0, 1, ratio);
		tmpSecondePartie.color = myColor;
	}

	void FadeOutText(){
		chronoFadeOut += Time.deltaTime;
		myColor = tmp.color;
		float ratio = chronoFadeOut / duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		tmp.color = myColor;
		if (chronoFadeOut > duration) {
			chronoFadeIn = 0;
			chronoFadeOut = 0;
			passage = false;
			etat += 1;

		}
	}
	void FadeOutTextSecondePartie(){
		chronoFadeOut += Time.deltaTime;
		myColor = tmpSecondePartie.color;
		float ratio = chronoFadeOut / duration;
		myColor.a = Mathf.Lerp (1, 0, ratio);
		tmpSecondePartie.color = myColor;
		if (chronoFadeOut > duration) {
			chronoFadeIn 	= 0;
			chronoFadeOut 	= 0;
			passage = false;
			etat += 1;
		}
		if (etat == Etat.fin) {
			ApplicationMode.passlevel = 1;
			this.GetComponent<Initiation> ().enabled = false;
			this.GetComponent<MoveTP> ().enabled = true;
			levelManager.GetComponent<GestionDestruction> ().enabled = true;
			canvasPremierePartie.SetActive (false);
			canvasSecondePartie.SetActive (false);

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

    bool focusCurseur()
    {
		if (dist <= 0.02f && Vector3.Distance (this.transform.position, curseur.transform.position) <= distZoneTp) {
            
			//Debug.Log(dist);
			chronoOld = chrono;
			chrono += Time.deltaTime;
			if (chronoOld < 0.25 && chrono >= 0.25) {
				anim ["Curseur_anim_simple"].speed = (float)(1.5 + ((3 - 1.5) * ((chrono - 0.25) / (1 - 0.25))));
				color = curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer> ().material.DOColor (Color.green, 1.25f);
				color2 = curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer> ().material.DOColor (Color.green, 1.25f);
				color3 = curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer> ().material.DOColor (Color.green, 1.25f);
				color4 = curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer> ().material.DOColor (Color.green, 1.25f);
				color5 = curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer> ().material.DOColor (Color.green, 1.25f);
			}
			if (chronoOld < 0.5 && chrono >= 0.5) {
				anim ["Curseur_anim_simple"].speed = (float)(1.5 + ((3 - 1.5) * ((chrono - 0.25) / (1 - 0.25))));
			}
			if (chronoOld < 0.75 && chrono >= 0.75) {
				anim ["Curseur_anim_simple"].speed = (float)(1.5 + ((3 - 1.5) * ((chrono - 0.25) / (1 - 0.25))));
			}
		} else {
			resetColorCurseur ();
		}
        
		if (Vector3.Distance (this.transform.position, curseur.transform.position) > distZoneTp) {
			curseur.SetActive (false);
			chrono = 0;
		}
		if (etat == Etat.tp || etat == Etat.cristauxPowers) {
			if (Vector3.Distance (this.transform.position, curseur.transform.position) < distZoneTp && tagTouchee == "CylinderZoneTp" || tagTouchee == "CristauxPowers") {
				curseur.SetActive (true);
			}
		}
        if (chrono > 1)
        {
            return true;
        }

        return false;
    }

    void resetColorCurseur()
    {
        curseur.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.grey;
        curseur.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.grey;
        curseur.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Renderer>().material.color = Color.grey;
        curseur.transform.GetChild(0).gameObject.transform.GetChild(3).GetComponent<Renderer>().material.color = Color.grey;
        curseur.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<Renderer>().material.color = Color.grey;
        color.Kill();
        color2.Kill();
        color3.Kill();
        color4.Kill();
        color5.Kill();
        curseur.transform.GetChild(0).GetComponent<Animation>().Stop();
        chrono = 0;
        anciennePositionCurseur = nouvellePosition;
    }

	IEnumerator CompleteTweenRouge(){
		yield return fade.WaitForCompletion();
		fade = fragment.transform.GetChild (30).gameObject.GetComponent<Renderer> ().material.DOColor (couleurBleu,  reduction);
		reduction -= 1.5f;
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
