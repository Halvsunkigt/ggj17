using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
	CoreController targetCore;
	NavMeshAgent navAgent;

	// Use this for initialization
	void Start ()
	{
		targetCore = GameObject.FindObjectOfType<CoreController> ();
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.destination = targetCore.transform.position; 
		navAgent.stoppingDistance = targetCore.transform.localScale.x;
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
}
