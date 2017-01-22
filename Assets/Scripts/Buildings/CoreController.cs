using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
	private static Quaternion UP = Quaternion.Euler (new Vector3 (-90, 0, 0));

	[SerializeField]
	private Transform hpTransform;

	[SerializeField]
	private GameObject damageEffect;

	[SerializeField]
	private GameObject destroyEffect;

	[SerializeField]
	private GameObject repairEffect;

	private float hp = 100f;
	private bool destroyed = false;
	
	// Update is called once per frame
	void Update ()
	{
		UpdateHp ();
	}

	private void UpdateHp ()
	{
		if (hp < 100f) {
			if (destroyed) {
				hp += Time.deltaTime * 2f;
				if (hp >= 100f) {
					HasBeenRebuilt ();
				}
			} else {
				GiveHealth (Time.deltaTime * 1f);
			}
		}
	}

	public void TakeDamage (float damage)
	{
		this.hp -= damage;
		UpdateHealthBar (hp);

		if (hp < 0f) {
			HasBeenDestroyed ();
		} else {
			if (damageEffect != null) {
				GameObject.Instantiate (damageEffect, gameObject.transform.position, UP);
			}
		}
	}

	public void GiveHealth(float health) 
	{
		this.hp = Mathf.Clamp (this.hp + health, 0.0f, 100.0f);
		UpdateHealthBar (hp);
		if (repairEffect != null) {
			GameObject.Instantiate (repairEffect, gameObject.transform.position, UP);
		}
	}

	private void UpdateHealthBar(float hp)
	{
		hpTransform.localScale = new Vector3 (hp / 100f, hpTransform.localScale.y, hpTransform.localScale.z);
		hpTransform.localPosition = new Vector3 (-0.5f + (hp / 100f) / 2, hpTransform.localPosition.y, hpTransform.localPosition.z);
	}

	private void HasBeenDestroyed ()
	{
		destroyed = true;
		hp = 0f;

		if (destroyEffect != null) {
			GameObject.Instantiate (destroyEffect, gameObject.transform.position, UP);
		}
	}

	private void HasBeenRebuilt ()
	{
		destroyed = false;
		hp = 100f;
	}
}
