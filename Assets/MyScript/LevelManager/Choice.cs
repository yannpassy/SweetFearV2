using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Choice : MonoBehaviour {
	public GameObject rejouer;
	public GameObject Quitter;
	public GameObject sphere;
	Color color = Color.red;
	public Renderer rend;
	private string TagTouchee;
	private RaycastHit hit;
	private double chrono;
	// Use this for initialization
	void Start () {
	
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit)) {
			TagTouchee = hit.collider.tag;
			sphere.transform.position = hit.point;
		}

		if (TagTouchee == "Rejouer") {
			chrono += Time.deltaTime;
			rend.material.color = Color.blue;
			if (chrono > 2) {
				SceneManager.LoadScene ("PetitProto");
			}
		}

		if (TagTouchee == "Quitter") {
			chrono += Time.deltaTime;
			rend.material.color = Color.red;
			if (chrono > 2) {
				Application.Quit ();
			}
		}
	}
}
