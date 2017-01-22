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

	[SerializeField]
	private LayerMask obstacleSearchMask;

	private int ammo;
	private float timeUntilNextShot;

	private TurretAnimation anim;


	// Use this for initialization
	void Start ()
	{
		timeUntilNextShot = Time.timeSinceLevelLoad + shootInterval;
		ammo = initialAmmo;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timeUntilNextShot > Time.timeSinceLevelLoad) {
			return;
		}
		timeUntilNextShot = Time.timeSinceLevelLoad + shootInterval;

		if (IsOutOfAmmunition()) {
			return;
		}

		var target = FindClosestEnemy ();
		if (target != null) {
			FireBullet (target.gameObject);
		}
	}

	/// <summary>
	/// Check to see if the distance between the two positions are within range of each other.
	/// </summary>
	/// <returns><c>true</c> if this instance is within range; otherwise, <c>false</c>.</returns>
	/// <param name="fromPosition">The first position.</param>
	/// <param name="toPosition">The second position.</param>
	/// <param name="range">The range.</param>
	private bool IsWithinRange(Vector3 fromPosition, Vector3 toPosition, float range) 
	{
		var sqrdist = Vector3.SqrMagnitude (fromPosition - toPosition);
		return sqrdist <= range * range;
	}

	/// <summary>
	/// Reload this turret's ammunition
	/// </summary>
	public void Reload()
	{
		ammo = initialAmmo;
		var indicator = transform.GetComponentInChildren<IndicatorEnableController> ();
		if (indicator != null) {
			indicator.Hide ();
		}
	}

	/// <summary>
	/// Fire a bullet at the supplied target
	/// </summary>
	/// <param name="target">A target</param>
	public void FireBullet (GameObject target)
	{
		if (IsOutOfAmmunition()) {
			return;
		}

		anim.SetShooting (true);

		ammo--;
		if (ammo == 0) {
			var indicator = transform.GetComponentInChildren<IndicatorEnableController> ();
			if (indicator != null) {
				indicator.Show ();
			}
		}

		var attackObject = Instantiate (bulletPrefab);
		var attack = attackObject.GetComponent<ProjectileAttack> ();
		var attackFrom = GetAttackFromObject (gameObject);
		attack.AttackTarget (attackFrom, target);
	}

	/// <summary>
	/// Try to find a point to attack from
	/// </summary>
	/// <returns>The attack from object.</returns>
	/// <param name="root">Root.</param>
	private GameObject GetAttackFromObject(GameObject root) {
		var from = root.transform.FindChild ("AttackFrom");
		if (from == null) {
			return root;
		}
		return from.gameObject;
	}

	/// <summary>
	/// Determines whether this instance is out of ammunition or not.
	/// </summary>
	/// <returns><c>true</c> if this instance is out of ammunition; otherwise, <c>false</c>.</returns>
	private bool IsOutOfAmmunition() {
		return ammo <= 0;
	}

	/// <summary>
	/// Retrieves the closest enemy - if found
	/// </summary>
	/// <returns>The closest enemy.</returns>
	private GameObject FindClosestEnemy ()
	{
		var enemies = GetEnemiesInSight ();
		if (enemies.Count == 0) {
			return null;
		}

		float distance = float.MaxValue;
		GameObject closestEnemy = null;

		foreach (var enemy in enemies) {
			var enemyPos = enemy.transform.position;
			var sqrdist = Vector3.SqrMagnitude (enemyPos - transform.position);
			if (distance > sqrdist) {
				closestEnemy = enemy;
				distance = sqrdist;
			}
		}

		return closestEnemy;
	}

	/// <summary>
	/// Locate all enemies that's within this turret's range and line-of-sight
	/// </summary>
	/// <returns>The enemies</returns>
	private List<GameObject> GetEnemiesInSight() 
	{
		List<GameObject> enemies = new List<GameObject> ();
		Collider[] colliders = Physics.OverlapSphere (transform.position, range);
		foreach (var hit in colliders) {
			var enemy = hit.GetComponent<EnemyController> ();
			if (enemy == null) {
				continue;
			}
			var direction = (hit.transform.position - transform.position).normalized;
			Ray ray = new Ray (transform.position, direction);
			var result = Physics.RaycastAll (ray, range, obstacleSearchMask);
			if (result.Length == 0) {
				enemies.Add (hit.gameObject);
			}
		}
		return enemies;
	}

	#if UNITY_EDITOR
	void OnDrawGizmosSelected ()
	{
		UnityEditor.Handles.color = Color.white;
		UnityEditor.Handles.DrawWireDisc (transform.position, transform.up, range);
	}
	#endif
}
