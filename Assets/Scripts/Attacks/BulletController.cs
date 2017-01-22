using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : ProjectileAttack
{
	public float speed;
	public float damage;

	void OnTriggerEnter (Collider other)
	{
		EnemyController enemy = other.GetComponent<EnemyController> ();
		if (enemy != null) {
			enemy.TakeDamage (damage);
		}

		Destroy (gameObject);
	}

	public override void AttackTarget(Vector3 from, GameObject target) {
		transform.position = from;
		transform.LookAt (target.transform);

		Vector3 direction = target.transform.position - transform.position;
		GetComponent<Rigidbody> ().AddForce (direction.normalized * speed);
	}
}
