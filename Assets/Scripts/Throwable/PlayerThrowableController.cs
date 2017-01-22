using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerThrowableController : ThrowableController
{
	private PlayerController player;

	void Start () 
	{
		player = GetComponent<PlayerController> ();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag != "Player") {
			player.ControlsLocked = false;
		}
	}

	protected override void OnPutDown ()
	{
		player.ControlsLocked = false;
	}

	protected override void OnLiftUp ()
	{
		player.ControlsLocked = true;
	}
}
