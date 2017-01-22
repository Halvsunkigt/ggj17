using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsAmountController : MonoBehaviour {

	private GameState gameState;

	// Use this for initialization
	void Start ()
	{
		gameState = GameObject.Find ("GameState").GetComponent<GameState> ();
	}

	void Update ()
	{
		var text = GetComponent<Text> ();
		text.text = gameState.NumCoins.ToString ();
	}
}
