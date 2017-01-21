﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(PlayerController))]
public class ObjectRangeController : MonoBehaviour 
{
	private const string CARRY_BUTTON = "Fire1_Player";
	private const string THROW_BUTTON = "Fire2_Player";

	private string[] carryableObjects = new string[] {
		"Box", "Player", "Turret", "VendingMachine"
	};

	private PlayerController player;
	private SphereCollider sphereCollider;

	private GameObject carryingObject;
	private GameObject collidingObject;

	private float carryingTime = 0;

	void Start () 
	{
		player = GetComponent<PlayerController> ();
		sphereCollider = GetComponent<SphereCollider> ();
	}

	void FixedUpdate() 
	{
		if (Input.GetButtonDown (CARRY_BUTTON + player.PlayerIndex)) {
			OnActionButtonDown ();
		} else if (Input.GetButtonDown (THROW_BUTTON + player.PlayerIndex)) {
			OnThrowButtonDown ();
		}

		carryingTime += Time.fixedDeltaTime;
	}

	private void OnActionButtonDown ()
	{
		if (IsCollidingWithObject () && !IsCarryingObject ()) { 
			StartCarryingObject (collidingObject);
		} else if (IsCarryingObject ()) {
			StopCarryingObject ();
		}
	}

	private void OnThrowButtonDown ()
	{
		if (!IsCarryingObject ()) {
			return;
		}

		ThrowableComponent throwable = carryingObject.GetComponent<ThrowableComponent> ();
		if (throwable == null) {
			Debug.LogError ("Object " + carryingObject.name + " is not allowed to be thrown. Dropping it instead");
			StopCarryingObject ();
			return;
		}

		Vector3 throwArc = (gameObject.transform.forward + gameObject.transform.up).normalized;
		throwable.Throw (throwArc * player.ThrowPower);

		carryingObject.transform.parent = null;
		carryingObject = null;

		player.MovementSpeed = player.InitialMovementSpeed;
	}

	/// <summary>
	/// Start carrying object
	/// </summary>
	/// <param name="collidingObject">Colliding object.</param>
	public void StartCarryingObject (GameObject collidingObject)
	{
		VendingMachineController vendingMachine = collidingObject.GetComponent<VendingMachineController> ();
		if (vendingMachine != null) {
			vendingMachine.TryBuyItem (this);
			return;
		}

		ThrowableComponent throwable = collidingObject.GetComponent<ThrowableComponent> ();
		if (throwable != null) {
			throwable.Disable ();
		}

		carryingObject = collidingObject;
		this.collidingObject = null;

		// Update parent node so that the carrying object is moved around with the player
		Vector3 offset = gameObject.transform.up * (sphereCollider.radius / 2.0f);
		carryingObject.transform.parent = gameObject.transform;
		carryingObject.transform.position = gameObject.transform.position + offset;
		carryingObject.transform.rotation = new Quaternion ();
		carryingTime = 0;

		player.MovementSpeed = player.InitialMovementSpeed / 2.0f;
	}

	private void StopCarryingObject ()
	{
		if (carryingObject == null && carryingTime > 0.5f) {
			return;
		}

		ThrowableComponent throwable = carryingObject.GetComponent<ThrowableComponent> ();
		if (throwable != null) {
			throwable.PutDown ();
		}

		Vector3 offset = gameObject.transform.forward * (sphereCollider.radius / 2.0f);
		carryingObject.transform.position = gameObject.transform.position + offset;
		carryingObject.transform.parent = null;
		carryingObject = null;

		player.MovementSpeed = player.InitialMovementSpeed;
	}

	void OnTriggerEnter(Collider other)
	{
		if (IsCarryingObject()) {
			return;
		}

		CheckForCollision (other);
	}

	private void CheckForCollision(Collider other)
	{
		for (var i = 0; i < carryableObjects.Length; ++i) {
			if (carryableObjects [i].Equals (other.tag)) {
				collidingObject = other.gameObject;
				Debug.Log ("Colliding with object: " + other.gameObject.name);
				break;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!IsCollidingWithObject (other.gameObject)) {
			return;
		}

		collidingObject = null;
	}

	void OnTriggerStay(Collider other)
	{
		if (IsCarryingObject() || IsCollidingWithObject()) {
			return;
		}

		CheckForCollision (other);
	}

	private bool IsCarryingObject() 
	{
		return carryingObject != null;
	}

	private bool IsCarryingObject(GameObject obj)
	{
		return obj.Equals (carryingObject);
	}

	private bool IsCollidingWithObject() 
	{
		return collidingObject != null;
	}

	private bool IsCollidingWithObject(GameObject obj) 
	{
		return obj.Equals(collidingObject);
	}

}

