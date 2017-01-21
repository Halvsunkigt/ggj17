using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxThrowableController : ThrowableComponent 
{
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Ground") {
			enabled = false;
		}
	}
}
