using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCristal : MonoBehaviour {
    public float chrono = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "curseur")
        {
            chrono += Time.deltaTime;
            Debug.Log("chrono");
            if (chrono > 2)
            {
                this.gameObject.transform.GetChild(0).gameObject.transform.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
