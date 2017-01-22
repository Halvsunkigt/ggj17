using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileAttack : MonoBehaviour
{
	/// <summary>
	/// Attacks the supplied target from the given position
	/// </summary>
	/// <param name="from">From.</param>
	/// <param name="target">Target.</param>
	public abstract void AttackTarget(GameObject from, GameObject target);

}
