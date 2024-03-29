using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour {

	float distance;
	float shootDistance = 50;
	float shootDistanceCamper = 80;
	bool shooting = false;
	GameObject[] enemies;
	PruebasNav mov;
	Animator anim;
	bool detected;
	Health health;
	AudioSource shot;

	public int enem = -1;
	Vector3 targetPoint;
	Quaternion targetRot;


	public Rigidbody bullet;
	public GameObject gunTip;

	public int team;

	// Use this for initialization
	void Start () {

		int enem = -1;

		anim = GetComponent<Animator> ();
		health = GetComponent<Health> ();
		mov = GetComponent<PruebasNav> ();
		shot = GetComponent<AudioSource> ();

		if (team == 1) {
			enemies = GameObject.FindGameObjectsWithTag("Team 2");
		} else {
			enemies = GameObject.FindGameObjectsWithTag("Team 1");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (health.lives > 0) {
			
			for (int i = 0; i < enemies.Length; i++) {
				distance = Vector3.Distance (transform.position, enemies[i].transform.position);

				float compare;

				if (mov.duty == 0) {
					compare = shootDistanceCamper;
				} else {
					compare = shootDistance;
				}

				if (distance < compare) {
					detected = true;
					enem = i;
					break;
				} else {
					enem = -1;
					detected = false;
				}
			}

			if (enem != -1) {
				transform.LookAt (enemies [enem].transform.position);
			}

			if (!shooting && detected) {
				StartCoroutine ("Shoot");
			}
		}
	}

	IEnumerator Shoot(){
		shooting = true;

		Rigidbody bulletShot = Instantiate (bullet, gunTip.transform.position,
			                       gunTip.transform.rotation);

		if (shot.isPlaying) {
			shot.Stop();
		}

		shot.Play ();

		bulletShot.AddForce (transform.forward * 4000);

		yield return new WaitForSeconds (0.3f);

		shooting = false;
	}
}
