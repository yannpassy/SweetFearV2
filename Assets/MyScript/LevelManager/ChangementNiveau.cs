using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementNiveau : MonoBehaviour {
    private int index;
    private float chrono;

	// Use this for initialization
	void Start () {
        index = 0;
        chrono = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (chrono > 1)
        {
            this.gameObject.transform.GetChild(index).gameObject.SetActive(true);
            index++;
            chrono = 0;
        }else
        {
            chrono += Time.deltaTime;
        }

        if(index == this.transform.childCount)
        {
            SceneManager.LoadScene("NiveauEte");
        }
	}
}
