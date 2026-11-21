
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
    float alturaReaparicion = 35f;

    private Rigidbody rb;
    private bool reapareciendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ActualizarInterfaz();
        Reaparecer();
    }

    void FixedUpdate()
    {
        if (reapareciendo)
            return;

        // Comprobar si la pelota ha caido por debajo del limite
        if (rb.position.y <= limiteInferior)
        {
            PerderVida();
            return;
        }

        // Mantener la pelota dentro de los limites horizontales
        Vector3 posicion = rb.position;

        posicion.x = Mathf.Clamp(
            posicion.x,
            limiteIzquierdo,
            limiteDerecho
        );

        rb.position = posicion;
    }

    // Detectar contacto mediante colision normal
    void OnCollisionEnter(Collision collision)
    {
        ComprobarPlataforma(collision.collider.transform);
    }

    // Detectar contacto si el Collider tiene Is Trigger activado
    void OnTriggerEnter(Collider other)
    {
        ComprobarPlataforma(other.transform);
    }

    void ComprobarPlataforma(Transform objeto)
    {
        if (reapareciendo)
            return;

        // Buscar la etiqueta en el objeto y sus padres
        Transform actual = objeto;

        while (actual != null)
        {
            if (actual.CompareTag("plat_win"))
            {
                aciertos++;

                ActualizarInterfaz();

                Reaparecer();

                return;
            }

            actual = actual.parent;
        }
    }

    void PerderVida()
    {
        if (reapareciendo)
            return;

        reapareciendo = true;

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
        reapareciendo = true;

        // Elegir una posicion X aleatoria dentro de los limites
        float moveX = Random.Range(
            limiteIzquierdo,
            limiteDerecho
        );

        // Establecer la nueva posicion X e Y
        Vector3 nuevaPosicion = new Vector3(
            moveX,
            alturaReaparicion,
            rb.position.z
        );

        rb.position = nuevaPosicion;

        // Reiniciar la velocidad para que no siga cayendo
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Sincronizar la posicion del objeto
        transform.position = nuevaPosicion;

        reapareciendo = false;
    }

    void ActualizarInterfaz()
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        if (textoAciertos != null)
            textoAciertos.text = "Aciertos: " + aciertos;
    }
}
