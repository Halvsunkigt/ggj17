﻿using System.Collections;
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
	/*
	override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (stateInfo.IsName ("Idle"))
		{
			if (latestCallback != null)
			{
				latestCallback ();
			}
		}
	}*/
}
