using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ThrowableController : MonoBehaviour
{
	public void Throw(Vector3 velocity)
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = velocity;
		rb.isKinematic = false;
		enabled = true;
	}

	public void Disable()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		enabled = false;
	}

	public void PutDown()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		enabled = true;
		rb.isKinematic = false;
	}
}
