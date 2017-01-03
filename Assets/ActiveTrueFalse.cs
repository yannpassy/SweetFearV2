using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTrueFalse : MonoBehaviour {
    public double chrono;
    public int compteur;
    public int nombreEnfant;
    public int EnfantActuel;
    // Use this for initialization
    void Start () {
        ChangeSetActive();
	}
	
	// Update is called once per frame
	void Update () {

        if (EnfantActuel < nombreEnfant)
        {
            chrono += Time.deltaTime;
            if (chrono > 0.1)
            {
                GameObject monGameObject = transform.GetChild(EnfantActuel).gameObject;
                if (monGameObject.tag == "fragment")
                {
                    ChangeGameObjectKinematic(monGameObject, false, true);
                }
                chrono = 0;
                EnfantActuel++;
            }
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
                monGameObject.SetActive(true);
            }
            else
            {
                monGameObject.SetActive(false);
            }
        }
    }
}
