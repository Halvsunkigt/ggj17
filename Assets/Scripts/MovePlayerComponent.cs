using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class MovePlayerComponent : MonoBehaviour 
{
	[SerializeField]
	private float speed = 3.0f;

	private PlayerMotor playerMotor;

	// Use this for initialization
	void Start () 
	{
		playerMotor = GetComponent<PlayerMotor> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void FixedUpdate() 
	{
		var vertical = Input.GetAxisRaw ("Vertical");
		var horizontal = Input.GetAxisRaw ("Horizontal");

		playerMotor.MoveForward (vertical * speed);
		playerMotor.MoveRight (horizontal * speed);
	}
}
