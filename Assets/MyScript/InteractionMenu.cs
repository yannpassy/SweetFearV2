﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMenu : MonoBehaviour
{
    public OVRCameraRig cameraOVR;
    public Camera cam;
    private enum Etat { Look, EcranTitre };
    Etat etat;
    private Vector3 centreCamera;
    private Vector3 nouvellePosition;
    private string tagTouchee;

    private Vector3 anciennePositionCurseur;
    public GameObject curseur;
    private float dist;
    private double chrono;
    public GameObject progressBar;
    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;
        centreCamera = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, cameraOVR.transform.forward.z);
        etat = Etat.Look;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(chrono);
        //affiche la souris
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape))
            Screen.lockCursor = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // pour l'oculus, mettre centreCamera par Input.mousePosition
        RaycastHit hit;
        dist = Vector3.Distance(anciennePositionCurseur, curseur.transform.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
            if (tagTouchee == "EcranTitre")
            {
                progressBar.GetComponent<Image>().fillAmount += Time.deltaTime;
            }
            else
            {
                anciennePositionCurseur = nouvellePosition;
                progressBar.GetComponent<Image>().fillAmount = 0;
            }

            if (progressBar.GetComponent<Image>().fillAmount >= 1)
            {
                //etat = Etat.EcranTitre;
            }
        }
    }
}
