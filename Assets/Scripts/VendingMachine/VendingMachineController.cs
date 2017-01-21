using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineController : MonoBehaviour
{
	[SerializeField]
	private int cost = 15;

	[SerializeField]
	private GameObject prefab;

	private GameState gameState;

	// Use this for initialization
	void Start () {
		var state = GameObject.Find ("GameState");
		gameState = state.GetComponent<GameState> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Buy a new item and carry it
	/// </summary>
	/// <param name="player">The player</param>
	/// <returns>The turret.</returns>
	public void TryBuyItem(ObjectRangeController player)
	{
		if (gameState.SpendCoins (cost)) {
			var obj = Instantiate (prefab);
			player.StartCarryingObject (obj);
			return;
		}
	}
}
