using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBall : MonoBehaviour {
    // Use this for initialization
    public GameObject sphere;
    void Start () {
		
	}

    // Update is called once per frame
    void OnCollisionStay(Collision col)
    {
        Debug.Log(name+ " OnCollisionStay " + col.gameObject.tag + " "+ col.gameObject.name);
        if (col.gameObject.tag == "boule")
        {
            col.gameObject.GetComponent<MeshCollider>().isTrigger = true;
            Debug.Log("la collision est avec l'objet :" + gameObject.name);
        }
        else
        {
            col.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
