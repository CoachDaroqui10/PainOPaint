using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int lives = 5;
	public Sprite[] Life;
	public Image myImageComponent;
	public Sprite fullLife;
	public Sprite penalty_Image;
	public float penalty_Time;
	public Text p_Time;
	public Sprite sight;
	public Image sight_Penalty;

	float startTime;
	bool exit;
	bool enter;
	float t;

	// Use this for initialization
	void Start () {
		penalty_Time = 10;
		exit = false;
		enter = false;
	}

	// Update is called once per frame
	void Update () {

		if (lives == 0) {
			sight_Penalty.sprite = fullLife;
		} else {
			sight_Penalty.sprite = sight;
			t = 0;
		}			

		if (enter && !exit) {
			myImageComponent.sprite = penalty_Image;
			string seconds = (t % 60).ToString ("f0");
			p_Time.text = seconds;
			if (t < penalty_Time) {
				t = Time.time - startTime;
			} else {
				lives = 5;
				enter = false;
				myImageComponent.sprite = fullLife;
			}
		} else {
			p_Time.text = "";
		}
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "bullet(Clone)" || col.gameObject.name == "bullet2(Clone)") {
			Destroy (col.gameObject);

			if (lives > 0) {
				p_Time.text = "";
				lives--;
				if (lives != 0) {
					myImageComponent.sprite = Life [lives];
				}
			}
			if (lives == 0){
				myImageComponent.sprite = Life [lives];
			}
		}
	}

	void OnTriggerEnter(Collider col){
		if (lives == 0){
			if (col.gameObject.name == "Penalty1") {
				startTime = Time.time;
				exit = false;
				enter = true;
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.name == "Penalty1") {

			if (lives == 0) {
				if (t < penalty_Time) {
					exit = true;
					myImageComponent.sprite = Life [0];
				} else {
					lives = 5;
					t = 0;
					myImageComponent.sprite = fullLife;
					exit = false;
					enter = false;
				}
			}

		}
	}
}
