using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ThrowableComponent : MonoBehaviour
{
	private Rigidbody rb;
	private Collider collider;

	protected Rigidbody RigidBody
	{
		get {
			return rb;
		}
	}

	protected Collider Collider
	{
		get {
			return collider;
		}
	}

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		collider = GetComponent<Collider> ();
	}

	public void Throw(Vector3 velocity)
	{
		rb.velocity = velocity;
		rb.isKinematic = false;
		enabled = true;
	}

	public void Disable()
	{
		rb.isKinematic = true;
		enabled = false;
	}

	public void PutDown()
	{
		enabled = true;
		rb.isKinematic = false;
	}
}
