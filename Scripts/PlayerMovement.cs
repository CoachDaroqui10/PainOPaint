using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float speed = 12;
	public Sprite fullLife;
	public Sprite countdown;
	public Image startCount;
	public Text startText;
	public Text flagCaptured;

	Animator anim;
	PlayerHealth p_Health;
	public GameManager gm;
	private float x2;
	private float z2;
	private bool isDead;
	Rigidbody rb;
	bool flag;
	float startTime;
	float time_left;

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody> ();
		p_Health = GetComponent<PlayerHealth> ();
		isDead = false;
		x2 = 0;
		z2 = 0;
		startTime = Time.time;

	}
	
	// Update is called once per frame
	void Update () {

		float t = Time.time - startTime;

		if (t < 4) {

			time_left = 4 - t;
			string seconds = (time_left % 60).ToString ("f0");
			startText.text = seconds;
			startCount.sprite = countdown;

		} else {

			startCount.sprite = fullLife;
			startText.text = "";

			float x = Input.GetAxis ("Horizontal") * Time.deltaTime * speed;
			float z = Input.GetAxis ("Vertical") * Time.deltaTime * speed;

			if ((x != x2) || (z != z2)) {
				this.transform.Translate (x, 0, z);
				x2 = x;
				z2 = z;
				anim.SetBool ("IsWalking", true);
			} else {
				anim.SetBool ("IsWalking", false);
				rb.constraints = RigidbodyConstraints.FreezeAll;
			}

			if (flag) {
				GameObject.Find ("Flag2").transform.position = transform.position;
			}

			if (p_Health.lives == 0 && flag) {

				flag = false;
				GameObject.Find ("Flag2").transform.position = GameObject.Find ("Flag2").transform.parent.position;

			}

			if (flag) {
				flagCaptured.text = "FLAG CAPTURED";
			} else {
				flagCaptured.text = "";
			}
		}
	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "Flag2") {
			flag = true;
		}
		if (col.gameObject.name == "Flag1" && flag) {
			flag = false;
			GameObject.Find ("Flag2").transform.position = GameObject.Find ("Flag2").transform.parent.position;

			gm.score1++;
			gm.updateScore ();
		}

	}
}
