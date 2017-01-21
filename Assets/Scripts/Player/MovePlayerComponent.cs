using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class MovePlayerComponent : MonoBehaviour 
{
	[SerializeField]
	private int playerIndex = 0;

	private Vector3 rotate;
	private MovementController movement;

	void Start () 
	{
		movement = GetComponent<MovementController> ();
	}

	void FixedUpdate() 
	{
		var v = Input.GetAxisRaw ("Vertical_Player" + playerIndex);
		var h = Input.GetAxisRaw ("Horizontal_Player" + playerIndex);

		// we use world-relative directions in the case of no main camera
		rotate = v * Vector3.forward + h * Vector3.right;

		movement.Rotate (rotate);
		movement.Move (h, v);
	}
}
