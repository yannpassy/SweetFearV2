using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrueFalse : MonoBehaviour {
    public double chrono;
    public int compteur;
    public int nombreEnfant;
    public int EnfantActuel;
	public GameObject fracturePrincipale;
    // Use this for initialization
    void Start () {
        ChangeSetActive();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (nombreEnfant);

		if (EnfantActuel < nombreEnfant) {
			Debug.Log (EnfantActuel);
			chrono += Time.deltaTime;
			if (chrono > 0.1) {
				
				GameObject monGameObject = transform.GetChild (EnfantActuel).gameObject;
				if (monGameObject.tag == "fragment") {

					ChangeGameObjectKinematic (monGameObject, false, true);

				}
				EnfantActuel++;
			} 
		} else {
			GameObject monGameObject = transform.GetChild (30).gameObject;
			monGameObject.GetComponent<MeshCollider> ().enabled = true;
			monGameObject.transform.tag = "obstacle";
		}
    }

    void ChangeGameObjectKinematic(GameObject monGameObject, bool kinematicValue, bool IsTriggerValue) {
        Rigidbody rb = monGameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = kinematicValue;
            monGameObject.GetComponent<MeshCollider>().isTrigger = IsTriggerValue;
        }

    }

    void ChangeSetActive()
    {
        EnfantActuel = 0;
        nombreEnfant = transform.childCount;
        for (int i = 0; i < nombreEnfant; i++)
        {
            GameObject monGameObject = transform.GetChild(i).gameObject;
            if (monGameObject.tag == "fragment")
            {
                monGameObject.SetActive (true);
            }
            else
            {
				monGameObject.GetComponent<MeshCollider> ().enabled = false;
				monGameObject.GetComponent<MeshRenderer> ().enabled = false;
            }
        }
    }
}
