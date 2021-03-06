﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController), typeof(PlayerController))]
public class MovePlayerComponent : MonoBehaviour 
{
	private PlayerController player;
	private MovementController movement;
	private Vector3 rotate;

	void Start () 
	{
		player = GetComponent<PlayerController> ();
		movement = GetComponent<MovementController> ();
	}

	void FixedUpdate() 
	{
		if (player.ControlsLocked) {
			return;
		}

		var v = Input.GetAxisRaw ("Vertical_Player" + player.PlayerIndex);
		var h = Input.GetAxisRaw ("Horizontal_Player" + player.PlayerIndex);

	    // we use world-relative directions in the case of no main camera
	    rotate = v * Vector3.forward + h * Vector3.right;

	    movement.Rotate (rotate);
	    movement.Move (h, v);
	}
}
