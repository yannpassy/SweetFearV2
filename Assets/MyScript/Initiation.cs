using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Initiation : MonoBehaviour {
	public Transform objectReference;
	public OVRCameraRig cameraOVR;

	public TextMeshProUGUI tmp;

	private enum Etat {texte1, texte2, texte3, texte4, cristauxPowers, texte5, tp, fadeOut, texte6, demiTour,  destructionFragment, fadeIn };

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

    // Use this for initialization
    void Start () {
		etat = 0;
		duration = 2.5f;
        destructionCristaux = false;
        Screen.lockCursor = true;
        animPioche = pioche.transform.GetChild(0).gameObject.GetComponent<Animation>();
        anim = curseur.transform.GetChild(0).gameObject.GetComponent<Animation>();
        chrono = 0;
        cylindreZoneTp.SetActive(false);
        chronoFadeOut = 0;

    }
	
	// Update is called once per frame
	void Update () {
        animPioche["pioche anim"].speed = 6.0f;
        anim.Play();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
		{
            if (etat == Etat.cristauxPowers || etat == Etat.tp)
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
				if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					Debug.Log ("on arrive a 4");
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
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
				Debug.Log (chronoValidation);
				if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					Debug.Log ("on y est");
					chronoValidation = 0;
					chronoValidationOld = 0;
					chronoFadeIn = 0;
					passage = true;
				}
			}

			if (passage == true) {
				FadeOutText ();
			}
		}

		if (etat == Etat.texte3) {
			tmp.text = "avant que Timmy ne vous trouve?";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
				if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					chronoValidation = 0;
					chronoValidationOld = 0;
					passage = true;
				}
			}

			if (passage == true)
            {
                FadeOutText ();
			}
		}

		if (etat == Etat.texte4) {
            Debug.Log("on est dans texte 4");
			tmp.text = "Passez par la porte Rouge...";
			if (passage == false) {
				FadeInText ();
			}

			if (tagTouchee == "Canvas" && chronoFadeIn > duration) {
				chronoValidationOld = chronoValidation;
				chronoValidation += Time.deltaTime;
				if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f) {
					chronoValidation = 0;
					chronoValidationOld = 0;
					passage = true;
				}
			}

			if (passage == true) {
				FadeOutText ();
			}
		}
        
        if (etat == Etat.cristauxPowers)
        {
            dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);
            if (destructionCristaux == false && tagTouchee == "CristauxPowers")
            {
                quatX = Quaternion.AngleAxis(-90, Vector3.right);
                quatZ = Quaternion.LookRotation(directionCurseur);
                Debug.Log(quatZ);
                quatResultat = quatZ * quatX;
                curseur.transform.rotation = quatResultat;
                pioche.SetActive(true);
                pioche.transform.position = cam.transform.position + cam.transform.rotation * new Vector3(0, 0, 0.4f);

                if (focusCurseur())
                {
                    Debug.Log("dans le bloc");
                    cristauxPowers.transform.GetChild(0).gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
                    tweenPioche = pioche.transform.DOMove(cristauxPowers.transform.position, 0.35f);
                    StartCoroutine(Pioche());
                    destructionCristaux = true;
                    cristauxPowers.GetComponent<MeshCollider>().enabled = false;
                    chrono = 0;
                    curseur.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
                    resetColorCurseur();
                    //FadeOutText();
                    etat = Etat.texte5;
                    curseur.SetActive(false);
                }
            }
        }
        if (etat == Etat.texte5)
        {
            Debug.Log("on est dans texte 5");
            tmp.text = "teleporte toi en fixant la zone indiqué ...";
            if (passage == false)
            {
                FadeInText();
            }

            if (tagTouchee == "Canvas" && chronoFadeIn > duration)
            {
                chronoValidationOld = chronoValidation;
                chronoValidation += Time.deltaTime;
                if (chronoValidationOld < 2.5f && chronoValidation >= 2.5f)
                {
                    chronoValidation = 0;
                    chronoValidationOld = 0;
                    passage = true;
                }
            }

            if (passage == true)
            {
                FadeOutText();
            }
        }
        if (etat == Etat.tp)
        {
            cylindreZoneTp.SetActive(true);
            curseur.SetActive(true);
            dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);
            if ( focusCurseur() && tagTouchee == "CylindreZoneTp")
            {
                //== fade in
                curseur.GetComponent<Rigidbody>().isKinematic = true;
                cam.GetComponent<OVRScreenFadeOut>().enabled = true;
                //cam.GetComponent<OVRScreenFadeOut>().StarFadeOut();
                chronoFadeOutTp += Time.deltaTime;
                Debug.Log(chronoFadeOutTp);
                if (chronoFadeOutTp > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
                {
                    Debug.Log("téléportation o/");
                    //== téléportation
                    this.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y + 0.066f, nouvellePosition.z);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/instant-teleport", this.transform.position);
                    cam.GetComponent<OVRScreenFadeOut>().StartFadeIn();

                    //==fade out
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
                        resetColorCurseur();
                    }
                }
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
        if (dist <= 0.02f && Vector3.Distance(this.transform.position, curseur.transform.position) <= distZoneTp)
        {
            
            //Debug.Log(dist);
            chronoOld = chrono;
            chrono += Time.deltaTime;
            if (chronoOld < 0.25 && chrono >= 0.25)
            {
                anim["Curseur_anim_simple"].speed = (float)(1.5 + ((3 - 1.5) * ((chrono - 0.25) / (1 - 0.25))));
                color = curseur.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Renderer>().material.DOColor(Color.green, 1.25f);
                color2 = curseur.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<Renderer>().material.DOColor(Color.green, 1.25f);
                color3 = curseur.transform.GetChild(0).gameObject.transform.GetChild(2).GetComponent<Renderer>().material.DOColor(Color.green, 1.25f);
                color4 = curseur.transform.GetChild(0).gameObject.transform.GetChild(3).GetComponent<Renderer>().material.DOColor(Color.green, 1.25f);
                color5 = curseur.transform.GetChild(0).gameObject.transform.GetChild(4).GetComponent<Renderer>().material.DOColor(Color.green, 1.25f);
            }
            if (chronoOld < 0.5 && chrono >= 0.5)
            {
                anim["Curseur_anim_simple"].speed = (float)(1.5 + ((3 - 1.5) * ((chrono - 0.25) / (1 - 0.25))));
            }
            if (chronoOld < 0.75 && chrono >= 0.75)
            {
                anim["Curseur_anim_simple"].speed = (float)(1.5 + ((3 - 1.5) * ((chrono - 0.25) / (1 - 0.25))));
            }
        }
        
        if (Vector3.Distance(this.transform.position, curseur.transform.position) > distZoneTp )
        {
            curseur.SetActive(false);
            chrono = 0;
        }
        if (Vector3.Distance(this.transform.position, curseur.transform.position) < distZoneTp && tagTouchee != "obstacle" )
        {
            curseur.SetActive(true);
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
        pioche.SetActive(false);
        curseur.transform.GetChild(0).GetComponent<Animation>().Stop();
        chrono = 0;
        anciennePositionCurseur = nouvellePosition;
    }
}
