using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class InstantAttackController : ProjectileAttack 
{
	[SerializeField]
	private float damage = 20;

	[SerializeField]
	private float lifeTime = 0.2f;

	void Update()
	{
		lifeTime -= Time.deltaTime;
		if (lifeTime < 0) {
			Destroy (gameObject);
		}
	}

	public override void AttackTarget(GameObject from, GameObject target) {
		EnemyController enemy = target.GetComponent<EnemyController> ();
		if (enemy != null) {
			enemy.TakeDamage (damage);

			var script = GetComponentInChildren<LightningBoltScript> ();
			script.StartObject = from;
			script.EndObject = target;
		}
	}
}
