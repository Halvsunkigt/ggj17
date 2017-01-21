using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private int bulletShootCount = 1;

	[SerializeField]
	private float shootInterval = 2f;

	[SerializeField]
	private float range = 4f;

	[SerializeField]
	private int initialAmmo = 25;

	private int ammo;
	private float nextShoot;


	// Use this for initialization
	void Start ()
	{
		nextShoot = Time.timeSinceLevelLoad + shootInterval;
		ammo = initialAmmo;
	}
	
	// Update is called once per frame
	void Update ()
	{
		TryToShoot ();
	}

	/// <summary>
	/// Reload this turrets ammunition
	/// </summary>
	public void Reload()
	{
		ammo = initialAmmo;
	}

	private void TryToShoot ()
	{
		if (nextShoot > Time.timeSinceLevelLoad) {
			return;
		}
		nextShoot = Time.timeSinceLevelLoad + shootInterval;

		if (IsOutOfAmmunition()) {
			return;
		}
		ammo--;

		EnemyController enemy = GetClosestEnemy ();
		if (enemy == null) {
			return;
		}

		GameObject bullet = Instantiate (bulletPrefab);
		bullet.transform.position = transform.position;
		bullet.transform.LookAt (enemy.transform);


		Vector3 direction = enemy.transform.position - transform.position;
		bullet.GetComponent<Rigidbody> ().AddForce (direction.normalized * bullet.GetComponent<BulletController> ().speed);

	}

	private bool IsOutOfAmmunition() {
		return ammo <= 0;
	}

	private EnemyController GetClosestEnemy ()
	{
		Collider[] colliders = Physics.OverlapSphere (transform.position, range);
		EnemyController closest = null;
		EnemyController enemy = null;
		float distance = range;

		//Debug.Log (colliders.Length);

		foreach (Collider hit in colliders) {

			enemy = hit.GetComponent<EnemyController> ();

			if (enemy == null) {
				continue;
			}

			if (closest == null || Vector3.Distance (transform.position, hit.transform.position) <= distance) {
				closest = enemy;
			}
		}

		return closest;
	}

	#if UNITY_EDITOR
	void OnDrawGizmosSelected ()
	{
		UnityEditor.Handles.color = Color.white;
		UnityEditor.Handles.DrawWireDisc (transform.position, transform.up, range);
	}
	#endif
}
