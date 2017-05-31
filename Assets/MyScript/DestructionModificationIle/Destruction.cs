using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Destruction : MonoBehaviour {
	private double chrono;
	private GameObject cube; 
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		chrono += Time.deltaTime;
		if (chrono > 5) {
			GameObject.Find ("Cube").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 10) {
			GameObject.Find ("Cube").transform.DOMoveY (-100, 20);
		}
		if (chrono > 15) {
			GameObject.Find ("Cube1").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 20) {
			GameObject.Find ("Cube1").transform.DOMoveY (-100, 20);
		}
		if (chrono > 25) {
			GameObject.Find ("Cube3").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 30) {
			GameObject.Find ("Cube3").transform.DOMoveY (-100, 20);
		}
		if (chrono > 35) {
			GameObject.Find ("Cube5").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 40) {
			GameObject.Find ("Cube5").transform.DOMoveY (-100, 20);
		}
		if (chrono > 45) {
			GameObject.Find ("Cube2").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 50) {
			GameObject.Find ("Cube2").transform.DOMoveY (-100, 20);
		}
		if (chrono > 55) {
			GameObject.Find ("Cube6").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 60) {
			GameObject.Find ("Cube6").transform.DOMoveY (-100, 20);
		}
		if (chrono > 65) {
			GameObject.Find ("Cube4").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 70) {
			GameObject.Find ("Cube4").transform.DOMoveY (-100, 20);
		}
		if (chrono > 75) {
			GameObject.Find ("Cube7").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 80) {
			GameObject.Find ("Cube7").transform.DOMoveY (-100, 20);
		}
		if (chrono > 85) {
			GameObject.Find ("Cube9").transform.DOShakeRotation (10, 2, 5, 10, true);
		}
		if (chrono > 90) {
			GameObject.Find ("Cube9").transform.DOMoveY (-100, 20);
		}
	}
}