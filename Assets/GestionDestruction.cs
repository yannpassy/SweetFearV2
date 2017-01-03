using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionDestruction : MonoBehaviour {
    public float[] dur = new float[10];
    public GameObject[] mesGameObject = new GameObject[10];
    public float chrono;
	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update() {
        float chronoOld = chrono;
        chrono += Time.deltaTime;

        if (chronoOld < dur[0] && chrono >= dur[0])
        {
            mesGameObject[0].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[1] && chrono >= dur[1])
        {
            mesGameObject[1].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[2] && chrono >= dur[2])
        {
            mesGameObject[2].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[3] && chrono >= dur[3])
        {
            mesGameObject[3].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[4] && chrono >= dur[4])
        {
            mesGameObject[4].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[5] && chrono >= dur[5])
        {
            mesGameObject[5].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[6] && chrono >= dur[6])
        {
            mesGameObject[6].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[7] && chrono >= dur[7])
        {
            mesGameObject[7].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[8] && chrono >= dur[8])
        {
            mesGameObject[8].GetComponent<ActiveTrueFalse>().enabled = true;
        }
        if (chronoOld < dur[9] && chrono >= dur[9])
        {
            mesGameObject[9].GetComponent<ActiveTrueFalse>().enabled = true;
        }
    }
}
