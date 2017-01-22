using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroller : MonoBehaviour {
    RectTransform trans;
    Vector3 startPos;
	// Use this for initialization
	void Start () {
        trans = GetComponent<RectTransform>();
        startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        trans.position = trans.position + transform.up * Time.deltaTime;

        //Debug.Log(trans.position.y);
        if (trans.position.y > 12)
        {
            transform.position = startPos;
        }
	}
}
