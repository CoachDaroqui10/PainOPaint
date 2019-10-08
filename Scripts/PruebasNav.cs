using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PruebasNav : MonoBehaviour {

	NavMeshAgent nav;
	Animator anim;
	bool waiting;
	bool reached;
	bool flag;
	GameObject[] enemies;
	int rand;
	Health health;
	float time = 0;
	Collider cl;
	AIShooting ais;

	public Transform[] positions;
	public int duty;
	public GameManager gm;
	public Text flagCaptured;

	public int team;
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		health = GetComponent<Health> ();
		ais = GetComponent<AIShooting> ();

		anim.SetBool ("IsWalking", true);

		waiting = false;
		reached = false;

		rand = Random.Range (0, positions.Length);

		if (team == 1) {
			enemies = GameObject.FindGameObjectsWithTag("Team 2");
		} else {
			enemies = GameObject.FindGameObjectsWithTag("Team 1");
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (health.lives > 0) {
			
			if (duty == 0) {

				nav.speed = 20;

				if (ais.enem == -1) {
					nav.SetDestination (positions [0].transform.position);
					if (Vector3.Distance(nav.destination, transform.position) < 5) {
						nav.SetDestination (transform.position);
						anim.SetBool ("IsWalking", false);

						nav.speed = 0;
					}
		
				} else if (ais.enem != -1 && enemies[ais.enem].GetComponent<PruebasNav>().health.lives > 0){
					anim.SetBool ("IsWalking", true);
					nav.SetDestination (enemies [ais.enem].transform.position);
				}

			}
			else if (duty == 1) {

				if (ais.enem == -1) {
					if (reached) {
						reached = false;
						rand = Random.Range (0, positions.Length);
					}

					Vector3 pos = new Vector3 (positions[rand].transform.position.x, 
						positions[rand].transform.position.y,
						positions[rand].transform.position.z);

					if (!waiting) {
						nav.SetDestination (pos);
						if (Vector3.Distance(pos, transform.position) < 25) {
							Rigidbody rb = GetComponent<Rigidbody> ();
							reached = true;
						}
					}
				} 

				else if (ais.enem != -1 && enemies[ais.enem].GetComponent<PruebasNav>().health.lives > 0) {
					nav.SetDestination (enemies [ais.enem].transform.position);
				}
					


			} 
			else {
				if (!flag) {
					nav.SetDestination(GameObject.Find("Flag1").transform.position);
				} 
				else {
					nav.SetDestination(GameObject.Find("Flag2Pos").transform.position);
				}

				if (flag) {
					GameObject.Find ("Flag1").GetComponent<Collider> ().enabled = false;
					GameObject.Find ("Flag1").transform.position = transform.position;
				}

				if (flag) {
					flagCaptured.text = "FLAG CAPTURED";
				} else {
					flagCaptured.text = "";
				}
			}
		} 

		else {

			nav.speed = 20;

			anim.SetBool ("IsWalking", true);

			if (team == 1) {
				nav.SetDestination (GameObject.Find ("Penalty1").transform.position);
			} else {
				nav.SetDestination (GameObject.Find ("Penalty2").transform.position);
			}

			if (Vector3.Distance(nav.destination, transform.position) < 15) {
				if (time < 10) {
					time += Time.deltaTime;
				} else {
					health.lives = 5;
					time = 0;
				}
			}

			if (duty == 2) {
				if (!flag) {
					flagCaptured.text = "";
				}
			}
		}

		if (health.lives == 0 && flag) {

			GameObject.Find ("Flag1").GetComponent<Collider> ().enabled = true;

			flag = false;
			GameObject.Find ("Flag1").transform.position = GameObject.Find ("Flag1").transform.parent.position;

		}
	}

	void wait(){

		while (time < 10) {
			
		}

		time = 0;
		health.lives = 5;

	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "Flag1") {
			flag = true;
		}
		if (col.gameObject.name == "Flag2" && flag) {
			flag = false;
			GameObject.Find ("Flag1").GetComponent<Collider> ().enabled = true;
			GameObject.Find ("Flag1").transform.position = GameObject.Find ("Flag1").transform.parent.position;

			gm.score2++;
			gm.updateScore ();
		}

	}
}
	