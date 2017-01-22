using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimation : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	private Callback latestCallback;

	public delegate void Callback();
	public void SetShooting (bool shooting, Callback callback)
	{
		animator.SetBool ("Shooting", shooting);
		latestCallback = callback;
	}

	public void DoCallback ()
	{
		Debug.Log ("Woop");
		if (latestCallback != null)
		{
			latestCallback ();
		}
	}
}
