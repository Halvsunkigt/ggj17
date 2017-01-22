using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
	private static Quaternion UP = Quaternion.Euler (new Vector3 (-90, 0, 0));

	[SerializeField]
	private float damageReduction = 1f;

	[SerializeField]
	private GameObject dropType;

	[SerializeField]
	private float initialAttackSpeed = 1.0f;

	[SerializeField]
	private float initialAttackDamage = 10.0f;

	[SerializeField]
	private GameObject attackEffect;

	private CoreController targetCore;
	private NavMeshAgent navAgent;
	private float hp = 100f;
	private float attackSpeed;
	private float attackDamage;
	private float nextAttackTime;

	/// <summary>
	/// Retireves the initial attack speed
	/// </summary>
	public float InitialAttackSpeed
	{
		get {
			return initialAttackSpeed;
		}
	}

	/// <summary>
	/// Gets or sets the attack speed.
	/// </summary>
	/// <value>The attack speed</value>
	public float AttackSpeed
	{
		get {
			return attackSpeed;
		}
		set {
			attackSpeed = value;
		}
	}

	/// <summary>
	/// Retrieves the initial attack damage
	/// </summary>
	/// <value>The initial attack damage.</value>
	public float InitialAttackDamage
	{
		get {
			return initialAttackDamage;
		}
	}

	/// <summary>
	/// Gets or sets how much damage this enemy does
	/// </summary>
	/// <value>The attack damage.</value>
	public float AttackDamage
	{
		get {
			return attackDamage;
		}
		set {
			attackDamage = value;
		}
	}

	// Use this for initialization
	void Start ()
	{
		targetCore = GameObject.FindObjectOfType<CoreController> ();
		navAgent = GetComponent<NavMeshAgent> ();
		navAgent.destination = targetCore.transform.position;
		attackSpeed = initialAttackSpeed;
		attackDamage = initialAttackDamage;
		nextAttackTime = Time.timeSinceLevelLoad + attackSpeed;
	}

	void FixedUpdate ()
	{
		if (!IsWalking ()) {
			Attack ();
		}

		// Todo: if targetCore.isDestroyed then find new core
	}

	private void Attack ()
	{
		if (targetCore != null && nextAttackTime < Time.timeSinceLevelLoad) {
			targetCore.TakeDamage (attackDamage);
			nextAttackTime = Time.timeSinceLevelLoad + attackSpeed;

			if (attackEffect != null) {
				GameObject.Instantiate (attackEffect, gameObject.transform.position, UP);
			}
		}
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
