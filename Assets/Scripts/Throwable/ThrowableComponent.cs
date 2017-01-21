using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ThrowableComponent : MonoBehaviour
{
	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	public void OnEnable()
	{
		rb.isKinematic = false;
	}

	public void OnDisable()
	{
		rb.isKinematic = true;
	}

	public void Throw(Vector3 velocity) {
		rb.velocity = velocity;
		enabled = true;
	}
}
