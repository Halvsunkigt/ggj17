using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreController : MonoBehaviour
{
	float hp = 100f;
	bool destroyed = false;

	// Use this for initialization
	void Start ()
	{
		
	}
	
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
