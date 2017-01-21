using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoThrowableController : ThrowableController 
{
	void OnCollisionEnter(Collision other)
	{
		if (other.collider.tag == "Turret") {
			FillAmmo (other.gameObject);
		} else if (other.collider.tag == "Ground") {
			// Verify that we are not colliding with a turret
			var pos = gameObject.transform.position;
			Ray ray = new Ray (pos, Vector3.down);
			var hits = Physics.RaycastAll (ray);
			for (var i = 0; i < hits.Length; ++i) {
				if (hits [i].collider.tag == "Turret") {
					FillAmmo (other.gameObject);
					return;
				}
			}

			Disable ();
		}
	}

	/// <summary>
	/// Fills the ammo.
	/// </summary>
	/// <param name="turretObject">Turret object.</param>
	private void FillAmmo (GameObject turretObject)
	{
		var controller = turretObject.GetComponent<TurretController> ();
		if (controller == null) {
			Debug.LogError ("GameObject " + turretObject.name + " is not a valid turret");
			return;
		} else {
			Debug.Log ("Reloading " + turretObject.name);
			controller.Reload ();
		}
		Destroy (gameObject);
	}
}
