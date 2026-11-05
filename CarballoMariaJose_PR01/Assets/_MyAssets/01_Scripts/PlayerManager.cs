using UnityEngine;
using UnityEngine.InputSystem;

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
        {
            Morir();
            print ("Fail");

            if (other.CompareTag("Corona"))
            {
                haGanado = true;
                print("Win!");
            }
        }
       
    }

    void Morir()
    {
        isAlive = false;

        rb.transform.position = posicionInicial;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        tiempoRestante = tiempoLimite;
        ultimoSegundo = -1;

        isAlive = true;
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

