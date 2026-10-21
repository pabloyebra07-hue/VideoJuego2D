using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class SaltoMetodo2 : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocity;

    [SerializeField] private InputActionReference actionMover; //private para que no lo puedan cambiar con cualquier script y serializeField para que aparezca en unityHub

    [Header("Salto")]
    [SerializeField] private InputActionReference actionSaltar;

    public float fuerzaImpulso = 5f; //fuerza al saltar
    [SerializeField] private LayerMask capaSuelo;


    [Header("Chequeo de Suelo")]
    [SerializeField] private float groundDistance = 0.2F;
    [SerializeField] private Transform esferaPies; //objeto vavio en los pies 

    private Rigidbody2D fisica;

    private Vector2 entradaMovimiento;

    private bool estaTocandoSuelo;
    private bool saltoPendiente;

    private void Awake()
    {
        fisica = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        entradaMovimiento = actionMover.action.ReadValue<Vector2>();

        estaTocandoSuelo = Physics2D.OverlapCircle(esferaPies.position, groundDistance, capaSuelo);

        if(actionSaltar.action.WasPressedThisFrame() && estaTocandoSuelo)
        {
            saltoPendiente = true;
        }
    }

    private void FixedUpdate()
    {
        fisica.linearVelocity = new Vector2(entradaMovimiento.x * velocity, fisica.linearVelocityY);

        if (saltoPendiente)
        {
            fisica.AddForce(Vector2.up * fuerzaImpulso, ForceMode2D.Impulse); //Force salto continuo, impulse un impulso 
        }

        saltoPendiente = false;
    }

    private void OnDrawGizmosSelected()
    {
        if(esferaPies != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(esferaPies.position , groundDistance);
        }
    }

    private void OnEnable() // se ejecuta cada vez que acctivo el jugador
    {
        if ((actionMover == null) || actionMover.action == null)
        {
            Debug.LogError("Asigna Mapa de accion Jugador/Mover en Accion Mover");
            enabled = false;
            return;
        }
        actionMover.action.Enable();

        if ((actionSaltar == null) || actionSaltar.action == null)
        {
            Debug.LogError("Asigna Mapa de accion Jugador/Saltar en Accion Saltar");
            enabled = false;
            return;
        }
        actionSaltar.action.Enable();
    }

    private void OnDisable()
    {
        if (actionMover != null && actionMover.action != null)
        {
            actionMover.action.Disable();
        }

        if (actionSaltar != null && actionSaltar.action != null)
        {
            actionSaltar.action.Disable();
        }
    }
}
