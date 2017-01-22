using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAnimation : MonoBehaviour {

	[SerializeField]
	private Animator animator;

	public void SetShooting (bool shooting)
	{
		animator.SetBool ("Shooting", shooting);
	}
}
