using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
	public Transform hpTransform;

	float hp = 100f;
	bool destroyed = false;
	
	// Update is called once per frame
	void Update ()
	{
		UpdateHp ();
	}

	void UpdateHp ()
	{
		if (hp < 100f) {
			if (destroyed) {
				hp += Time.deltaTime * 2f;
				if (hp >= 100f) {
					HasBeenRebuilt ();
				}
			} else {
				hp += Time.deltaTime * 1f;
			}
		}
	}

	public void TakeDamage (float damage)
	{
		hp -= damage;

		hpTransform.localScale = new Vector3 (hp / 100f, hpTransform.localScale.y, hpTransform.localScale.z);
		hpTransform.localPosition = new Vector3 (-0.5f + (hp / 100f) / 2, hpTransform.localPosition.y, hpTransform.localPosition.z);

		if (hp < 0f) {
			HasBeenDestroyed ();
		}
	}

	void HasBeenDestroyed ()
	{
		destroyed = true;
		hp = 0f;
	}

	void HasBeenRebuilt ()
	{
		destroyed = false;
		hp = 100f;
	}
}
