using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private float x;
    private float y;
    private float z;

	// Use this for initialization
	void Start () {
        x = Random.Range(0.0f, 100f);
        y = Random.Range(0.0f, 100f);
        z = Random.Range(0.0f, 100f);
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(new Vector3(x, y, z) * Time.deltaTime);
	}
}
