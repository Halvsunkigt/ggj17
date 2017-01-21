using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(PlayerController))]
public class ObjectRangeController : MonoBehaviour 
{
	[SerializeField]
	private string[] carryableObjects = new string[] {
		"Box", "Player", "Turret"
	};

	private PlayerController player;
	private GameObject carryingObject;
	private GameObject collidingObject;

	void Start () 
	{
		
	}

	void FixedUpdate() 
	{
		if (!IsCollidingWithObject ()) {
			return;
		}

		if (Input.GetButtonDown("Fire1_Player" + player.playerIndex)) {
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (IsCarryingObject()) {
			return;
		}

		for (var i = 0; i < carryableObjects.Length; ++i) {
			if (carryableObjects [i].Equals (other.tag)) {
				collidingObject = other.gameObject;
				break;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!IsCarryingObject (other.gameObject)) {
			return;
		}
	}

	private bool IsCarryingObject() 
	{
		return carryingObject != null;
	}

	private bool IsCollidingWithObject() 
	{
		return collidingObject != null;
	}

	private bool IsCarryingObject(GameObject obj)
	{
		return obj.Equals (carryingObject);
	}
}
