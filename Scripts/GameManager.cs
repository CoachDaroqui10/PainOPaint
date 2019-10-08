using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int score1;
	public int score2;
	public Sprite[] flags1;
	public Sprite[] flags2;

	public Image team1;
	public Image team2;

	// Use this for initialization
	void Start () {
		
		score1 = 0;
		score2 = 0;

	}
	
	// Update is called once per frame
	void Update () {

		if (score1 == 3) {
			SceneManager.LoadScene ("BlueWins");
		} else if (score2 == 3) {
			SceneManager.LoadScene ("OrangeWins");
		}

		if (Input.GetKeyDown(KeyCode.M)){
			SceneManager.LoadScene("Game");
		}

	}

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "bullet(Clone)" || col.gameObject.name == "bullet2(Clone)") {
			Destroy (col.gameObject);
		}
	}

	public void updateScore (){

		team1.sprite = flags1 [score1];
		team2.sprite = flags2 [score2];

	}
}
