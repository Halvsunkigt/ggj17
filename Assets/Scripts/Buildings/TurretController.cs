using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
	public GameObject bulletPrefab;
	public int bulletShootCount = 1;
	public float shootInterval = 2f;
	public float range = 4f;

	float nextShoot;

	// Use this for initialization
	void Start ()
	{
		nextShoot = Time.timeSinceLevelLoad + shootInterval;
	}
	
	// Update is called once per frame
	void Update ()
	{
		TryToShoot ();

	}

	void TryToShoot ()
	{
		if (nextShoot < Time.timeSinceLevelLoad) {
			
		}

		// Todo: Check ammo
	}

	EnemyController GetClosestEnemy ()
	{
		return GameObject.FindObjectOfType<EnemyController> ();
	}
}
