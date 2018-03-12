using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;//distancia entre la camara y la esfera.	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LateUpdate () {
		//Transform contiene escala, posicion y la rotacion
		//transform.position define la posicion de la camara.
		//player.transform.position define la posicion de la esfera
		transform.position = player.transform.position + offset;
	}
}
