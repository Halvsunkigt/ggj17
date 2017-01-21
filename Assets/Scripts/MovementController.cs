using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour 
{
	[SerializeField]
	private float movementSpeed = 3.0f;

	[SerializeField] 
	private float stationaryTurnSpeed = 360;

	[SerializeField] 
	private float movingTurnSpeed = 360 * 2;

	private Rigidbody rb;

	private float turnAmount = 0;
	private float vertical = 0;
	private float horizontal = 0;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}

	void FixedUpdate () 
	{
		rb.velocity = new Vector3 (horizontal * movementSpeed, rb.velocity.y, vertical * movementSpeed);
	}

	public void Rotate(Vector3 rotate)
	{
		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired direction.
		if (rotate.magnitude > 1f)
			rotate.Normalize ();
		rotate = transform.InverseTransformDirection (rotate);
		turnAmount = Mathf.Atan2 (rotate.x, rotate.z);
	
		ApplyExtraTurnRotation ();
	}

	void ApplyExtraTurnRotation ()
	{
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp (stationaryTurnSpeed, movingTurnSpeed, 1);
		transform.Rotate (0, turnAmount * turnSpeed * Time.deltaTime, 0);
	}

	/// <summary>
	/// Tell this controller to move the player in the supplied directions
	/// </summary>
	/// <param name="h">Amount of movement on the horizontal plane</param>
	/// <param name="v">Amount of movement on the vertical plane</param>
	public void Move(float h, float v)
	{
		this.horizontal = h;
		this.vertical = v;
	}
}
