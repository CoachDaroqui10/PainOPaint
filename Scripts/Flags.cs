using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flags : MonoBehaviour {

	Vector3 initPos;

	public PlayerHealth health;

	// Use this for initialization
	void Start () {
		health = GetComponent<PlayerHealth> ();

		initPos= this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		if (this.transform.parent != GameObject.Find("Player")) {
			transform.position = initPos;
		}
		if (health.lives == 0) {
			transform.parent = GameObject.Find ("Flag1Pos").transform;
		}

	}
}
