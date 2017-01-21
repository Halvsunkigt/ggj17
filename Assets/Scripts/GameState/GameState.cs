using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
	[SerializeField]
	private int initialCoins = 35;

	private int numCoins;

	/// <summary>
	/// The number of coins the players have available for them
	/// </summary>
	/// <value>The number coins.</value>
	public int NumCoins
	{
		get { 
			return numCoins;
		}
		set {
			numCoins = value;
		}
	}

	// Use this for initialization
	void Start ()
	{
		numCoins = initialCoins;
	}

	/// <summary>
	/// Try to spend some coin
	/// </summary>
	/// <returns><c>true</c>, if coins was spent, <c>false</c> otherwise.</returns>
	/// <param name="cost">Cost.</param>
	public bool SpendCoins(int cost) {
		var bought = numCoins >= cost;
		if (bought) {
			numCoins -= cost;
			Debug.Log ("Spending " + cost);
		} else {
			Debug.Log ("Could not afford " + cost);
		}
		return bought;
	}

	/// <summary>
	/// Give coins
	/// </summary>
	/// <param name="coins">Coins.</param>
	public void GiveCoins(int coins) {
		numCoins += coins;
		Debug.Log ("Give coins: " + coins);
	}
}
