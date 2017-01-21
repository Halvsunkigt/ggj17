using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	[SerializeField]
	private float damageReduction = 1f;

	[SerializeField]
	private GameObject dropType;

	private CoreController targetCore;
	private NavMeshAgent navAgent;
	private float hp = 100f;

	// Use this for initialization
	void Start ()
	{
		targetCore = GameObject.FindObjectOfType<CoreController> ();
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.destination = targetCore.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!IsWalking ()) {
			targetCore.TakeDamage (10f * Time.deltaTime);
			// Todo: attack in intervals with attack animation?
		}

		// Todo: if targetCore.isDestroyed then find new core
	}

	bool IsWalking ()
	{
		
		if (navAgent.pathPending) {
			return true;

		}

		if (navAgent.remainingDistance > navAgent.stoppingDistance) {
			return true;
	
		}

		if (navAgent.hasPath && navAgent.velocity.sqrMagnitude != 0f) {
			return true;
		}

		return false;
	}

	public void TakeDamage (float damage)
	{
		hp -= damage / damageReduction;
		if (hp <= 0f) {
			HasBeenKilled ();
		}
	}

	void HasBeenKilled ()
	{
		GameObject.Instantiate (dropType, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
