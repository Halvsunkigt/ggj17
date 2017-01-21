using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour 
{
	private Rigidbody rb;

	private float forward = 0;
	private float right = 0;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		rb.velocity = new Vector3 (right, 0, forward);
	}

	public void MoveForward(float forward)
	{
		this.forward = forward;
	}

	public void MoveRight(float right)
	{
		this.right = right;
	}
}
