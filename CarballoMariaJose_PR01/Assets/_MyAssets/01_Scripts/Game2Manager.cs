using UnityEngine;

public class Game2Manager : MonoBehaviour

{
    inputAction inputActions;
    float moveL;
    float moveR;
    float fuerza =1f;
    Rigidbody[] rb1;
    Rigidbody[] rb2;

    private void Awake()
    {
        inputActions = new inputAction();

        inputActions.Plataformas.Plataforma1.performed += ctx => moveL = ctx.ReadValue<float>();
        inputActions.Plataformas.Plataforma1.canceled += _ => moveL = 0f;

        inputActions.Plataformas.Plataforma2.performed += ctx => moveR = ctx.ReadValue<float>();
        inputActions.Plataformas.Plataforma2.canceled += _ => moveR = 0f;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    void Start()
    {
        GameObject[] plataformas1 = GameObject.FindGameObjectsWithTag("plat1");
        GameObject[] plataformas2 = GameObject.FindGameObjectsWithTag("plat2");

        rb1 = new Rigidbody[plataformas1.Length];
        rb2 = new Rigidbody[plataformas2.Length];

        for (int i = 0; i < plataformas1.Length; i++)
        {
            rb1[i] = plataformas1[i].GetComponent<Rigidbody>();
        }

        for (int i = 0; i < plataformas2.Length; i++)
        {
            rb2[i] = plataformas2[i].GetComponent<Rigidbody>();
        }
    }

    private void FixedUpdate()
    {
        
        foreach (Rigidbody rb in rb1)
        {
            rb.AddTorque(Vector3.forward * -moveL * fuerza);
        }

        
        foreach (Rigidbody rb in rb2)
        {
            rb.AddTorque(Vector3.forward * -moveR * fuerza);
        }
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
