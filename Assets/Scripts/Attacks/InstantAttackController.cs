using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantAttackController : ProjectileAttack 
{
	[SerializeField]
	private float damage = 20;

	public override void AttackTarget(Vector3 from, GameObject target) {
		EnemyController enemy = target.GetComponent<EnemyController> ();
		if (enemy != null) {
			enemy.TakeDamage (damage);
		}
	}
}
