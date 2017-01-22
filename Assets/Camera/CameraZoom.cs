using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    [Range(0.0f, 1.0f)]
    public float range = 0f;

    public Vector3 ZoomInPos = new Vector3(0, 60, -50f);
    public Vector3 ZoomOutPos = new Vector3(0, 120, -42);
    public float ZoomInRot = 50;
    public float ZoomOutRot = 70;

    float lastRange;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (range != lastRange)
        {
            Vector3 currentPos = ((ZoomOutPos - ZoomInPos) * range) + ZoomInPos;
            float currentRot = (ZoomOutRot - ZoomInRot) * range + ZoomInRot; 
            transform.position = currentPos;
            transform.eulerAngles = new Vector3(currentRot, 0, 0);
        }
        lastRange = range;
	}
}
