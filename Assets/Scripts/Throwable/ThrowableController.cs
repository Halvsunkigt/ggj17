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

	public void LiftUp()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		enabled = false;
		OnLiftUp ();
	}

	public void PutDown()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		enabled = true;
		OnPutDown ();
	}

	protected virtual void OnLiftUp()
	{
	}

	protected virtual void OnPutDown()
	{
	}
}
