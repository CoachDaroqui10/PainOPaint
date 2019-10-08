using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public int lives = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "bullet(Clone)" || col.gameObject.name == "bullet2(Clone)") {
			Destroy (col.gameObject);

			if (lives > 0) {
				lives--;
			}
		}
	}
}
