using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using TMPro;
using UnityEngine.SocialPlatforms;

public class PlayerManager : MonoBehaviour
{
    float moveX;
    float moveZ;
    bool isAlive;
    Vector3 posicionInicial;
    [SerializeField] private float moveSpeed =7f;

    float tiempoLimite = 100f;
    float tiempoRestante;
    bool haGanado = false;
    int ultimoSegundo;

    

    [SerializeField] private TMP_Text textoTiempo;
    [SerializeField] private TMP_Text textoDistancia;
    [SerializeField] private Transform meta;

    inputAction inputActions;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        posicionInicial = new Vector3(-6f, 0.5f, -24f);
        transform.position = posicionInicial;
        tiempoRestante = tiempoLimite;
        isAlive = true;
        ultimoSegundo = -1;
    }

    // Update is called once per frame
    void Update()
    {
        Contador();
        ContadorDistancia();
    }

    private void FixedUpdate()
    {
       MovePlayer();
       
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(moveX, 0, moveZ);

        rb.AddForce(movement * moveSpeed);
    }

    void Contador()
    {
        {
            if (haGanado || !isAlive)
                return;

            tiempoRestante -= Time.deltaTime;
            int segundos = Mathf.CeilToInt(tiempoRestante);
            textoTiempo.text = "Tiempo: " + segundos + " s";


            if (segundos != ultimoSegundo)
            {
                print("Tiempo restante: " + segundos);
                ultimoSegundo = segundos;
            }

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                print("Fail");
                Morir();
            }
        }
    }

    
  void ContadorDistancia()
    {
        if (haGanado || !isAlive)
            return;

        float distancia = Vector3.Distance(transform.position, meta.position);

        textoDistancia.text = "Meta: " + distancia.ToString("F2") + " m";
    }

    private void Awake()
    {
        inputActions = new inputAction();

        inputActions.Player.MoveZ.performed += ctx => moveZ = ctx.ReadValue<float>();
        inputActions.Player.MoveZ.canceled += ctx => moveZ = 0;

        inputActions.Player.MoveX.performed += ctx => moveX = ctx.ReadValue<float>();
        inputActions.Player.MoveX.canceled += ctx => moveX = 0;

        inputActions.Player.MoveZ1.performed += ctx => moveZ = ctx.ReadValue<float>();
        inputActions.Player.MoveZ1.canceled += ctx => moveZ = 0;

        inputActions.Player.MoveX1.performed += ctx => moveX = ctx.ReadValue<float>();
        inputActions.Player.MoveX1.canceled += ctx => moveX = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Corona"))
        {
            haGanado = true;
            print("Win!");
            VolverAlMenu();
        }
        else
        {
            print("Fail");
            Morir();
        }
    }

    void Morir()
    {        
         if (!isAlive)
          return;

          isAlive = false;

          VolverAlMenu();
    }
    void VolverAlMenu()
    {
        SceneManager.LoadScene("Escena_inicial");
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
}

