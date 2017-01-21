using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	private int playerIndex = 0;

	[SerializeField]
	private int startHealth = 100;

	private int health;

	/// <summary>
	/// Retrieves the current players index
	/// </summary>
	/// <value>The player index</value>
	public int PlayerIndex 
	{ 
		get 
		{ 
			return this.playerIndex; 
		} 
	}

	/// <summary>
	/// Retireves the current players health
	/// </summary>
	/// <value>The health.</value>
	public int Health
	{
		get 
		{
			return health;
		}
		set 
		{
			health = value;
		}
	}

	void Start()
	{
		health = startHealth;
	}
}
