using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MoveTP : MonoBehaviour
{
	public string myAmbiance = "event:/AmbianceCreepy";
	FMOD.Studio.EventInstance eventCreepy;
	FMOD.Studio.ParameterInstance parameterCreepy;

    public Transform objectReference;
    public OVRCameraRig cameraOVR;
    private enum Etat { Look, AnalyseCommande, fadeOut, teleportation, fadeIn, demiTour, cristauxPowers, ouvertureRouge };
    Etat etat;

    private Vector3 centreCamera;
    private Vector3 nouvellePosition;

    private double chrono;
    private double chronoFadeOut;
    private double chronoFadeIn;

    private string tagTouchee;

    private OVRScreenFadeOut fadeout;
    private OVRScreenFadeIn fadein;

    private Vector3 anciennePositionCurseur;
    private Vector3 PositionCube;

    public Camera cam;

    public GameObject curseur;
    public GameObject cristauxPowers;
    public GameObject clef;
    public GameObject porte;
    public GameObject clefOuverture;
    private GameObject vide;
    public GameObject Timmy;

    private Rigidbody rb;
    private float dist;
    private float distZoneTp = 7.0f;
	private float vitesseAnimation;
	private Animation anim;

    private bool obtentionClefRouge;

    public GameObject canvasClef;
    public GameObject canvasPorte;

    void Start()
    {
        Screen.lockCursor = true;
        centreCamera = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
        vide = GameObject.Find("Vide");
        etat = Etat.Look;
        obtentionClefRouge = false;
		eventCreepy = FMODUnity.RuntimeManager.CreateInstance (myAmbiance);
		eventCreepy.start ();
		eventCreepy.getParameter ("creep", out parameterCreepy);
       
    }

    // Update is called once per frame
    void Update()
    {

		//On acccentue le son creepy au fur et a mesure de l'approche de l'ourson
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 10) {
			parameterCreepy.setValue (0.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 9 && Vector3.Distance(this.transform.position, Timmy.transform.position) < 10) {
			parameterCreepy.setValue (2.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 7 && Vector3.Distance(this.transform.position, Timmy.transform.position) < 8) {
			parameterCreepy.setValue (4.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 6 && Vector3.Distance(this.transform.position, Timmy.transform.position) < 7) {
			parameterCreepy.setValue (5.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 5 &&  Vector3.Distance(this.transform.position, Timmy.transform.position) < 6) {
			parameterCreepy.setValue (6.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 4 &&  Vector3.Distance(this.transform.position, Timmy.transform.position) < 5) {
			parameterCreepy.setValue (7.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 3 &&  Vector3.Distance(this.transform.position, Timmy.transform.position) < 4) {
			parameterCreepy.setValue (8.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 2 &&  Vector3.Distance(this.transform.position, Timmy.transform.position) < 3) {
			parameterCreepy.setValue (9.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 1 &&  Vector3.Distance(this.transform.position, Timmy.transform.position) < 2) {
			parameterCreepy.setValue (10.0f);
		}
		if (Vector3.Distance (this.transform.position, Timmy.transform.position) < 1.0f) {
			eventCreepy.release ();
		}

        //affiche la souris
        /*if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = false;*/

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
        RaycastHit hit;
        
		if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
			anciennePositionCurseur = curseur.transform.position;
			if (chrono < 1) {
				nouvellePosition = hit.point;
				curseur.transform.position = nouvellePosition;
			} else {
				curseur.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			}
                tagTouchee = hit.collider.tag;
                cristauxPowers = hit.collider.gameObject;
        }
		/*if (Physics.Raycast(new Ray(cam.transform.position, cam.transform.rotation*Vector3.forward), out hit, Mathf.Infinity)
		{
			cube.SetActive(true);
			nouvellePosition = hit.point;
			cube.transform.position = nouvellePosition;
			tagTouchee = hit.collider.tag;
			cristauxPowers = hit.collider.gameObject;
		}*/

        if(Vector3.Distance(this.transform.position, Timmy.transform.position) < 0.2f)
        {
            SceneManager.LoadScene("EcranGameOver");
        }
        //affiche ou affiche pas le curseur


        if (etat == Etat.Look)
        {
			//on part sur un renderer gris pour le curseur
			curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer>().material.color = Color.grey;
			curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer>().material.color = Color.grey;
			curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer>().material.color = Color.grey;
			curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer>().material.color = Color.grey;
			curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer>().material.color = Color.grey;

			//On calcule la distance entre l'ancienne position du cube et la nouvelle
			dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);
            if (tagTouchee == "terrain")
            {
				curseur.transform.GetChild(0).gameObject.SetActive(true);
				curseur.transform.GetChild (1).gameObject.SetActive (false);
            }
            else if (tagTouchee == "obstacle" || tagTouchee == "porte")
            {
				curseur.SetActive (false);
            }
            else if (tagTouchee == "CristauxPowers")
            {
				curseur.transform.GetChild (0).gameObject.SetActive (false);
				curseur.transform.GetChild (1).gameObject.SetActive (true);
            }
            else if (tagTouchee == "serrureRouge" && obtentionClefRouge == true)
            {
                curseur.SetActive(false);
                clefOuverture.SetActive(true);
                
            }
            else if(tagTouchee == "serrureRouge" && obtentionClefRouge == false)
            {
                canvasPorte.SetActive(true);
                curseur.SetActive(true);
            }
            else
            {
                //curseur.SetActive(true);
                clefOuverture.SetActive(false);
                canvasPorte.SetActive(false);
            }
				
			if (dist <= 0.02f && Vector3.Distance (this.transform.position, curseur.transform.position) <= distZoneTp) {
				anim = curseur.transform.GetChild (0).gameObject.GetComponent<Animation> ();
				anim.Play ();
				//Debug.Log(dist);
				chrono += Time.deltaTime;
				if (chrono > 0.25) {
					anim ["Curseur_anim_simple"].speed = (float)(1.5+((3-1.5)*((chrono-0.25)/(1-0.25))));
					curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer>().material.color = Color.grey;
				}
				if (chrono > 0.5) {
					anim ["Curseur_anim_simple"].speed = (float)(1.5+((3-1.5)*((chrono-0.25)/(1-0.25))));
					curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer>().material.color = Color.grey;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer>().material.color = Color.grey;
				}
				if (chrono > 0.75) {
					anim ["Curseur_anim_simple"].speed = (float)(1.5+((3-1.5)*((chrono-0.25)/(1-0.25))));
					curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer>().material.color = Color.green;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer>().material.color = Color.green;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer>().material.color = Color.green;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer>().material.color = Color.green;
					curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer>().material.color = Color.green;
				}
			}
            else 
            {
				curseur.transform.GetChild (0).GetComponent<Animation> ().Stop ();
                chrono = 0;
                anciennePositionCurseur = nouvellePosition;
            }

			if(Vector3.Distance(this.transform.position, curseur.transform.position) > distZoneTp)
			{
				curseur.SetActive(false);
				chrono = 0;
			}


			if(Vector3.Distance(this.transform.position, curseur.transform.position) < distZoneTp && tagTouchee != "obstacle"){
				curseur.SetActive(true);
			}


            if (chrono > 1)
            {
                //etat = Etat.teleportation;
                etat = Etat.AnalyseCommande;
            }
        }
        else if (etat == Etat.AnalyseCommande)
        {
            
           if (tagTouchee == "terrain")
            {
				curseur.GetComponent<Rigidbody> ().isKinematic = true;
				cam.GetComponent<OVRScreenFadeOut>().enabled = true;
				cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
                etat = Etat.fadeOut;
                //curseur.GetComponent<MeshRenderer>().material.color = Color.green;
            }


            if (tagTouchee == "obstacle" || tagTouchee == "porte")
            {
                //curseur.GetComponent<MeshRenderer>().material.color = Color.red;
                chrono = 0;
                etat = Etat.Look;
            }


            if(tagTouchee == "serrureRouge" && obtentionClefRouge == true)
            {
                clefOuverture.transform.DORotate(new Vector3(90, 0, 90), 1, 0);
                etat = Etat.ouvertureRouge;
            }


            if (tagTouchee == "demi-tour")
            {
                etat = Etat.demiTour;
            }


            if(tagTouchee == "CristauxPowers")
            {
                etat = Etat.cristauxPowers;
                //curseur.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        } else if (etat == Etat.fadeOut) {
            chronoFadeOut += Time.deltaTime;
            if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
			{
                etat = Etat.teleportation;
                chronoFadeOut = 0;
            }
        } else if (etat == Etat.teleportation) {

            this.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y + 0.066f, nouvellePosition.z);

			FMODUnity.RuntimeManager.PlayOneShot ("event:/instant-teleport", this.transform.position);	

			cam.GetComponent<OVRScreenFadeOut> ().StartFadeIn ();

            //cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.066f + 0.7f, this.transform.position.z);

            //cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.066f + 2.5f, this.transform.position.z);

            etat = Etat.fadeIn;
        } else if (etat == Etat.fadeIn) {
			curseur.transform.GetChild (0).GetComponent<Animation> ().Stop ();
            chronoFadeIn += Time.deltaTime;
            if (chronoFadeIn > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
            {
                chronoFadeIn = 0;
                chrono = 0;
				cam.GetComponent<OVRScreenFadeOut> ().enabled = false;
                etat = Etat.Look;
            }

        } else if (etat == Etat.demiTour) {
            cam.GetComponent<OVRScreenFadeOut>().enabled = true;
            chronoFadeOut += Time.deltaTime;
            if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
            {
                cam.GetComponent<OVRScreenFadeOut>().enabled = false;
                this.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
                cameraOVR.transform.rotation *= Quaternion.AngleAxis (180, Vector3.up);
                etat = Etat.fadeIn;
                chronoFadeOut = 0;
            }

            // == le code original
            //this.transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);
            //cameraOVR.transform.rotation *= Quaternion.AngleAxis (180, Vector3.up);
            //chrono = 0;
            //etat = Etat.Look;
        } else if(etat == Etat.cristauxPowers) {
            
            cristauxPowers.transform.GetChild(0).gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            cristauxPowers.GetComponent<MeshCollider>().enabled = false;
            if (cristauxPowers.transform.FindChild("Clef"))
            {
                clef = cristauxPowers.transform.GetChild(1).gameObject;
                clef.transform.DOMoveY(6, 2);
                obtentionClefRouge = true;
                StartCoroutine(AffichageText());    
            }
            chrono = 0;
            etat = Etat.Look;
        } else if(etat == Etat.ouvertureRouge) 	{
            StartCoroutine(DeplacementPorte());
            chrono = 0;
            etat = Etat.Look;
        }


    }

    IEnumerator DeplacementPorte()
    {
        yield return new WaitForSeconds(1);

        porte.GetComponent<Animation>().Play();

    }

    IEnumerator AffichageText()
    {
        yield return new WaitForSeconds(2);

        Destroy(clef);
        canvasClef.SetActive(true);
    }

}