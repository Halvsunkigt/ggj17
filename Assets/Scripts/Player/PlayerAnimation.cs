using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

	[SerializeField] 
	private Animator animator;

	public void SetSpeed (float speed)
	{
		animator.SetFloat ("Speed", speed);
	}

	public void Throw ()
	{
		animator.SetTrigger ("Throw");
	}

	public void Punch ()
	{
		animator.SetTrigger ("Punch");
	}

	public void Die ()
	{
		animator.SetTrigger ("Die");
	}
}
