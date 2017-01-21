using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[SerializeField]
	private int playerIndex = 0;

	[SerializeField]
	private int startHealth = 100;

	[SerializeField]
	private float throwPower = 5.0f;

	[SerializeField]
	private float initialMovementSpeed = 3.0f;

	private int health;
	private float movementSpeed;

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
	/// <value></value>
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

	/// <summary>
	/// The power used when throwing an object
	/// </summary>
	/// <value></value>
	public float ThrowPower
	{
		get
		{
			return throwPower;
		}
	}

	/// <summary>
	/// The initial movement speed
	/// </summary>
	/// <value></value>
	public float InitialMovementSpeed
	{
		get {
			return initialMovementSpeed;
		}
	}

	/// <summary>
	/// The current movement speed
	/// </summary>
	/// <value></value>
	public float MovementSpeed
	{
		get {
			return movementSpeed;
		}
		set {
			movementSpeed = value;
		}
	}

	void Start()
	{
		health = startHealth;
		movementSpeed = initialMovementSpeed;
	}
}
