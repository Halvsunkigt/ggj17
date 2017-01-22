using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour {

	[SerializeField]
	private Transform follow;

	[SerializeField]
	private bool followPosition;

	[SerializeField]
	private bool followRotation;

	[SerializeField]
	private bool followScale;

	void Update () {
		if (followPosition)
		{
			transform.position = follow.position;
		}
		if (followRotation)
		{
			transform.rotation = follow.rotation;
		}
		if (followScale)
		{
			transform.localScale = follow.localScale;
		}
	}
}
