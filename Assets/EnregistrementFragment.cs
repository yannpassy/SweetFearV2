﻿using System.Collections;
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

	
}
