using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	bool shooting;
	float time;
	bool isDead;
	PlayerHealth health;
	AudioSource shot;

	public Rigidbody bullet;
	public GameObject gunTip;

	// Use this for initialization
	void Start () {
		shooting = false;
		isDead = false;
		health = GetComponent<PlayerHealth>();
		shot = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		gunTip.transform.rotation = Quaternion.Euler (Camera.main.transform.rotation.eulerAngles.x + -10, 
			Camera.main.transform.rotation.eulerAngles.y, Camera.main.transform.rotation.eulerAngles.z);

		if (health.lives > 0) {
			if (!shooting) {
				if (Input.GetAxis ("Fire1") > 0) {

					Rigidbody bulletShot = Instantiate (bullet, gunTip.transform.position,
						                      gunTip.transform.rotation);
					if (shot.isPlaying) {
						shot.Stop();
					}

					shot.Play ();
					
					bulletShot.AddForce (gunTip.transform.forward * 4000);

					shooting = true;
				}
			} else {
				time += Time.deltaTime;

				if (time > 0.15) {
					shooting = false;
					time = 0;
				}
			}
		}
	}
}
