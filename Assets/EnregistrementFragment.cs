using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnregistrementFragment : MonoBehaviour {
    public Rigidbody rb;
    private bool boolean;
    public GameObject monFragment;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        boolean = true;
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<MeshCollider>().isTrigger == true)
        {
            rb.constraints = RigidbodyConstraints.None;
            
        }
    }
}
