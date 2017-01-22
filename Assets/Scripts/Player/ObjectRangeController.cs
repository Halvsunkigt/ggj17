using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(PlayerController), typeof(PlayerAnimation))]
public class ObjectRangeController : MonoBehaviour 
{
	private const string CARRY_BUTTON = "Fire1_Player";
	private const string THROW_BUTTON = "Fire2_Player";

	[SerializeField]
	private LayerMask carryLayerMask;

	[SerializeField]
	private LayerMask attackMask;

	[SerializeField]
	private Transform carryNode;

	private PlayerController player;
	private SphereCollider sphereCollider;
	private PlayerAnimation anim;

	private GameObject carryingObject;

	private float carryingTime = 0;

	void Start () 
	{
		player = GetComponent<PlayerController> ();
		sphereCollider = GetComponent<SphereCollider> ();
		anim = GetComponent<PlayerAnimation> ();
	}

	void FixedUpdate() 
	{
		if (Input.GetButtonDown (CARRY_BUTTON + player.PlayerIndex)) {
			OnActionButtonDown ();
		} else if (Input.GetButtonDown (THROW_BUTTON + player.PlayerIndex)) {
			OnThrowButtonDown ();
		}

		carryingTime += Time.fixedDeltaTime;
	}

	private void OnActionButtonDown ()
	{
		if (player.ControlsLocked) {
			return;
		}

		if (IsCarryingObject ()) {
			StopCarryingObject ();
		} else {
			var closestObject = GetClosestInteractable ();
			if (closestObject != null) {
				LayerMask closestObjectMask = 1 << closestObject.layer;
				bool isAttackable = (closestObjectMask & attackMask) != 0;
				if (isAttackable) {
					anim.Punch ();
					Attack (closestObject);
				} else {
					StartCarryingObject (closestObject);
				}
			}
		}
	}

	private void Attack(GameObject enemy) {
		EnemyController controller = enemy.GetComponent<EnemyController> ();
		if (controller != null) {
			controller.TakeDamage (player.AttackDamage);
			enemy.transform.position = enemy.transform.position + transform.forward * 1f;
		}
	}

	private GameObject GetClosestInteractable ()
	{
		float distance = float.MaxValue;
		GameObject closestInteractable = null;

		var colliders = Physics.OverlapSphere (transform.position, sphereCollider.radius, carryLayerMask | attackMask);
		foreach (var collider in colliders) {
			var collidedObject = collider.gameObject;
			if (collidedObject.Equals (gameObject)) {
				continue;
			}

			// Ignore objects that's already being carried
			ThrowableController throwable = collidedObject.GetComponent<ThrowableController> ();
			if (throwable != null && throwable.IsCarried) {
				continue;
			}

			var targetPos = collider.transform.position;
			var targetDir = targetPos - transform.position;
			float length = Vector3.Distance (targetPos, transform.position);
			targetDir = targetDir / length;
			if (Vector3.Dot (transform.forward, targetDir) <= 0.6) {
				continue;
			}

			if (distance > length) {
				closestInteractable = collider.gameObject;
				distance = length;
			}
		}

		return closestInteractable;
	}

	private void OnThrowButtonDown ()
	{
		if (!IsCarryingObject ()) {
			return;
		}

		var throwable = carryingObject.GetComponent<ThrowableController> ();
		if (throwable == null) {
			Debug.LogError ("Object " + carryingObject.name + " is not allowed to be thrown. Dropping it instead");
			StopCarryingObject ();
			return;
		}

		anim.Throw ();

		Vector3 throwArc = (gameObject.transform.forward + gameObject.transform.up).normalized;
		throwable.Throw (throwArc * player.ThrowPower);

		carryingObject.transform.parent = null;
		carryingObject = null;

		player.MovementSpeed = player.InitialMovementSpeed;
	}

	/// <summary>
	/// Start carrying object
	/// </summary>
	/// <param name="collidingObject">Colliding object.</param>
	public void StartCarryingObject (GameObject obj)
	{
		VendingMachineController vendingMachine = obj.GetComponent<VendingMachineController> ();
		if (vendingMachine != null) {
			vendingMachine.TryBuyItem (this);
			return;
		}

		var throwable = obj.GetComponent<ThrowableController> ();
		if (throwable != null) {
			throwable.LiftUp ();
		}

		carryingObject = obj;

		// Update parent node so that the carrying object is moved around with the player
		Vector3 offset = gameObject.transform.up * (sphereCollider.radius / 2.0f);
		carryingObject.transform.parent = carryNode;
		carryingObject.transform.position = gameObject.transform.position + offset;
		carryingObject.transform.rotation = new Quaternion ();
		carryingTime = 0;

		player.MovementSpeed = player.InitialMovementSpeed / 2.0f;
	}

	private void StopCarryingObject ()
	{
		if (carryingObject == null || carryingTime < 0.5f) {
			return;
		}

		var throwable = carryingObject.GetComponent<ThrowableController> ();
		if (throwable != null) {
			throwable.PutDown ();
		}

		Vector3 offset = gameObject.transform.forward * (sphereCollider.radius / 2.0f);
		carryingObject.transform.position = gameObject.transform.position + offset;
		carryingObject.transform.parent = null;
		carryingObject = null;

		player.MovementSpeed = player.InitialMovementSpeed;
	}

	private bool IsCarryingObject() 
	{
		return carryingObject != null;
	}

	private bool IsCarryingObject(GameObject obj)
	{
		return obj.Equals (carryingObject);
	}
}

