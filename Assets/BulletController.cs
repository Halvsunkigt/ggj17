using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
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
}
