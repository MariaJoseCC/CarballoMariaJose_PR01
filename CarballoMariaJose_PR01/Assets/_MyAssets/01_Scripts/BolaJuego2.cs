using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BolaJuego2 : MonoBehaviour
{
   
    [Header("Vidas y puntuacion")]
    public int vidas = 3;
    public int aciertos = 0;

    public TMP_Text textoVidas;
    public TMP_Text textoAciertos;

    [Header("Limites del escenario")]
    public float limiteIzquierdo = -77f;
    public float limiteDerecho = 77f;
    public float limiteInferior = -46f;

    [Header("Reaparicion")]
    public float alturaReaparicion = 5f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ActualizarInterfaz();
        Reaparecer();
    }

    void FixedUpdate()
    {
        // Comprobar si la pelota sale por abajo
        if (rb.position.y < limiteInferior)
        {
            PerderVida();
        }

        // Limitar el movimiento horizontal
        Vector3 posicion = rb.position;

        posicion.x = Mathf.Clamp(posicion.x,limiteIzquierdo,limiteDerecho);

        rb.position = posicion;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Comprobar si toca una plataforma
        if (collision.gameObject.CompareTag("plat_win"))
        {
            aciertos++;

            ActualizarInterfaz();

            Reaparecer();
        }
    }

    void PerderVida()
    {
        vidas--;

        ActualizarInterfaz();

        if (vidas <= 0)
        {
           
            SceneManager.LoadScene("Escena_Inicial");
        }
        else
        {
            Reaparecer();
        }
    }

    void Reaparecer()
    {
        float moveX = Random.Range(limiteIzquierdo,limiteDerecho);

        Vector3 nuevaPosicion = new Vector3(moveX,alturaReaparicion,rb.position.z);

        // Reiniciar posicion y velocidad
        rb.position = nuevaPosicion;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void ActualizarInterfaz()
    {
        textoVidas.text = "Vidas: " + vidas;
        textoAciertos.text = "Aciertos: " + aciertos;
    }
}

