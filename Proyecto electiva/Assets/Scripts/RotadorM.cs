using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotadorM : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(45, 0, 90) * Time.deltaTime);
	}
}
