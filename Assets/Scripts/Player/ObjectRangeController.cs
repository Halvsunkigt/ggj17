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
		if (Input.GetButtonDown("Fire1_Player" + player.PlayerIndex)) {
			if (IsCollidingWithObject () && !IsCarryingObject ()) { 
				StartCarryingObject (collidingObject);
			} else if (IsCarryingObject ()) {
				StopCarryingObject ();
			}
		}

		carryingTime += Time.fixedDeltaTime;
	}

	/// <summary>
	/// Start carrying object
	/// </summary>
	/// <param name="collidingObject">Colliding object.</param>
	public void StartCarryingObject (GameObject collidingObject)
	{
		carryingObject = collidingObject;
		this.collidingObject = null;

		// Update parent node so that the carrying object is moved around with the player
		Vector3 offset = gameObject.transform.up * (sphereCollider.radius / 2.0f);
		carryingObject.transform.parent = gameObject.transform;
		carryingObject.transform.position = gameObject.transform.position + offset;
		carryingObject.transform.rotation = new Quaternion ();
		carryingTime = 0;
	}

	void StopCarryingObject ()
	{
		if (carryingObject == null && carryingTime > 0.5f) {
			return;
		}

		Vector3 offset = gameObject.transform.forward * (sphereCollider.radius / 2.0f);
		carryingObject.transform.position = gameObject.transform.position + offset;
		carryingObject.transform.parent = null;
		carryingObject = null;

		// TODO: Snap to grid!
	}

	void OnTriggerEnter(Collider other)
	{
		if (IsCarryingObject()) {
			return;
		}

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

