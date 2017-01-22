using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ThrowableController : MonoBehaviour
{
	private bool carried = false;

	public bool IsCarried
	{
		get {
			return carried;
		}
	}

	public void Throw(Vector3 velocity)
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.velocity = velocity;
		rb.isKinematic = false;
		carried = false;
		enabled = true;
	}

	public void LiftUp()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		enabled = false;
		carried = true;
		OnLiftUp ();
	}

	public void Disable()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		enabled = false;
		carried = false;
		OnLiftUp ();
	}

	public void PutDown()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		enabled = true;
		carried = false;
		OnPutDown ();
	}

	protected virtual void OnLiftUp()
	{
	}

	protected virtual void OnPutDown()
	{
	}
}
