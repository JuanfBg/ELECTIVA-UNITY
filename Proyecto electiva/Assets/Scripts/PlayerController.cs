using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed;
	private float time;

	private Rigidbody rb;

	private Vector3 posicion;

	public GameObject capsulaT1;
	public GameObject capsulaT2;

	private int contPuntos;
	private int contCapsulas;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();//Referencia al componente de tipo Rigidbody de Player.
		contPuntos = 0;
		contCapsulas = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");//Captura del teclado movimiento horizontal
		float moveVertical = Input.GetAxis("Vertical");//Captura del teclado movimiento vertical


		Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);//Coordenadas


		rb.AddForce(movimiento * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		//Referencia al collider al cual hemos impactado.

		if (other.gameObject.CompareTag ("PlaneTrampa")) {
			Destroy (other.gameObject);
			capsulaT1.SetActive (false);
		} else if(other.gameObject.CompareTag("PlaneTrampa2")){
			Destroy (other.gameObject);
			capsulaT2.SetActive (false);
		} 
		else if (other.gameObject.CompareTag ("CapsulaBuena")) {
			contPuntos += 5;
			contCapsulas++;
			Destroy (other.gameObject);
			Debug.Log ("Se suman 5 puntos. Marcador: "+contPuntos);
			if(contCapsulas == 11){
				Debug.Log ("Total puntos obtenidos: "+contPuntos);
				Debug.Log ("Tiempo total: "+time);
				Debug.Log ("Fin del primer nivel");				
			}
		} else if (other.gameObject.CompareTag ("CapsulaMala")) {
			contPuntos -= 2;
			if (contPuntos < 0) {
				Debug.Log ("No tienes puntos, no se puede descontar");
				contPuntos = 0;
			} else {
				Destroy (other.gameObject);
				Debug.Log ("Se restan 2 puntos. Marcador: "+contPuntos);
			}

		} else {
			//nada
		}




	}
}
