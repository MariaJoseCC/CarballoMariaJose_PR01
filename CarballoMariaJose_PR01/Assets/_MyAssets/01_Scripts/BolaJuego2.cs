
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
    float limiteIzquierdo = -77f;
    float limiteDerecho = 77f;
    float limiteInferior = -46f;

    [Header("Reaparicion")]
    float alturaReaparicion = 35f;

    private Rigidbody rb;
    private bool reapareciendo = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        ActualizarContadores();
        Reaparecer();
    }

    void FixedUpdate()
    {
        if (reapareciendo)
            return;

        // Para comprobar si la pelota ha caido por debajo del limite en Y para que pierda
        if (rb.position.y <= limiteInferior)
        {
            PerderVida();
            return;
        }

        // Para que la pelota no se vaya fuera de los limites horizontales
        Vector3 posicion = rb.position;

        posicion.x = Mathf.Clamp(posicion.x,limiteIzquierdo,limiteDerecho);

        rb.position = posicion;
    }


    void OnCollisionEnter(Collision collision)
    {
        PlataformaWin(collision.collider.transform);
    }


    void OnTriggerEnter(Collider other)
    {
        PlataformaWin(other.transform);
    }

    void PlataformaWin(Transform objeto)
    {
        if (reapareciendo)
            return;


        Transform actual = objeto;

        while (actual != null)
        {
            if (actual.CompareTag("plat_win"))
            {
                aciertos++;

                ActualizarContadores();

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

        ActualizarContadores();

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

        // Para que la bola aparezca en una posición aleatoria cada vez que reaparece pero dentro de los limites en X
        float moveX = Random.Range(limiteIzquierdo,limiteDerecho);

        // Nueva posicion X e Y
        Vector3 nuevaPosicion = new Vector3(moveX,alturaReaparicion,rb.position.z);

        rb.position = nuevaPosicion;

        // Reiniciar la velocidad para que no siga cayendo
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

       
        transform.position = nuevaPosicion;

        reapareciendo = false;
    }

    void ActualizarContadores()
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas: " + vidas;

        if (textoAciertos != null)
            textoAciertos.text = "Aciertos: " + aciertos;
    }
}
