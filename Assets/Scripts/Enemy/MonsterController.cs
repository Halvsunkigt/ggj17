using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
	CoreController targetCore;
	Transform targetTransform;
	NavMeshAgent navAgent;

	// Use this for initialization
	void Start ()
	{
		targetCore = GameObject.FindObjectOfType<CoreController> ();
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.destination = targetCore.transform.position; 
		navAgent.stoppingDistance = targetTransform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (IsWalking ());

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
