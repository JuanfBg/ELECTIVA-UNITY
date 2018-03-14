using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	private Vector3 posicion;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();//Referencia al componente de tipo Rigidbody de Player.
	}
	
	// Update is called once per frame
	void Update () {
		
	}







	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");//Captura del teclado movimiento horizontal
		float moveVertical = Input.GetAxis("Vertical");//Captura del teclado movimiento vertical


		Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);//Coordenadas


		rb.AddForce(movimiento * speed);
	}


	void OnTriggerEnter(Collider other){
		//Debug.Log ("jijijjii");
		if (other.gameObject.CompareTag ("Recolectable")){


			Destroy (other.gameObject);

		}else{
	}
	
	}




}
