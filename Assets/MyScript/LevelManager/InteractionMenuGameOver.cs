using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InteractionMenuGameOver : MonoBehaviour {
    public OVRCameraRig cameraOVR;
    public Camera cam;
    private enum Etat { Look , enValidation};
    Etat etat;
    private Vector3 centreCamera;
    private Vector3 nouvellePosition;
    private string tagTouchee;
    private string tagSelectionnee;

    private Vector3 anciennePositionCurseur;
    public GameObject curseur;
    private float dist;
    private double chrono;
    public GameObject progressBarRecommencer;
    public GameObject progressBarQuitter;
    private GameObject particleRecommencer;
    private GameObject particleQuitter;
    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;
        centreCamera = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
        etat = Etat.Look;
        particleRecommencer = GameObject.Find("ParticleRecommencer");
        particleRecommencer.SetActive(false);
        particleQuitter = GameObject.Find("ParticleQuitter");
        particleQuitter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(chrono);
        //affiche la souris
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = false;

       // Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
        RaycastHit hit;
        dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);

        if (Physics.Raycast(new Ray(cam.transform.position, cam.transform.rotation * Vector3.forward), out hit, Mathf.Infinity))
        {
            if (chrono < 2)
            {
                curseur.SetActive(true);
                nouvellePosition = hit.point;
                curseur.transform.position = nouvellePosition;
                tagTouchee = hit.collider.tag;
            }
        }
        else
        {
            curseur.SetActive(false);
            chrono = 0;
            tagTouchee = "";
        }

        if (etat == Etat.Look)
        {
            progressBarRecommencer.GetComponent<Image>().fillAmount = 0;
            progressBarQuitter.GetComponent<Image>().fillAmount = 0;

            if (tagTouchee == "Recommencer" || tagTouchee == "Quitter")
            {
                tagSelectionnee = tagTouchee;
                etat = Etat.enValidation;
            }
            
        }
        else if (etat == Etat.enValidation)
        {
            if (tagSelectionnee == tagTouchee)
            {
                if (tagSelectionnee == "Recommencer")
                {
                    progressBarRecommencer.GetComponent<Image>().fillAmount += Time.deltaTime / 1.75f;
                    particleQuitter.GetComponent<ParticleSystem>().startLifetime = 0f;
                    particleRecommencer.SetActive(true);
                    particleRecommencer.GetComponent<ParticleSystem>().startLifetime = 0.5f;
                }
                else
                {
                    progressBarQuitter.GetComponent<Image>().fillAmount += Time.deltaTime / 1.75f;
                    particleRecommencer.GetComponent<ParticleSystem>().startLifetime = 0f;
                    particleQuitter.SetActive(true);
                    particleQuitter.GetComponent<ParticleSystem>().startLifetime = 0.5f;
                }
            }
            else
            {
                anciennePositionCurseur = nouvellePosition;
                particleRecommencer.GetComponent<ParticleSystem>().startLifetime = 0f;
                particleQuitter.GetComponent<ParticleSystem>().startLifetime = 0f;
                etat = Etat.Look;
            }

            if (progressBarRecommencer.GetComponent<Image>().fillAmount >= 1)
            {
				if (ApplicationMode.passlevel == 1) {
					SceneManager.LoadScene("NiveauHiver");
				}
				if (ApplicationMode.passlevel == 2) {
					SceneManager.LoadScene("NiveauEte");
				}
       
            }
            else if (progressBarQuitter.GetComponent<Image>().fillAmount >= 1)
            {
                Application.Quit();
            }
        }
    }
}
