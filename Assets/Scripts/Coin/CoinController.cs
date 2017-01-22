using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour 
{
	private static Quaternion UP = Quaternion.Euler (new Vector3 (-90, 0, 0));

	[SerializeField]
	private int gainCoins = 2;

	[SerializeField]
	private GameObject destroyEffect;

	void Start() {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			var gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
			gameState.GiveCoins (gainCoins);

			Instantiate (destroyEffect, transform.position, UP);
			Destroy (gameObject);
		}
	}
}
