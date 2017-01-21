using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AutoDestroyController : MonoBehaviour 
{
	private ParticleSystem system;

	void Start ()
	{
		system = GetComponent<ParticleSystem> ();	
	}

	void FixedUpdate ()
	{
		if (!system.IsAlive ()) {
			Destroy (gameObject);
		}	
	}
}
