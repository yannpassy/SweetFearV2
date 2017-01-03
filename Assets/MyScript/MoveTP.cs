using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MoveTP : MonoBehaviour
{
    public Transform objectReference;
    public OVRCameraRig cameraOVR;
    public bool clef;
    private enum Etat { Look, AnalyseCommande, fadeOut, teleportation, fadeIn, demiTour };
    Etat etat;
    private Vector3 centreCamera;
    private Vector3 nouvellePosition;
    private double chrono;
    private double chronoFadeOut;
    private double chronoFadeIn;
    private string tagTouchee;
    private OVRScreenFadeOut fadeout;
    private OVRScreenFadeIn fadein;

    private Vector3 anciennePositionCube;
    private Vector3 PositionCube;
    public Camera cam;
    public GameObject cube;
    private GameObject vide;
    private float dist;
    private float distZoneTp = 13.0f;

    void Start()
    {
        Screen.lockCursor = true;
        centreCamera = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
        vide = GameObject.Find("Vide");
        etat = Etat.Look;
        clef = false;
    }

    // Update is called once per frame
    void Update()
    {
        //affiche la souris
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
        RaycastHit hit;
        dist = Vector3.Distance(anciennePositionCube, cube.transform.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (chrono < 2)
            {
                cube.SetActive(true);
                nouvellePosition = hit.point;
                cube.transform.position = nouvellePosition;
                tagTouchee = hit.collider.tag;
            }
        }
        else
        {
            cube.SetActive(false);
            chrono = 0;
        }

        //affiche ou affiche pas le curseur
        if (Vector3.Distance(this.transform.position, cube.transform.position) <= distZoneTp)
        {
            cube.SetActive(true);
        }
        else
        {
            cube.SetActive(false);
        }


        if (etat == Etat.Look)
        {
            if (dist < 0.1f && Vector3.Distance(this.transform.position, cube.transform.position) <= distZoneTp)
            {
                chrono += Time.deltaTime;
            }
            else
            {
                chrono = 0;
                anciennePositionCube = nouvellePosition;
            }

            if (chrono > 2)
            {
                //etat = Etat.teleportation;
                etat = Etat.AnalyseCommande;
            }
        }
        else if (etat == Etat.AnalyseCommande)
        {
            
            if (tagTouchee == "terrain")
            {
                etat = Etat.fadeOut;
            }
            if (tagTouchee == "obstacle")
            {
                chrono = 0;
                etat = Etat.Look;
            }
            if (tagTouchee == "demi-tour")
            {
                etat = Etat.demiTour;
            }
            if (tagTouchee == "Coffre1")
            {
                Debug.Log(chrono);
                Sequence seq = DOTween.Sequence();
                GameObject.Find("Couvercle1").transform.DOMoveY(0.6f, 2);
                seq.Append(GameObject.Find("key_gold").transform.DOMoveY(0.3f, 2));
                seq.Append(GameObject.Find("key_gold").transform.DOMove(this.transform.position, 0.5f));
                clef = true;
                chrono = 0;
                etat = Etat.Look;
            }
            if (tagTouchee == "Coffre2")
            {
                GameObject.Find("Couvercle2").transform.DOMoveY(0.6f, 2);
                chrono = 0;
                etat = Etat.Look;
            }

            if (tagTouchee == "Pouttre")
            {
                if (clef == true)
                {
                    Sequence seq = DOTween.Sequence();
                    seq.Append(GameObject.Find("Pouttre").transform.DOMoveY(5.0f, 2));
                    seq.Insert(1, GameObject.Find("Porte2").transform.DOMoveX(3.5f, 5));
                    seq.Insert(1, GameObject.Find("Porte1").transform.DOMoveX(-0.05f, 5));
                    chrono = 0;
                    etat = Etat.Look;
                }
            }
        }
        else if (etat == Etat.fadeOut)
        {
            cam.GetComponent<OVRScreenFadeOut>().enabled = true;
            chronoFadeOut += Time.deltaTime;
            if (chronoFadeOut > cam.GetComponent<OVRScreenFadeOut>().fadeTime)
            {
                cam.GetComponent<OVRScreenFadeOut>().enabled = false;
                etat = Etat.teleportation;
                chronoFadeOut = 0;
            }
        }
        else if (etat == Etat.teleportation)
        {
            this.transform.position = new Vector3(nouvellePosition.x, nouvellePosition.y + 0.066f, nouvellePosition.z);

            cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.066f + 0.7f, this.transform.position.z);

            cameraOVR.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.066f + 2.5f, this.transform.position.z);

            etat = Etat.fadeIn;
        }
        else if (etat == Etat.fadeIn)
        {
            cam.GetComponent<OVRScreenFadeIn>().enabled = true;
            chronoFadeIn += Time.deltaTime;
            if (chronoFadeIn > cam.GetComponent<OVRScreenFadeIn>().fadeTime)
            {
                cam.GetComponent<OVRScreenFadeIn>().enabled = false;
                chronoFadeIn = 0;
                chrono = 0;
                etat = Etat.Look;
            }
        }
        else if (etat == Etat.demiTour)
        {
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
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Vide")
        {
            SceneManager.LoadScene("GameOver");
        }
        if (collision.gameObject.name == "Fin")
        {
            SceneManager.LoadScene("Fin");
        }
    }


}