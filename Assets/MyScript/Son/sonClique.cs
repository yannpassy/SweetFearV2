using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class sonClique : MonoBehaviour
{

    public AudioClip bruit;
    AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("it's work");
            audio.PlayOneShot(bruit, 1.0F);
        }
    }

}