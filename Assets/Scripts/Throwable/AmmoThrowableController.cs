using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoThrowableController : ThrowableComponent 
{
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Turret") {
			FillAmmo ();
		} else if (other.collider.tag == "Ground") {
			// Verify that we are not colliding with a turret
			var pos = gameObject.transform.position;
			Ray ray = new Ray (pos, Vector3.down);
			var hits = Physics.RaycastAll (ray);
			for (var i = 0; i < hits.Length; ++i) {
				if (hits [i].collider.tag == "Turret") {
					FillAmmo ();
					return;
				}
			}

			Disable ();
		}
	}

	void FillAmmo ()
	{
		//var turret = other.collider.gameObject
		//turret.SetAmmo(100);
		Destroy (gameObject);
	}
}
