
using UnityEngine;

public class PlataformaMovimiento : MonoBehaviour
{
    [Header("Limites de movimiento")]
    public float limiteIzquierdo = -77f;
    public float limiteDerecho = 77f;

    [Header("Velocidad")]
    public float velocidad = 35f;

    private float posicionY;
    private float posicionZ;

    private float limiteRealIzquierdo;
    private float limiteRealDerecho;

    private Collider col;

    void Start()
    {
        col = GetComponent<Collider>();

        posicionY = transform.position.y;
        posicionZ = transform.position.z;

        // Para no superar los limites teniendo en cuenta el ancho de la plataforma
        float ancho = col != null ? col.bounds.extents.x : 0f;

        limiteRealIzquierdo = limiteIzquierdo + ancho;
        limiteRealDerecho = limiteDerecho - ancho;
    }

    void Update()
    {
        // Movimiento de izquierda a derecha en bucle
        float nuevaX = Mathf.PingPong(
            Time.time * velocidad,
            limiteRealDerecho - limiteRealIzquierdo
        ) + limiteRealIzquierdo;

        // Para la posicion de la plataforma
        transform.position = new Vector3(nuevaX,posicionY,posicionZ);
    }
}

