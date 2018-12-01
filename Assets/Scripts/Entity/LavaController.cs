using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour {

    public float raiseSpeed = 0.1f;
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + (raiseSpeed * Time.deltaTime), transform.localPosition.z);
	}
}
