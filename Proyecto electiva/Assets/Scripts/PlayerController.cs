using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	private float time;
    public float timePanelNivel1;

    private bool banderaPanelNivel1;

    private Rigidbody rb;

	private Vector3 posicion;

	public GameObject capsulaT1;
	public GameObject capsulaT2;

	private int contPuntos;
	private int contCapsulas;

    public Text textoMarcador;
    public Text textoCronometro;
    public Text textoPanelGanador;
    public Text textoPanelPerdedor;
    private Text textoSumaRestaP;

    private AudioSource audioRecoleccion;
    private AudioSource audioRecoleccionTrampa;

    public Transform particulas;
    private ParticleSystem systemaParticulas;

    public float x, z;

    public GameObject panelg;
    public GameObject panelp;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody>();//Referencia al componente de tipo Rigidbody de Player.
		contPuntos = 0;
		contCapsulas = 0;
        textoMarcador.text = "Marcador: 0";
        textoCronometro.text = "Cronómetro: 0";
        AudioSource[] allMyAudioSource = GetComponents<AudioSource>();
        audioRecoleccion = allMyAudioSource[0];
        audioRecoleccionTrampa = allMyAudioSource[1];
        systemaParticulas = particulas.GetComponent<ParticleSystem>();
        systemaParticulas.Stop();
        panelg.SetActive(false);
        panelp.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		time += Time.deltaTime;
        textoCronometro.text = "Cronómetro: " + time.ToString("f0") ;

        if (time>=60) {
            speed = 0;
            Debug.Log("Perdiste, se acabó el tiempo.");
            textoCronometro.text = "Cronómetro: 60";
            textoPanelPerdedor.text = "¡PERDISTE! Se te acabó el tiempo. Eres muy lento";
            panelp.SetActive(true);
        }

        if (banderaPanelNivel1)
        {
            timePanelNivel1 -= Time.deltaTime;
            if (timePanelNivel1 < 0)
            {
                //Application.LoadLevel(1);
                SceneManager.LoadScene(1);
            }
        }
    }

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");//Captura del teclado movimiento horizontal
		float moveVertical = Input.GetAxis("Vertical");//Captura del teclado movimiento vertical


		Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);//Coordenadas


		rb.AddForce(movimiento * speed);

        x = transform.position.y;


        if (x < -30)
        {
           	Debug.Log("se cae");
            textoPanelPerdedor.text = "¡PERDISTE! Se te cayó la bola. Eres malo con las flechas";
            panelp.SetActive(true);
        }
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
            textoMarcador.text = "Marcador: "+contPuntos;
            posicion = other.gameObject.transform.position;//Obtener la posicion del objeto que estamos colisionando.
            particulas.position = posicion;//Asignarle 
            systemaParticulas = particulas.GetComponent<ParticleSystem>();//Obtener el componente ParticleSystem de particulas
            systemaParticulas.Play();
            audioRecoleccion.Play();
            if (contCapsulas == 11){
                speed = 0;
				Debug.Log ("Total puntos obtenidos: "+contPuntos);
				Debug.Log ("Tiempo total: "+time);
				Debug.Log ("Fin del primer nivel");
                textoPanelGanador.text = "¡PASASTE AL SEGUNDO NIVEL!\n\nTiempo total: "+time.ToString("f0")+"\n\nPuntos totales: "+contPuntos;
                panelg.SetActive(true);
                timePanelNivel1 = 3;
                banderaPanelNivel1 = true;
            }
		} else if (other.gameObject.CompareTag ("CapsulaMala")) {
			contPuntos -= 2;
			if (contPuntos < 0) {
				Debug.Log ("No tienes puntos, no se puede descontar");
				contPuntos = 0;
			} else {
				Destroy (other.gameObject);
                textoMarcador.text = "Marcador: " + contPuntos;
                Debug.Log ("Se restan 2 puntos. Marcador: "+contPuntos);
                posicion = other.gameObject.transform.position;//Obtener la posicion del objeto que estamos colisionando.
                particulas.position = posicion;//Asignarle 
                systemaParticulas = particulas.GetComponent<ParticleSystem>();//Obtener el componente ParticleSystem de particulas
                systemaParticulas.Play();
                audioRecoleccionTrampa.Play();
            }

		} else {
			//nada
		}




	}
}
