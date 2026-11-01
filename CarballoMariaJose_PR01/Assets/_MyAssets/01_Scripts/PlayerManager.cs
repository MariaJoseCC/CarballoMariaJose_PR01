using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    float moveX;
    float moveZ;
    bool isAlive;
    Vector3 posicionInicial;
    [SerializeField] private float moveSpeed =10f;

    inputAction inputActions;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        posicionInicial = new Vector3(-6f, 0.5f, -24f);
        transform.position = posicionInicial;

    }

    // Update is called once per frame
    void Update()
    {

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

    private void Awake()
    {
        inputActions = new inputAction();

        inputActions.Player.MoveZ.performed += ctx => moveZ = ctx.ReadValue<float>();
        inputActions.Player.MoveZ.canceled += ctx => moveZ = 0;

        inputActions.Player.MoveX.performed += ctx => moveX = ctx.ReadValue<float>();
        inputActions.Player.MoveX.canceled += ctx => moveX = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("suelo_fail"))
        {
            Morir();
        }
    }

    void Morir()
    {
        
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = posicionInicial;
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

