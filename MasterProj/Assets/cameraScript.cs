using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

    public GameObject focus;
    private float zPos;
    public float lerpFactor = 0.3f;

	// Use this for initialization
	void Start () {
        zPos = this.transform.position.z;
        //Vector3 target = focus.transform.position;
        //target.z = zPos;
        //this.transform.position = target;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 target = focus.transform.position;
        target.z = zPos;
        this.transform.position = Vector3.Lerp(this.transform.position, target, lerpFactor);
	}
}
