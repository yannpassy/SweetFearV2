using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class MoveTP : MonoBehaviour
{
	public string myAmbiance = "event:/AmbianceCreepy";
	FMOD.Studio.EventInstance eventCreepy;
	FMOD.Studio.ParameterInstance parameterCreepy;
	FMOD.Studio.STOP_MODE stopMusic;
	Tween color;
	Tween color2;
	Tween color3;
	Tween color4;
	Tween color5;
	Tween tweenPioche;

	private bool timmyActive;

    public Transform objectReference;
    public OVRCameraRig cameraOVR;
	private enum Etat { Look, AnalyseCommande, fadeOut, teleportation, fadeIn, demiTour, cristauxPowers, ouvertureRouge, QuartGauche, QuartDroite, InterrupteurTriangle, InterrupteurLosange, InterrupteurCarre, InterrupteurCroix, InterrupteurRond, texteCle, piedestal};
    Etat etat;

    private Vector3 centreCamera;
    private Vector3 nouvellePosition;

    private float chrono;
	private float chronoOld;
    private float chronoFadeOut;
    private float chronoFadeIn;
	private float chronoValidation;
	private float chronoValidationOld;

    private string tagTouchee;

    private OVRScreenFadeOut fadeout;
    private OVRScreenFadeIn fadein;

    private Vector3 anciennePositionCurseur;
    private Vector3 PositionCube;
	private Vector3 positionPioche;	
	private Vector3 directionCurseur;
	private Vector3 positionCanvas;

	private Quaternion quatX;
	private Quaternion quatZ;
	private Quaternion quatResultat;

    public Camera cam;

    public GameObject curseur;
    public GameObject cristauxPowers;
    public GameObject clef;
    public GameObject porte;
    public GameObject clefOuverture;
	public GameObject pioche;
    public GameObject Timmy;
	public GameObject porteTriangle;
	public GameObject porteLosange;
	public GameObject porteCarre;
	public GameObject porteCroix;
	public GameObject porteRondePremiere;
	public GameObject porteRondeSeconde;
	public GameObject InterrupteurTriangle;
	public GameObject InterrupteurLosange;
	public GameObject InterrupteurCarre;
	public GameObject InterrupteurCroix;
	public GameObject InterrupteurRond;
	public GameObject CanvasCle;
	public GameObject Sphere;

	private GameObject vide;
	private GameObject objetTouche;

	public TextMeshProUGUI tmpClef;
	private Color myColor;
	private float duration;
	private float ratio;

    private Rigidbody rb;
    private float dist;
    private float distZoneTp = 7.0f;
	private float vitesseAnimation;
	private Animation anim;
	private Animation animPioche;

    private bool obtentionClefRouge;
	private bool destructionCristaux;
	private bool passage;

    public GameObject canvasClef;
    public GameObject canvasPorte;

    private bool cleActiverEffet;
    private float chronoCle;
    private GameObject particleCle;
    private GameObject MauvaisPortail;

    void Start()
    {
		destructionCristaux = false;
		timmyActive = false;
		Screen.lockCursor = true;
		obtentionClefRouge = false;

        centreCamera = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
		anim = curseur.transform.GetChild (0).gameObject.GetComponent<Animation> ();
		animPioche = pioche.transform.GetChild (0).gameObject.GetComponent<Animation> ();
        vide = GameObject.Find("Vide");
        etat = Etat.Look;
		eventCreepy = FMODUnity.RuntimeManager.CreateInstance (myAmbiance);
		eventCreepy.start ();
		eventCreepy.getParameter ("creep", out parameterCreepy);
        cleActiverEffet = false;
        chronoCle=0;
        particleCle= GameObject.Find("ParticleCle");
        particleCle.SetActive(false);
        MauvaisPortail= GameObject.Find("ScriptMauvaisPortail");
    }

    // Update is called once per frame
    void Update()
    {
		animPioche ["pioche anim"].speed = 6.0f;
		//On acccentue le son creepy au fur et a mesure de l'approche de l'ourson
		if (timmyActive == true) {
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 10) {
				parameterCreepy.setValue (0.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 9 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 10) {
				parameterCreepy.setValue (2.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 7 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 8) {
				parameterCreepy.setValue (4.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 6 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 7) {
				parameterCreepy.setValue (5.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 5 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 6) {
				parameterCreepy.setValue (6.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 4 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 5) {
				parameterCreepy.setValue (7.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 3 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 4) {
				parameterCreepy.setValue (8.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 2 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 3) {
				parameterCreepy.setValue (9.0f);
			}
			if (Vector3.Distance (this.transform.position, Timmy.transform.position) > 1 && Vector3.Distance (this.transform.position, Timmy.transform.position) < 2) {
				parameterCreepy.setValue (10.0f);
			}
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
			objetTouche = hit.collider.gameObject;
			directionCurseur = hit.point - cam.transform.position;
			directionCurseur.Normalize();

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
			parameterCreepy.setValue (0.0f);
            SceneManager.LoadScene("EcranGameOver");
        }
        //affiche ou affiche pas le curseur


		if (etat == Etat.Look) {
			
			if (destructionCristaux == false) {
				//On calcule la distance entre l'ancienne position du cube et la nouvelle
				dist = Vector3.Distance (anciennePositionCurseur, curseur.transform.position);
                if (tagTouchee == "terrain") {
                    if (Vector3.Distance(this.transform.position, curseur.transform.position) < distZoneTp && tagTouchee != "obstacle") {
                        curseur.SetActive(true);
                    }
                    curseur.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
                    pioche.SetActive(false);
                } else if (tagTouchee == "demi-tour") {
                    //curseur.transform.GetChild (0).gameObject.SetActive (true);
                    //curseur.transform.GetChild (1).gameObject.SetActive (false);
                    curseur.transform.rotation = Quaternion.AngleAxis(0, Vector3.right);
                    pioche.SetActive(false);
                } else if (tagTouchee == "obstacle") {
                    curseur.SetActive(false);
                } else if (tagTouchee == "Piedestal") {
                    Debug.Log("touché");
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                } else if (tagTouchee == "CristauxPowers") {
                    //curseur.transform.GetChild (0).gameObject.SetActive (false);
                    //curseur.transform.GetChild (1).gameObject.SetActive (true);
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                    pioche.SetActive(true);
                    pioche.transform.position = cam.transform.position + cam.transform.rotation * new Vector3(0, 0, 0.4f);
                } else if (tagTouchee == "serrureRouge" && obtentionClefRouge == true) {
                    curseur.SetActive(false);
                    clefOuverture.SetActive(true);
                    pioche.SetActive(false);

                } else if (tagTouchee == "serrureRouge" && obtentionClefRouge == false) {
                    canvasPorte.SetActive(true);
                    pioche.SetActive(false);
                } else if (tagTouchee == "InterrupteurLosange") {
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    Debug.Log(quatZ);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                } else if (tagTouchee == "InterrupteurTriangle") {
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                } else if (tagTouchee == "InterrupteurCarre") {
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                } else if (tagTouchee == "InterrupteurCroix") {
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                } else if (tagTouchee == "InterrupteurRond") {
                    quatX = Quaternion.AngleAxis(-90, Vector3.right);
                    quatZ = Quaternion.LookRotation(directionCurseur);
                    quatResultat = quatZ * quatX;
                    curseur.transform.rotation = quatResultat;
                } else if (tagTouchee == "serrureJaune" && obtentionClefRouge== true) {
                    MauvaisPortail.GetComponent<MauvaisPortail>().activerScript = true;
                }else {
					//curseur.SetActive(true);
					clefOuverture.SetActive (false);
					canvasPorte.SetActive (false);
				}
			} else {
				StartCoroutine (FinDestructionCristaux ());
			}
				
			if (dist <= 0.02f && Vector3.Distance (this.transform.position, curseur.transform.position) <= distZoneTp) {
				anim.Play ();
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
				curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer> ().material.color = Color.grey;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer> ().material.color = Color.grey;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer> ().material.color = Color.grey;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer> ().material.color = Color.grey;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer> ().material.color = Color.grey;
				color.Kill ();
				color2.Kill ();
				color3.Kill ();
				color4.Kill ();
				color5.Kill ();
				pioche.SetActive (false);
				curseur.transform.GetChild (0).GetComponent<Animation> ().Stop ();
				chrono = 0;
				anciennePositionCurseur = nouvellePosition;
			}

			if (Vector3.Distance (this.transform.position, curseur.transform.position) > distZoneTp) {
				curseur.SetActive (false);
				chrono = 0;
			}	


			if (Vector3.Distance (this.transform.position, curseur.transform.position) < distZoneTp && tagTouchee != "obstacle") {
				curseur.SetActive (true);
			}


			if (chrono > 1) {
				//etat = Etat.teleportation;
				etat = Etat.AnalyseCommande;
			}
		} else if (etat == Etat.AnalyseCommande) {
            
			if (tagTouchee == "terrain") {
				curseur.GetComponent<Rigidbody> ().isKinematic = true;
				cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
				cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
				etat = Etat.fadeOut;
				//curseur.GetComponent<MeshRenderer>().material.color = Color.green;
			}


			if (tagTouchee == "obstacle" || tagTouchee == "porte") {
				//curseur.GetComponent<MeshRenderer>().material.color = Color.red;
				chrono = 0;
				etat = Etat.Look;
			}

			if (tagTouchee == "Piedestal") {
				etat = Etat.piedestal;
			}

			if (tagTouchee == "serrureRouge" && obtentionClefRouge == true) {
				clefOuverture.transform.DORotate (new Vector3 (90, 0, 90), 1, 0);
				etat = Etat.ouvertureRouge;
			}


			if (tagTouchee == "demi-tour") {
				etat = Etat.demiTour;
			}
			if (tagTouchee == "gauche") {
				etat = Etat.QuartGauche;
			}
			if (tagTouchee == "droite") {
				etat = Etat.QuartDroite;
			}

			if (tagTouchee == "CristauxPowers") {
				etat = Etat.cristauxPowers;
				//curseur.GetComponent<MeshRenderer>().material.color = Color.green;
			}

			if (tagTouchee == "InterrupteurTriangle") {
				etat = Etat.InterrupteurTriangle;
			}

			if (tagTouchee == "InterrupteurLosange") {
				etat = Etat.InterrupteurLosange;
			}

			if (tagTouchee == "InterrupteurCarre") {
				etat = Etat.InterrupteurCarre;
			}

			if (tagTouchee == "InterrupteurCroix") {
				etat = Etat.InterrupteurCroix;
			}

			if (tagTouchee == "InterrupteurRond") {
				etat = Etat.InterrupteurRond;
			}
				
			if (tagTouchee == "portail") {
				cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
				cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
				StartCoroutine (ChangerScene ());
			}

		} else if (etat == Etat.fadeOut) {
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				etat = Etat.teleportation;
				chronoFadeOut = 0;
			}
		} else if (etat == Etat.teleportation) {

			this.transform.position = new Vector3 (nouvellePosition.x, nouvellePosition.y + 0.066f, nouvellePosition.z);

			FMODUnity.RuntimeManager.PlayOneShot ("event:/instant-teleport", this.transform.position);	

			cam.GetComponent<OVRScreenFadeOut> ().StartFadeIn ();

			//cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.066f + 0.7f, this.transform.position.z);

			//cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.066f + 2.5f, this.transform.position.z);

			etat = Etat.fadeIn;
		} else if (etat == Etat.fadeIn) {
			curseur.transform.GetChild (0).GetComponent<Animation> ().Stop ();
			chronoFadeIn += Time.deltaTime;
			if (chronoFadeIn > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				//on part sur un renderer noir pour le curseur
				curseur.transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Renderer> ().material.color = Color.black;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (1).GetComponent<Renderer> ().material.color = Color.black;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (2).GetComponent<Renderer> ().material.color = Color.black;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (3).GetComponent<Renderer> ().material.color = Color.black;
				curseur.transform.GetChild (0).gameObject.transform.GetChild (4).GetComponent<Renderer> ().material.color = Color.black;
				chronoFadeIn = 0;
				chrono = 0;
				cam.GetComponent<OVRScreenFadeOut> ().enabled = false;
				etat = Etat.Look;
			}

		} else if (etat == Etat.demiTour) {
			cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
			cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				chronoFadeOut = 0;
				FMODUnity.RuntimeManager.PlayOneShot ("event:/instant-teleport", this.transform.position);
				this.transform.rotation *= Quaternion.AngleAxis (180, Vector3.up);
				cam.GetComponent<OVRScreenFadeOut> ().StartFadeIn ();
				etat = Etat.fadeIn;
			}

		} else if (etat == Etat.QuartGauche) {
			cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
			cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				chronoFadeOut = 0;
				FMODUnity.RuntimeManager.PlayOneShot ("event:/instant-teleport", this.transform.position);
				this.transform.rotation *= Quaternion.AngleAxis (-90, Vector3.up);
				cam.GetComponent<OVRScreenFadeOut> ().StartFadeIn ();
				etat = Etat.fadeIn;
			}

		} else if (etat == Etat.QuartDroite) {
			cam.GetComponent<OVRScreenFadeOut> ().enabled = true;
			cam.GetComponent<OVRScreenFadeOut> ().StarFadeOut ();
			chronoFadeOut += Time.deltaTime;
			if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut> ().fadeTime) {
				chronoFadeOut = 0;
				FMODUnity.RuntimeManager.PlayOneShot ("event:/instant-teleport", this.transform.position);
				this.transform.rotation *= Quaternion.AngleAxis (90, Vector3.up);
				cam.GetComponent<OVRScreenFadeOut> ().StartFadeIn ();
				etat = Etat.fadeIn;
			}

		} else if (etat == Etat.cristauxPowers) {
            
			cristauxPowers.transform.GetChild (0).gameObject.transform.GetComponent<Rigidbody> ().isKinematic = false;
			tweenPioche = pioche.transform.DOMove (cristauxPowers.transform.position, 0.35f);
			StartCoroutine (Pioche ());
			destructionCristaux = true;
			cristauxPowers.GetComponent<MeshCollider> ().enabled = false;
			if (cristauxPowers.transform.FindChild ("Clef")) {
				clef = cristauxPowers.transform.GetChild (1).gameObject;
				clef.transform.DOMoveY (6, 2);
                clef.transform.rotation *= Quaternion.AngleAxis(-45, Vector3.right);
                cleActiverEffet = true;
                particleCle.SetActive(true);
                particleCle.transform.position = clef.transform.position+ new Vector3(0,1.5f,0);
                obtentionClefRouge = true;
				StartCoroutine (AffichageText ()); 
				Timmy.SetActive (true);
				timmyActive = true;
				Timmy.GetComponent<TimmyMove> ().enabled = true;
				FMODUnity.RuntimeManager.PlayOneShot ("event:/Rugissement Timmy", this.transform.position);
			}
			chrono = 0;
			etat = Etat.Look;
		} else if (etat == Etat.ouvertureRouge) {
			StartCoroutine (DeplacementPorte ());
			chrono = 0;
			etat = Etat.Look;
		} else if (etat == Etat.InterrupteurTriangle) {
			InterrupteurTriangle.GetComponent<Animation> ().Play ();
			objetTouche.transform.tag = "obstacle";
			StartCoroutine (OuverturePorteTriangle ());
		} else if (etat == Etat.InterrupteurLosange) {
			InterrupteurLosange.GetComponent<Animation> ().Play ();
			objetTouche.transform.tag = "obstacle";
			StartCoroutine (OuverturePorteLosange ());
		} else if (etat == Etat.InterrupteurCarre) {
			InterrupteurCarre.GetComponent<Animation> ().Play ();
			objetTouche.transform.tag = "obstacle";
			StartCoroutine (OuverturePorteCarre ());
		} else if (etat == Etat.InterrupteurCroix) {
			InterrupteurCroix.GetComponent<Animation> ().Play ();
			objetTouche.transform.tag = "obstacle";
			StartCoroutine (OuverturePorteCroix ());
		} else if (etat == Etat.InterrupteurRond) {
			InterrupteurRond.GetComponent<Animation> ().Play ();
			objetTouche.transform.tag = "obstacle";
			StartCoroutine (OuverturePorteRond ());
		} else if (etat == Etat.piedestal) {
			Sphere.GetComponent<Rigidbody> ().isKinematic = false;
			objetTouche.transform.tag = "obstacle";
			Timmy.GetComponent<TimmyMove> ().enabled = true;
			Timmy.SetActive (true);
			FMODUnity.RuntimeManager.PlayOneShot ("event:/Rugissement Timmy", this.transform.position);
			etat = Etat.Look;
		}

        //== affiche les effets quand on récupère la clé
        if (cleActiverEffet)
        {
            chronoCle += Time.deltaTime;
            clef.transform.rotation *= Quaternion.AngleAxis(-45 * Time.deltaTime, new Vector3(0,0.75f, 0.5f));
            particleCle.GetComponent<ParticleSystem>().startColor+= new Color(0, -Time.deltaTime / 3, Time.deltaTime/3, 0);

            if (chronoCle > 5)
            {
                cleActiverEffet = false;
                clef.SetActive(false);
                particleCle.SetActive(false);
            }
        }
    }

    IEnumerator DeplacementPorte()
    {
        yield return new WaitForSeconds(1);
		FMODUnity.RuntimeManager.PlayOneShot ("event:/grincement porte", porte.transform.position);
		porte.GetComponent<Animation>().Play();

    }

    IEnumerator AffichageText()
    {
        yield return new WaitForSeconds(2);
		CanvasCle.SetActive (true);
		positionCanvas = CanvasCle.transform.position;
		cam.transform.DetachChildren ();
		CanvasCle.transform.position = positionCanvas;
        Destroy(clef);
		StartCoroutine (destructionCanvas ());
    }

	IEnumerator Pioche ()
	{
		yield return new WaitForSeconds (0.55f);
		//pioche.transform.position = cam.transform.position + cam.transform.rotation * Vector3.forward * 2.0f;
		pioche.SetActive (false);
	}	

	IEnumerator FinDestructionCristaux (){
		yield return new WaitForSeconds (0.55f);
		destructionCristaux = false;
	}

	IEnumerator ChangerScene(){
		yield return new WaitForSeconds (1.0f);
		parameterCreepy.setValue (0.0f);
		ApplicationMode.passlevel = 2;
		SceneManager.LoadScene ("EcranChangementNiveau");
	}

	IEnumerator destructionCanvas(){
		yield return new WaitForSeconds(4);
		Destroy (CanvasCle);
	}

	IEnumerator OuverturePorteTriangle(){
		yield return new WaitForSeconds (1.0f);
		porteTriangle.GetComponent<Animation>().Play ();
		etat = Etat.Look;
	}

	IEnumerator OuverturePorteLosange(){
		yield return new WaitForSeconds (1.0f);
		porteLosange.GetComponent<Animation>().Play ();
		etat = Etat.Look;
	}

	IEnumerator OuverturePorteCarre(){
		yield return new WaitForSeconds (1.0f);
		porteCarre.GetComponent<Animation>().Play ();
		etat = Etat.Look;
	}

	IEnumerator OuverturePorteCroix(){
		yield return new WaitForSeconds (1.0f);
		porteCroix.GetComponent<Animation>().Play ();
		etat = Etat.Look;
	}

	IEnumerator OuverturePorteRond(){
		yield return new WaitForSeconds (1.0f);
		porteRondePremiere.GetComponent<Animation>().Play ();
		porteRondeSeconde.GetComponent<Animation>().Play ();
		etat = Etat.Look;
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Plane") {
			ApplicationMode.passlevel = 1;
			SceneManager.LoadScene("EcranGameOver");
		}
	}

}