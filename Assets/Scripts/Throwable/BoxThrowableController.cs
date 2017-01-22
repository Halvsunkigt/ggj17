using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxThrowableController : ThrowableController 
{
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Ground") {
			LiftUp ();
		}
	}
}
	